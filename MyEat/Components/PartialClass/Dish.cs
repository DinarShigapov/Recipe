using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEat.Components
{
    public partial class Dish
    {
        public double ServingPrice
        {
            get
            {
                var allIngredints = CookingStage.SelectMany(x => x.IngredientOfStage).ToList();
                double totalSum = allIngredints.Sum(x => x.Quantity * (double)x.Ingredient.Price);
                double price = totalSum / ServingQuantity;
                price = price - price % 0.1;
                return price;
            }
        }
    }
}
