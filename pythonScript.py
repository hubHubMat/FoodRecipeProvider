import pandas as pd
import sqlalchemy
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer
import time

start_time = time.time()

db_server = '(localdb)\\MSSQLLocalDB'
db_name = 'aspnet-FoodRecipeProvider-7d86217e-e193-4fe9-8574-ee04e3043999'
db_driver = 'ODBC+Driver+18+for+SQL+Server'

db_uri = f'mssql+pyodbc://{db_server}/{db_name}?driver={db_driver}'

try:
    engine = sqlalchemy.create_engine(db_uri)

    ingredients_query = "SELECT * FROM [dbo].[Ingredients]"
    ingredients_df = pd.read_sql_query(ingredients_query, engine)

    recipes_query = "SELECT * FROM [dbo].[Recipes]"
    recipes_df = pd.read_sql_query(recipes_query, engine)

    userreciperates_query = "SELECT * FROM [dbo].[UserRecipeRates]"
    userreciperates_df = pd.read_sql_query(userreciperates_query, engine)

    recipe_ingredients_query = "SELECT * FROM [dbo].[RecipeIngredients]"
    recipe_ingredients_df = pd.read_sql_query(recipe_ingredients_query, engine)

    merged_df = userreciperates_df.merge(recipes_df, left_on='AppRecipeId', right_on='Id')
    merged_df = merged_df.merge(recipe_ingredients_df, left_on='AppRecipeId', right_on='AppRecipeId')
    merged_df = merged_df.merge(ingredients_df, left_on='IngredientId', right_on='Id')

    recipe_features = merged_df.groupby('Label')['Name'].apply(lambda x: ' '.join(x)).reset_index()

    tfidf = TfidfVectorizer(stop_words='english')
    recipe_features_matrix = tfidf.fit_transform(recipe_features['Name'])

    item_similarity_matrix = cosine_similarity(recipe_features_matrix)

    def recommend_recipes(email, top_n=5):
        user_rated_recipes = merged_df[merged_df['AppUserId'] == user_id]['Label'].unique()

        recommended_recipes = {}

        for rated_recipe in user_rated_recipes:
            recipe_idx = recipe_features[recipe_features['Label'] == rated_recipe].index[0]
            similar_recipes = item_similarity_matrix[recipe_idx].argsort()[::-1][1:]  # Exclude self
            for sim_recipe_idx in similar_recipes:
                sim_recipe_label = recipe_features.iloc[sim_recipe_idx]['Label']
                if sim_recipe_label not in user_rated_recipes:
                    if sim_recipe_label not in recommended_recipes:
                        recommended_recipes[sim_recipe_label] = 0
                    recommended_recipes[sim_recipe_label] += item_similarity_matrix[recipe_idx, sim_recipe_idx]

        recommended_recipes = sorted(recommended_recipes.items(), key=lambda x: x[1], reverse=True)[:top_n]

        return recommended_recipes

    user_id = '26c1b77f-11ec-4f0b-b9fa-7fe2faf13901'
    recommended_recipes = recommend_recipes(user_id, top_n=5)

    print(f"Recommended recipes for user {user_id}:")
    if recommended_recipes:
        for recipe_label, score in recommended_recipes:
            print(f"Recipe Label: {recipe_label}, Recommendation Score: {score:.4f}")
    else:
        print("No recipes recommended. Check input data and interactions.")

    print(f"Elapsed time: {time.time() - start_time} seconds")
except Exception as ex:
    print(f"Error: {ex}")

finally:
    if 'engine' in locals() and engine is not None:
        engine.dispose()

print(f"Elapsed time: {time.time() - start_time} seconds")
