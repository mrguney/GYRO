using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public RectTransform ContentPanel;
    public GameObject DetailPanel; // Detay paneli
    public TMP_Text DetailNameText;
    public TMP_Text DetailCaloriesText;
    public TMP_Text DetailIngredientsText;
    public TMP_Text DetailInstructionsText;
    public TMP_Text DetailPreparationTimeText;
    public TMP_Text DetailCookingTimeText;

    public Sprite VeganIcon;
    public Sprite LowCalorieIcon;
    public Sprite HighProteinIcon;
    public Sprite GlutenFreeIcon;

    private List<Recipe> recipes = new List<Recipe>();

    void Start()
    {
        LoadRecipes();
        Destroy(ContentPanel.GetChild(0).gameObject); // İlk prefab'ı yok et
        PopulateRecipeList();
        DetailPanel.SetActive(false); // Detay panelini başlangıçta gizle
    }

    private void LoadRecipes()
    {
        recipes.Add(new Recipe("Avocado Oatmeal", 300, true, false, false, false,
            "Oats, Avocado, Honey, Almond milk",
            "1. Mix oats and almond milk.\n2. Mash avocado and add to the mixture.\n3. Sweeten with honey.",
            5, 5));

        recipes.Add(new Recipe("Egg White Omelette", 150, false, false, true, false,
            "Egg whites, Spinach, Mushrooms, Onion",
            "1. Beat egg whites.\n2. Add spinach, mushrooms, and onion.\n3. Cook in a pan.",
            10, 10));

        recipes.Add(new Recipe("Grilled Chicken Quinoa Salad", 400, false, true, false, false,
            "Grilled chicken, Quinoa, Kale, Cherry tomatoes, Olive oil",
            "1. Grill the chicken breast.\n2. Cook quinoa.\n3. Mix all ingredients.",
            15, 20));

        recipes.Add(new Recipe("Vegan Pancakes", 250, true, false, false, false,
            "Whole wheat flour, Almond milk, Banana, Baking powder",
            "1. Mix flour, almond milk, and mashed banana.\n2. Add baking powder.\n3. Cook on a non-stick pan.",
            10, 10));

        recipes.Add(new Recipe("Baked Salmon with Asparagus", 350, false, false, true, false,
            "Salmon fillet, Asparagus, Lemon, Olive oil",
            "1. Preheat oven to 200°C.\n2. Place salmon and asparagus on a baking sheet.\n3. Drizzle with olive oil and lemon juice.\n4. Bake for 20 minutes.",
            10, 20));

        recipes.Add(new Recipe("Quinoa and Black Bean Salad", 220, true, false, false, false,
            "Quinoa, Black beans, Corn, Red bell pepper, Cilantro",
            "1. Cook quinoa.\n2. Mix with black beans, corn, and chopped bell pepper.\n3. Garnish with cilantro.",
            15, 0));

        recipes.Add(new Recipe("Greek Yogurt Parfait", 180, false, true, false, false,
            "Greek yogurt, Mixed berries, Honey, Granola",
            "1. Layer Greek yogurt, berries, and granola in a glass.\n2. Drizzle with honey.",
            5, 0));

        recipes.Add(new Recipe("Lentil Soup", 300, false, false, false, true,
            "Lentils, Carrot, Onion, Garlic, Vegetable broth",
            "1. Sauté onion and garlic.\n2. Add carrots and lentils.\n3. Pour in vegetable broth and simmer for 25 minutes.",
            10, 25));

        recipes.Add(new Recipe("Chicken Caesar Salad", 450, false, false, true, false,
            "Grilled chicken, Romaine lettuce, Caesar dressing, Parmesan cheese, Croutons",
            "1. Chop romaine lettuce.\n2. Add grilled chicken, croutons, and Caesar dressing.\n3. Top with Parmesan cheese.",
            10, 0));

        recipes.Add(new Recipe("Spaghetti with Tomato Basil Sauce", 350, true, false, false, false,
            "Spaghetti, Tomatoes, Garlic, Basil, Olive oil",
            "1. Cook spaghetti.\n2. Sauté garlic and chopped tomatoes.\n3. Mix with spaghetti and garnish with basil.",
            10, 15));
    }

    private void PopulateRecipeList()
    {
        foreach (Recipe recipe in recipes)
        {
            GameObject newButton = Instantiate(ButtonPrefab, ContentPanel);
            TMP_Text nameText = newButton.transform.Find("Text_NickName").GetComponent<TMP_Text>();
            Image veganIcon = newButton.transform.Find("VeganIcon").GetComponent<Image>();
            Image lowCalorieIcon = newButton.transform.Find("LowCalorieIcon").GetComponent<Image>();
            Image highProteinIcon = newButton.transform.Find("HighProteinIcon").GetComponent<Image>();
            Image glutenFreeIcon = newButton.transform.Find("GlutenFreeIcon").GetComponent<Image>();

            nameText.text = recipe.Name;

            veganIcon.gameObject.SetActive(recipe.IsVegan);
            lowCalorieIcon.gameObject.SetActive(recipe.IsLowCalorie);
            highProteinIcon.gameObject.SetActive(recipe.IsHighProtein);
            glutenFreeIcon.gameObject.SetActive(recipe.IsGlutenFree);

            newButton.GetComponent<Button>().onClick.AddListener(() => ShowRecipeDetails(recipe));
        }
    }

    private void ShowRecipeDetails(Recipe recipe)
    {
        DetailPanel.SetActive(true);
        DetailNameText.text = recipe.Name;
        DetailCaloriesText.text = $"Calories: {recipe.Calories} kcal";
        DetailIngredientsText.text = $"Ingredients: {recipe.Ingredients}";
        DetailInstructionsText.text = $"Instructions:\n{recipe.Instructions}";
        DetailPreparationTimeText.text = $"Preparation Time: {recipe.PreparationTime} minutes";
        DetailCookingTimeText.text = $"Cooking Time: {recipe.CookingTime} minutes";
    }
}

[System.Serializable]
public class Recipe
{
    public string Name;
    public int Calories;
    public bool IsVegan;
    public bool IsLowCalorie;
    public bool IsHighProtein;
    public bool IsGlutenFree;
    public string Ingredients;
    public string Instructions;
    public int PreparationTime;
    public int CookingTime;

    public Recipe(string name, int calories, bool isVegan, bool isLowCalorie, bool isHighProtein, bool isGlutenFree, string ingredients, string instructions, int preparationTime, int cookingTime)
    {
        Name = name;
        Calories = calories;
        IsVegan = isVegan;
        IsLowCalorie = isLowCalorie;
        IsHighProtein = isHighProtein;
        IsGlutenFree = isGlutenFree;
        Ingredients = ingredients;
        Instructions = instructions;
        PreparationTime = preparationTime;
        CookingTime = cookingTime;
    }
}