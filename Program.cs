using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poe_part_2
{
    class Program
    {
        private static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Recipe App");
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. List Recipes");
                Console.WriteLine("3. View Recipe");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        ListRecipes();
                        break;
                    case "3":
                        ViewRecipe();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private static void AddRecipe()
        {
            Console.Write("Enter the recipe name: ");
            string recipeName = Console.ReadLine();

            Recipe recipe = new Recipe(recipeName);
            recipe.OnCaloriesExceeded += NotifyCaloriesExceeded;

            Console.WriteLine("Enter ingredients (name, quantity, unit, calories, food group). Type 'done' to finish:");
            while (true)
            {
                Console.Write("Ingredient name (or 'done'): ");
                string name = Console.ReadLine();
                if (name.ToLower() == "done") break;

                Console.Write("Quantity: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write("Unit: ");
                string unit = Console.ReadLine();

                Console.Write("Calories: ");
                double calories = double.Parse(Console.ReadLine());

                Console.Write("Food group: ");
                string foodGroup = Console.ReadLine();

                recipe.AddIngredient(name, quantity, unit, calories, foodGroup);
            }

            Console.WriteLine("Enter steps. Type 'done' to finish:");
            while (true)
            {
                Console.Write("Step description (or 'done'): ");
                string description = Console.ReadLine();
                if (description.ToLower() == "done") break;

                recipe.AddStep(description);
            }

            recipes.Add(recipe);
        }

        private static void ListRecipes()
        {
            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }
        }

        private static void ViewRecipe()
        {
            Console.Write("Enter the recipe name to view: ");
            string recipeName = Console.ReadLine();

            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Name}, {ingredient.Quantity} {ingredient.Unit}, {ingredient.Calories} calories, {ingredient.FoodGroup}");
            }

            Console.WriteLine("Steps:");
            foreach (var step in recipe.Steps)
            {
                Console.WriteLine(step.Description);
            }

            double totalCalories = recipe.CalculateTotalCalories();
            Console.WriteLine($"Total Calories: {totalCalories}");
        }

        private static void NotifyCaloriesExceeded(string recipeName, double totalCalories)
        {
            Console.WriteLine($"Warning: The recipe '{recipeName}' exceeds 300 calories. Total calories: {totalCalories}");
        }
    }
}
