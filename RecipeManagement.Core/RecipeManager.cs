using System;
using System.Collections.Generic;

namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>
public sealed class RecipeManager : IRecipeManager
{
    // TODO Part A: add your private collection fields here.
    private readonly Dictionary<int, Recipe> _recipes = new();
    private readonly List<string> _shoppingList = new();
    private readonly LinkedList<int> _cookingPlan = new();
    private readonly Stack<int> _removedRecipes = new();
    private readonly Queue<string> _instructionQueue = new();

    public RecipeManager(IEnumerable<Recipe> recipes)
    {
        // TODO Part A: validate recipes and build Dictionary<int, Recipe>.
        ArgumentNullException.ThrowIfNull(recipes);

        foreach (Recipe recipe in recipes)
        {
            if (recipe.Id <= 0)
            {
                throw new ArgumentException("Recipe ID must be positive.", nameof(recipes));
            }

            if (string.IsNullOrWhiteSpace(recipe.Title))
            {
                throw new ArgumentException("Recipe title must not be blank.", nameof(recipes));
            }

            if (_recipes.ContainsKey(recipe.Id))
            {
                throw new ArgumentException($"Duplicate recipe ID: {recipe.Id}", nameof(recipes));
            }

            _recipes.Add(recipe.Id, recipe);
        }
    }

    public int RecipeCount => _recipes.Count;
    public int ShoppingItemCount => _shoppingList.Count;
    public int CookingPlanCount => _cookingPlan.Count;
    public int PendingInstructionCount => _instructionQueue.Count;
    public int RemovedRecipeCount => _removedRecipes.Count;

    public bool AddRecipe(Recipe recipe)
    {
        ArgumentNullException.ThrowIfNull(recipe);

        if (recipe.Id <= 0 || string.IsNullOrWhiteSpace(recipe.Title))
        {
            return false;
        }

        return _recipes.TryAdd(recipe.Id, recipe);
    }

    public Recipe? FindRecipe(int recipeId) => _recipes.GetValueOrDefault(recipeId);

    public bool RemoveRecipe(int recipeId)
    {
        if (!_recipes.ContainsKey(recipeId))
        {
            return false;
        }

        if (_cookingPlan.Find(recipeId) is not null)
        {
            return false;
        }

        return _recipes.Remove(recipeId);
    }

    public int AddIngredientsToShoppingList(int recipeId)
    {
        if (!_recipes.TryGetValue(recipeId, out Recipe? recipe))
        {
            return 0;
        }

        int before = _shoppingList.Count;

        foreach (string ingredient in recipe.Ingredients)
        {
            _shoppingList.Add(ingredient);
        }

        return _shoppingList.Count - before;
    }

    public IReadOnlyList<string> GetShoppingList() => _shoppingList.AsReadOnly();

    public void ClearShoppingList() => _shoppingList.Clear();

    public bool AddRecipeToCookingPlan(int recipeId)
    {
        if (!_recipes.ContainsKey(recipeId))
        {
            return false;
        }

        if (_cookingPlan.Contains(recipeId))
        {
            return false;
        }

        _cookingPlan.AddLast(recipeId);
        return true;
    }

    public bool RemoveRecipeFromCookingPlan(int recipeId)
    {
        if (!_cookingPlan.Remove(recipeId))
        {
            return false;
        }

        _removedRecipes.Push(recipeId);
        return true;
    }

    public bool RestoreLastRemovedRecipe()
    {
        if (_removedRecipes.Count == 0)
        {
            return false;
        }

        int recipeId = _removedRecipes.Pop();

        if (!_recipes.ContainsKey(recipeId) || _cookingPlan.Contains(recipeId))
        {
            return false;
        }

        _cookingPlan.AddLast(recipeId);
        return true;
    }

    public int? PeekLastRemovedRecipe()
    {
        if (_removedRecipes.Count == 0)
        {
            return null;
        }

        return _removedRecipes.Peek();
    }

    public IReadOnlyList<int> GetCookingPlan() => new List<int>(_cookingPlan).AsReadOnly();

    public bool StartCooking(int recipeId) =>
        throw new NotImplementedException("Part A: implement StartCooking.");

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}
