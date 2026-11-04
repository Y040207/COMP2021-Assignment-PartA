using System.Collections.Generic;
using RecipeManagement.Core;

namespace RecipeManagement.Tests;

/// <summary>
/// Example tests from the assignment specification. Add your own tests as you work.
/// </summary>
public sealed class RecipeManagerTests
{
    [Fact]
    public void Constructor_BuildsRecipeDictionary()
    {
        var manager = CreateManager();
        Assert.Equal(2, manager.RecipeCount);
        Assert.Equal("Recipe A", manager.FindRecipe(10)?.Title);
    }

    [Fact]
    public void InstructionsAreCompletedInFileOrder()
    {
        var manager = CreateManager();
        Assert.True(manager.StartCooking(10));
        Assert.Equal("First step", manager.PeekNextInstruction());
        Assert.Equal("First step", manager.CompleteNextInstruction());
        Assert.Equal("Second step", manager.PeekNextInstruction());
    }

    [Fact]
    public void RemovedRecipesAreRestoredLastInFirstOut()
    {
        var manager = CreateManager();
        manager.AddRecipeToCookingPlan(10);
        manager.AddRecipeToCookingPlan(20);
        manager.RemoveRecipeFromCookingPlan(10);
        manager.RemoveRecipeFromCookingPlan(20);
        Assert.Equal(20, manager.PeekLastRemovedRecipe());
        Assert.True(manager.RestoreLastRemovedRecipe());
        Assert.Equal(new[] { 20 }, manager.GetCookingPlan());
    }

 [Fact]
    public void ClearShoppingList_EmptiesTheList()
    {
        var manager = CreateManager();

        manager.AddIngredientsToShoppingList(10);
        manager.ClearShoppingList();

        Assert.Empty(manager.GetShoppingList());
    }
   [Fact]
    public void Constructor_ThrowsWhenRecipesIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new RecipeManager(null!));
    }

    [Fact]
    public void Constructor_ThrowsOnDuplicateIds()
    {
        var recipes = new[]
        {
            new Recipe { Id = 10, Title = "Recipe A" },
            new Recipe { Id = 10, Title = "Recipe B" }
        };

        Assert.Throws<ArgumentException>(() => new RecipeManager(recipes));
    }

    [Fact]
    public void Constructor_ThrowsOnNonPositiveId()
    {
        var recipes = new[] { new Recipe { Id = 0, Title = "Recipe A" } };

        Assert.Throws<ArgumentException>(() => new RecipeManager(recipes));
    }

    [Fact]
    public void Constructor_ThrowsOnBlankTitle()
    {
        var recipes = new[] { new Recipe { Id = 10, Title = "   " } };

        Assert.Throws<ArgumentException>(() => new RecipeManager(recipes));
    }

    [Fact]
    public void FindRecipe_ReturnsNullForMissingId()
    {
        var manager = CreateManager();

        Assert.Null(manager.FindRecipe(99));
    }

    [Fact]
    public void AddRecipe_ReturnsFalseForDuplicateId()
    {
        var manager = CreateManager();

        Assert.False(manager.AddRecipe(new Recipe { Id = 10, Title = "Recipe C" }));
        Assert.Equal(2, manager.RecipeCount);
    }

    [Fact]
    public void AddRecipe_ReturnsFalseForInvalidRecipe()
    {
        var manager = CreateManager();

        Assert.False(manager.AddRecipe(new Recipe { Id = 0, Title = "No id" }));
        Assert.False(manager.AddRecipe(new Recipe { Id = 30, Title = "   " }));
        Assert.Equal(2, manager.RecipeCount);
    }

    [Fact]
    public void RemoveRecipe_RemovesExistingRecipe()
    {
        var manager = CreateManager();

        Assert.True(manager.RemoveRecipe(20));

        Assert.Equal(1, manager.RecipeCount);
        Assert.Null(manager.FindRecipe(20));
    }

    [Fact]
    public void RemoveRecipe_ReturnsFalseForMissingId()
    {
        var manager = CreateManager();

        Assert.False(manager.RemoveRecipe(99));
        Assert.Equal(2, manager.RecipeCount);
    }

    [Fact]
    public void RemoveRecipe_ReturnsFalseWhenRecipeIsInCookingPlan()
    {
        var manager = CreateManager();
        manager.AddRecipeToCookingPlan(10);

        Assert.False(manager.RemoveRecipe(10));
        Assert.Equal(2, manager.RecipeCount);
    }

    [Fact]
    public void AddIngredientsToShoppingList_ReturnsZeroForMissingRecipe()
    {
        var manager = CreateManager();

        Assert.Equal(0, manager.AddIngredientsToShoppingList(99));
        Assert.Empty(manager.GetShoppingList());
    }

    [Fact]
    public void AddIngredientsToShoppingList_CountsOnlyNewItems()
    {
        var manager = CreateManager();

        Assert.Equal(1, manager.AddIngredientsToShoppingList(10));
        Assert.Equal(1, manager.AddIngredientsToShoppingList(10));
        Assert.Equal(2, manager.ShoppingItemCount);
    }
    private static RecipeManager CreateManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new() { "1 apple" },
                Instructions = new() { "First step", "Second step" }
            },
            new Recipe
            {
                Id = 20,
                Title = "Recipe B"
            }
        });
    }
 [Fact]
    public void AddIngredientsToShoppingList_CopiesIngredientsInOrder()
    {
        var manager = CreateManager();

        int added = manager.AddIngredientsToShoppingList(10);

        Assert.Equal(1, added);
        Assert.Equal(new[] { "1 apple" }, manager.GetShoppingList());
    }
}
