from flask import Flask, jsonify, request
import pandas as pd
import numpy as np
import sqlalchemy
import random
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics import jaccard_score
import time

app = Flask(__name__)
db_server = '(localdb)\\MSSQLLocalDB'
db_name = 'aspnet-FoodRecipeProvider-7d86217e-e193-4fe9-8574-ee04e3043999'
db_driver = 'ODBC+Driver+18+for+SQL+Server'

db_uri = f'mssql+pyodbc://{db_server}/{db_name}?driver={db_driver}'

engine = sqlalchemy.create_engine(db_uri)

def load_cbf_data():
    ingredients_query = "SELECT * FROM [dbo].[Ingredients]"
    recipes_query = "SELECT * FROM [dbo].[Recipes]"
    userreciperates_query = "SELECT * FROM [dbo].[UserRecipeRates]"
    recipe_ingredients_query = "SELECT * FROM [dbo].[RecipeIngredients]"

    ingredients_df = pd.read_sql_query(ingredients_query, engine)
    recipes_df = pd.read_sql_query(recipes_query, engine)
    userreciperates_df = pd.read_sql_query(userreciperates_query, engine)
    recipe_ingredients_df = pd.read_sql_query(recipe_ingredients_query, engine)

    merged_df = userreciperates_df.merge(recipes_df, left_on='AppRecipeId', right_on='Id')
    merged_df = merged_df.merge(recipe_ingredients_df, left_on='AppRecipeId', right_on='AppRecipeId')
    merged_df = merged_df.merge(ingredients_df, left_on='IngredientId', right_on='Id')
    merged_df.to_csv("1.csv")

    recipe_features = merged_df.groupby('Uri')['Name'].apply(lambda x: ' '.join(x)).reset_index()

    tfidf = TfidfVectorizer(stop_words='english')
    recipe_features_matrix = tfidf.fit_transform(recipe_features['Name'])

    item_similarity_matrix = cosine_similarity(recipe_features_matrix)
    
    return merged_df, recipe_features, item_similarity_matrix, tfidf


def cbf_recipes_recommendation(user_id, top_n=5):
    try:
        merged_df, recipe_features, item_similarity_matrix, tfidf = load_cbf_data()
        merged_df.to_csv("1.csv")
        recipe_features.to_csv("2.csv")
        #item_similarity_matrix.to_csv("3.csv")
        #tfidf.to_csv("4.csv")
        user_rated_recipes = merged_df[merged_df['AppUserId'] == user_id]['Uri'].unique()
        print(user_rated_recipes)
        recommended_recipes = {}

        for rated_recipe in user_rated_recipes:
            recipe_idx = recipe_features[recipe_features['Uri'] == rated_recipe].index[0]
            similar_recipes = item_similarity_matrix[recipe_idx].argsort()[::-1][1:]  # Exclude self
            for sim_recipe_idx in similar_recipes:
                sim_recipe_uri = recipe_features.iloc[sim_recipe_idx]['Uri']
                if sim_recipe_uri not in user_rated_recipes:
                    if sim_recipe_uri not in recommended_recipes:
                        recommended_recipes[sim_recipe_uri] = 0
                    recommended_recipes[sim_recipe_uri] += item_similarity_matrix[recipe_idx, sim_recipe_idx]

        recommended_recipes = sorted(recommended_recipes.items(), key=lambda x: x[1], reverse=True)[:top_n]

        return recommended_recipes

    except Exception as ex:
        print(f"Error in recommend_recipes: {str(ex)}")
        raise ex
    
def load_cf_data():
    diet_labels_query = "SELECT AppUserId, DietLabelId FROM UserDietLabels"
    health_labels_query = "SELECT AppUserId, HealthLabelId FROM UserHealthLabels"
    cuisine_types_query = "SELECT AppUserId, CuisineTypeId FROM UserCuisineTypes"

    diet_labels_df = pd.read_sql_query(diet_labels_query, engine)
    health_labels_df = pd.read_sql_query(health_labels_query, engine)
    cuisine_types_df = pd.read_sql_query(cuisine_types_query, engine)

    merged_df = pd.merge(diet_labels_df, health_labels_df, on='AppUserId')
    merged_df = pd.merge(merged_df, cuisine_types_df, on='AppUserId')

    merged_df['DietLabelId'] = merged_df['DietLabelId'].astype(str)
    merged_df['HealthLabelId'] = merged_df['HealthLabelId'].astype(str)
    merged_df['CuisineTypeId'] = merged_df['CuisineTypeId'].astype(str)

    grouped_df = merged_df.groupby('AppUserId').agg({
        'DietLabelId': lambda x: ','.join(set(x)),
        'HealthLabelId': lambda x: ','.join(set(x)),
        'CuisineTypeId': lambda x: ','.join(set(x))
    }).reset_index()

    max_length = 36

    def pad_labels(label_str):
        labels = label_str.split(',')
        labels = labels[:max_length]
        labels += ['0'] * (max_length - len(labels))
        return ','.join(labels)

    grouped_df['DietLabelId'] = grouped_df['DietLabelId'].apply(pad_labels)
    grouped_df['HealthLabelId'] = grouped_df['HealthLabelId'].apply(pad_labels)
    grouped_df['CuisineTypeId'] = grouped_df['CuisineTypeId'].apply(pad_labels)

    return grouped_df

