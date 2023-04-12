using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyEat.Components
{
    public partial class Ingredient
    {
        public SolidColorBrush PriceColor
        {
            get
            {
                if(Cost <= 60)
                    return Brushes.LightGreen; 
                else if (Cost <= 200)
                    return Brushes.LightYellow;
                else
                    return Brushes.LightPink; 
            }
        }
    }
}
