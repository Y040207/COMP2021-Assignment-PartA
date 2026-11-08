# AI Acknowledgement

**Student:** Jinghui Yang (2518040207)

**Assignment:** Software Development Practice — Part A

## Tool used

WorkBuddy (AI coding assistant)

## Purpose of use

I used the tool as a tutoring aid in three specific ways:

1. To explain how the five nominated collections behave in C# — in particular
   `Dictionary.TryAdd` versus `Add`, `LinkedList.AddLast` and `Remove`, and the
   fact that `Peek`, `Pop` and `Dequeue` throw on empty collections.
2. To interpret compiler and test error messages when a build failed, and to
   understand why a test passed or failed.
3. To check the order of validation and side effects in the methods with more
   than one precondition, such as `StartCooking`, `RemoveRecipeFromCookingPlan`
   and `RestoreLastRemovedRecipe`.

## Topics discussed

- Choosing and using `Dictionary<int, Recipe>`, `List<string>`, `LinkedList<int>`,
  `Stack<int>` and `Queue<string>` for their nominated roles.
- Handling duplicate and missing recipe IDs safely.
- Handling an empty stack and an empty queue without throwing.
- Avoiding side effects when an operation fails (for example, not clearing the
  instruction queue before the recipe has been validated).
- Writing xUnit tests with `Assert.Equal`, `Assert.True`, `Assert.False`,
  `Assert.Null`, `Assert.Empty` and `Assert.Throws`.

## How I developed the submitted work

I implemented `RecipeManager.cs` myself, one method group at a time, and committed
after each feature so the repository history shows how the work progressed.
I typed the implementation, ran `dotnet build` and `dotnet test` after every
change, and fixed the errors I found. Where the tool explained a concept, I
re-read the explanation, closed it, and wrote the code from my own understanding
of the course material.

I referred to the tool's suggested test cases, but read each one and rewrote them
in my own naming and assertion order, so that each test name describes the
behaviour I was checking and each assertion is one I can explain.

## Declaration

I understand that there is a closed-book exam at the end of the semester and that
I am expected to explain and extend this code without assistance. I can explain
every method in `RecipeManager.cs` and every test in `RecipeManagerTests.cs`,
including why the validation order matters in each case.