def preprocess_user_attributes(users_df):
    users_df['DietLabels'] = users_df['DietLabelId'].apply(lambda x: x.split(',') if x else [])
    users_df['HealthLabels'] = users_df['HealthLabelId'].apply(lambda x: x.split(',') if x else [])
    users_df['CuisineTypes'] = users_df['CuisineTypeId'].apply(lambda x: x.split(',') if x else [])

    return users_df

def calculate_jaccard_similarity(user1_attributes, user2_attributes):
    diet_labels_1 = user1_attributes['DietLabels']
    diet_labels_2 = user2_attributes['DietLabels']
    health_labels_1 = user1_attributes['HealthLabels']
    health_labels_2 = user2_attributes['HealthLabels']
    cuisine_types_1 = user1_attributes['CuisineTypes']
    cuisine_types_2 = user2_attributes['CuisineTypes']

    diet_similarity = jaccard_score(diet_labels_1, diet_labels_2, average='micro')
    health_similarity = jaccard_score(health_labels_1, health_labels_2, average='micro')
    cuisine_similarity = jaccard_score(cuisine_types_1, cuisine_types_2, average='micro')

    overall_similarity = (diet_similarity + health_similarity + cuisine_similarity) / 3
    return overall_similarity

def cf_recipes_recommendation(user_id, top_n=5):
    users_df = load_cf_data()
    users_df = preprocess_user_attributes(users_df)
    user = users_df[users_df['AppUserId'] == user_id].iloc[0]

    user_attributes = users_df[users_df['AppUserId'] == user_id].iloc[0]
    users_df['Similarity'] = users_df.apply(lambda x: calculate_jaccard_similarity(user_attributes, x), axis=1)

    similar_users = users_df[users_df['AppUserId'] != user_id].sort_values(by='Similarity', ascending=False)
    top_similar_users = similar_users[['AppUserId', 'Similarity']].head(5)
    similar_users_filtered = top_similar_users[top_similar_users['Similarity'] >= 0.80]

    if not similar_users_filtered.empty:
        selected_user_id = similar_users.iloc[np.random.randint(0, len(similar_users_filtered))]['AppUserId']

    userreciperates_query = "SELECT * FROM [dbo].[UserRecipeRates]"
    userreciperates_df = pd.read_sql_query(userreciperates_query, engine)

    rated_by_user = userreciperates_df[userreciperates_df['AppUserId'] == selected_user_id]

    userreciperates_query = f"SELECT TOP {top_n} r.Id, r.Uri FROM [dbo].[UserRecipeRates] ur INNER JOIN [dbo].[Recipes] r ON ur.AppRecipeId = r.Id WHERE ur.AppUserId = '{selected_user_id}' ORDER BY ur.Rate DESC"
    recommended_recipes = pd.read_sql_query(userreciperates_query, engine)['Uri'].tolist()
    return(recommended_recipes)

@app.route('/recommend', methods=['POST'])
def get_recommendations():
    try:
        data = request.get_json()
        if data is None:
            return jsonify({'error': 'Invalid JSON payload'}), 400

        user_id = data.get('user_id')
        top_n = data.get('top_n', 5)

        if user_id is None:
            return jsonify({'error': 'user_id is required'}), 400

        cbf_recommended_recipes = cbf_recipes_recommendation(user_id, top_n)
        cbf_recipe_uris = [recipe_uri for recipe_uri, _ in cbf_recommended_recipes]
        
        try:
            cf_recommended_recipes = cf_recipes_recommendation(user_id, top_n)
            cf_recipe_uris = [recipe_uri for recipe_uri in cf_recommended_recipes]
        except Exception as cf_ex:
            print(f"Error in cf_recipes_recommendation: {str(cf_ex)}")
            cf_recipe_uris = []

        recommended_recipes_uris = cbf_recipe_uris + cf_recipe_uris
        random.shuffle(recommended_recipes_uris)

        recommended_recipes_uris = recommended_recipes_uris[:6]
        print(cbf_recommended_recipes)
        response = {
            'user_id': user_id,
            'recommended_recipes': [{'recipe_uri': uri} for uri in recommended_recipes_uris]
        }

        return jsonify(response), 200

    except Exception as ex:
        print(f"Error in get_recommendations: {str(ex)}")
        return jsonify({'error': str(ex)}), 500

if __name__ == '__main__':
    app.run(host="127.0.0.1", debug=True)
