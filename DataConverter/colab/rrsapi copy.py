import pandas as pd
import sqlalchemy
import numpy as np
from sklearn.metrics import jaccard_score
db_server = '(localdb)\\MSSQLLocalDB'
db_name = 'aspnet-FoodRecipeProvider-7d86217e-e193-4fe9-8574-ee04e3043999'
db_driver = 'ODBC+Driver+18+for+SQL+Server'

db_uri = f'mssql+pyodbc://{db_server}/{db_name}?driver={db_driver}'
engine = sqlalchemy.create_engine(db_uri)

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


# Assuming user_id is defined
user_id = "740e3e43-19cd-4611-a894-4c82726c7aa7"
def cf_recipe_recommendation(user_id, top_n=5):
    users_df = load_cf_data()
    users_df = preprocess_user_attributes(users_df)

    user_attributes = users_df[users_df['AppUserId'] == user_id].iloc[0]
    users_df['Similarity'] = users_df.apply(lambda x: calculate_jaccard_similarity(user_attributes, x), axis=1)

    similar_users = users_df[users_df['AppUserId'] != user_id].sort_values(by='Similarity', ascending=False)
    top_similar_users = similar_users[['AppUserId', 'Similarity']].head(5)
    similar_users_filtered = top_similar_users[top_similar_users['Similarity'] >= 0.80]
    print(similar_users_filtered)

    if not similar_users_filtered.empty:
        selected_user_id = similar_users.iloc[np.random.randint(0, len(similar_users_filtered))]['AppUserId']

    userreciperates_query = "SELECT * FROM [dbo].[UserRecipeRates]"
    userreciperates_df = pd.read_sql_query(userreciperates_query, engine)

    rated_by_user = userreciperates_df[userreciperates_df['AppUserId'] == selected_user_id]

    userreciperates_query = f"SELECT TOP {top_n} r.Id, r.Uri FROM [dbo].[UserRecipeRates] ur INNER JOIN [dbo].[Recipes] r ON ur.AppRecipeId = r.Id WHERE ur.AppUserId = '{selected_user_id}' ORDER BY ur.Rate DESC"
    recommended_recipes = pd.read_sql_query(userreciperates_query, engine)['Uri'].tolist()
    
    print(recommended_recipes)
cf_recipe_recommendation(user_id)