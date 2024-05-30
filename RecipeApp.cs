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
                Console.WriteLine("Enter 1 to add a new recipe or 2 to display recipes:");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
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
                else if (choice == 2)
                {
                    DisplayAllRecipes();
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
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                }

                Console.WriteLine("\nDo you want to continue? Press 1 to continue, any other key to exit.");
                string response = Console.ReadLine();
                stay = response.Equals("1");
            }
        }

        private void DisplayAllRecipes()
        {
            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }
        }

        private void NotifyCalorieLimitExceeded(string message)
        {
            Console.WriteLine(message);
        }
    }
}
