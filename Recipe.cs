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
        public List<Ingredient> Ingredient { get; private set; }
        public List<Steps> Steps { get; private set; }

        public delegate void CaloriesExceededHandler(string recipeName, double totalCalories);
        public event CaloriesExceededHandler OnCaloriesExceeded;

        public Recipe(string name)
        {
            Name = name;
            Ingredient = new List<Ingredient>();
            Steps = new List<Steps>();
        }

        public void AddIngredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Ingredient ingredient = new Ingredient
            {
                Name = name,
                Quantity = quantity,
                Unit = unit,
                Calories = calories,
                FoodGroup = foodGroup
            };
            Ingredient.Add(ingredient);

            double totalCalories = CalculateTotalCalories();
            if (totalCalories > 300)
            {
                OnCaloriesExceeded?.Invoke(Name, totalCalories);
            }
        }

        public void AddStep(string description)
        {
            Steps step = new Steps { Description = description };
            Steps.Add(step);
        }

        public double CalculateTotalCalories()
        {
            return Ingredient.Sum(ingredient => ingredient.Calories);
        }
    }
}
