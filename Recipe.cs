using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poe_part_2
{
    public class Recipe
    {
        public string Name { get; private set; }
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<string> stepDescriptions = new List<string>();

        public void GetRecipeDetails()
        {
            Console.WriteLine("Please enter the name of the recipe:");
            Name = Console.ReadLine();

            GetIngredients();
            GetSteps();
        }

        public void GetIngredients()
        {
            Console.WriteLine("Please enter the number of ingredients:");
            int ingredientCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < ingredientCount; i++)
            {
                Console.WriteLine($"Enter details for ingredient {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Quantity: ");
                double quantity = double.Parse(Console.ReadLine());
                Console.Write("Unit: ");
                string unit = Console.ReadLine();
                Console.Write("Calories: ");
                double calories = double.Parse(Console.ReadLine());
                Console.Write("Food Group: ");
                string foodGroup = Console.ReadLine();

                ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            }
        }

        public void GetSteps()
        {
            Console.WriteLine("Enter the number of steps:");
            int stepCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < stepCount; i++)
            {
                Console.WriteLine($"Enter description for step {i + 1}:");
                stepDescriptions.Add(Console.ReadLine());
            }
        }

        public void DisplayRecipe()
        {
            Console.WriteLine($"\nRecipe: {Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < stepDescriptions.Count; i++)
            {
                Console.WriteLine($"- {i + 1}. {stepDescriptions[i]}");
            }
            Console.WriteLine($"\nTotal Calories: {CalculateTotalCalories()}");
        }

        public double CalculateTotalCalories()
        {
            return ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity);
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.ResetQuantity();
            }
        }

        public void ClearData()
        {
            ingredients.Clear();
            stepDescriptions.Clear();
        }
    }
}