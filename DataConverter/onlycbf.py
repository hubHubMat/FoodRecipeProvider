from flask import Flask, jsonify, request
import pandas as pd
import sqlalchemy
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer
import time

app = Flask(__name__)

# Connect to the database and preprocess data
def load_data():
    db_server = '(localdb)\\MSSQLLocalDB'
    db_name = 'aspnet-FoodRecipeProvider-7d86217e-e193-4fe9-8574-ee04e3043999'
    db_driver = 'ODBC+Driver+18+for+SQL+Server'
    
    db_uri = f'mssql+pyodbc://{db_server}/{db_name}?driver={db_driver}'
    
    engine = sqlalchemy.create_engine(db_uri)

    ingredients_query = "SELECT * FROM [dbo].[Ingredients]"
    recipes_query = "SELECT * FROM [dbo].[Recipes]"
    userreciperates_query = "SELECT * FROM [dbo].[UserRecipeRates]"
    recipe_ingredients_query = "SELECT * FROM [dbo].[RecipeIngredients]"

    ingredients_df = pd.read_sql_query(ingredients_query, engine)
    recipes_df = pd.read_sql_query(recipes_query, engine)
    userreciperates_df = pd.read_sql_query(userreciperates_query, engine)
    recipe_ingredients_df = pd.read_sql_query(recipe_ingredients_query, engine)

    # Merge data
    merged_df = userreciperates_df.merge(recipes_df, left_on='AppRecipeId', right_on='Id')
    merged_df = merged_df.merge(recipe_ingredients_df, left_on='AppRecipeId', right_on='AppRecipeId')
    merged_df = merged_df.merge(ingredients_df, left_on='IngredientId', right_on='Id')

    # Prepare recipe features for TF-IDF
    recipe_features = merged_df.groupby('Uri')['Name'].apply(lambda x: ' '.join(x)).reset_index()

    # Create TF-IDF matrix
    tfidf = TfidfVectorizer(stop_words='english')
    recipe_features_matrix = tfidf.fit_transform(recipe_features['Name'])

    # Compute item similarity matrix using cosine similarity
    item_similarity_matrix = cosine_similarity(recipe_features_matrix)

    return merged_df, recipe_features, item_similarity_matrix, tfidf

# Load data once at application startup
merged_df, recipe_features, item_similarity_matrix, tfidf = load_data()

# Define recommendation function
def recommend_recipes(user_id, top_n=5):
    try:
        user_rated_recipes = merged_df[merged_df['AppUserId'] == user_id]['Uri'].unique()

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


# Define Flask route for recommending recipes
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

        recommended_recipes = recommend_recipes(user_id, top_n)
        
        response = {
            'user_id': user_id,
            'recommended_recipes': [
                {'recipe_uri': recipe_uri, 'score': score}
                for recipe_uri, score in recommended_recipes
            ]
        }

        print(response)

        return jsonify(response), 200

    except Exception as ex:
        return jsonify({'error': str(ex)}), 500

if __name__ == '__main__':
    app.run(host="0.0.0.0")