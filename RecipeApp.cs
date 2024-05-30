using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poe_part_2
{
    class RecipeApp
    {
        private List<Recipe> recipes = new List<Recipe>();
        public delegate void CalorieNotification(string message);

        public void Run()
        {
            bool stay = true;
            while (stay)
            {
                Console.WriteLine("Hi, welcome to our storing ingredient app");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. Display recipes");
                Console.WriteLine("3. Scale recipe");
                Console.WriteLine("4. Reset quantities");
                Console.WriteLine("5. Clear recipe data");
                Console.WriteLine("6. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddNewRecipe();
                        break;
                    case 2:
                        DisplayRecipes();
                        break;
                    case 3:
                        ScaleRecipe();
                        break;
                    case 4:
                        ResetQuantities();
                        break;
                    case 5:
                        ClearRecipeData();
                        break;
                    case 6:
                        stay = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }

        private void AddNewRecipe()
        {
            Recipe recipe = new Recipe();
            recipe.GetRecipeDetails();
            recipes.Add(recipe);

            if (recipe.CalculateTotalCalories() > 300)
            {
                CalorieNotification notify = NotifyCalorieLimitExceeded;
                notify($"The total calories of the recipe '{recipe.Name}' exceed 300!");
            }
        }

        private void DisplayRecipes()
        {
            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }

            Console.WriteLine("Enter the name of the recipe you want to display:");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (selectedRecipe != null)
            {
                selectedRecipe.DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        private void ScaleRecipe()
        {
            Console.WriteLine("Enter the name of the recipe you want to scale:");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (selectedRecipe != null)
            {
                Console.WriteLine("By how much would you like your quantities to be changed?:");
                double factor = double.Parse(Console.ReadLine());
                selectedRecipe.ScaleRecipe(factor);
                Console.WriteLine("The quantities have been updated. Here are the new quantities:");
                selectedRecipe.DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        private void ResetQuantities()
        {
            Console.WriteLine("Enter the name of the recipe you want to reset:");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (selectedRecipe != null)
            {
                selectedRecipe.ResetQuantities();
                Console.WriteLine("The quantity values have been reset.");
                selectedRecipe.DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        private void ClearRecipeData()
        {
            Console.WriteLine("Enter the name of the recipe you want to clear:");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (selectedRecipe != null)
            {
                selectedRecipe.ClearData();
                recipes.Remove(selectedRecipe);
                Console.WriteLine("The recipe data has been cleared.");
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        private void NotifyCalorieLimitExceeded(string message)
        {
            Console.WriteLine(message);
        }
    }
}