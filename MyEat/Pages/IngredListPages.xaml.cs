using MyEat.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace MyEat.Pages
{
    /// <summary>
    /// Логика взаимодействия для IngredListPages.xaml
    /// </summary>
    public partial class IngredListPages : Page
    {
        int MaxPage = 0;
        int currentPage = 1;

        List<List<Ingredient>> ingredients = new List<List<Ingredient>>();

        public IngredListPages()
        {
            InitializeComponent();
            
            var ingredientBuffer = App.DB.Ingredient.ToList();
            //CsvGrid.ItemsSource = ingredientBuffer.Where(x => x.Id <= sours).ToList();
            int ingredientCount = 0;
            do
            {
                ingredientCount += 7;
                MaxPage++;
            } while (ingredientCount < ingredientBuffer.Count());
            ListTb.Text = $"{currentPage}/{MaxPage}";

            int start = 1;
            for (int i = 1; i <= MaxPage; i++)
            {
                List<Ingredient> buffer = new List<Ingredient>();

                for (; start <= 7 * i; start++)
                {
                    if (start <= ingredientBuffer.Count)
                    {
                        buffer.Add(ingredientBuffer[start - 1]);
                    }
                    
                }

                ingredients.Add(buffer);
            }

            CsvGrid.ItemsSource = ingredients[currentPage - 1];


            TBQuantity.Text = $"{ingredientBuffer.ToList().Count().ToString()} наименований";
            TBQuantity.Text = $"{ingredientBuffer.ToList().Count().ToString()} наименований";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == BNext)
            {
                if (currentPage == MaxPage)
                    currentPage = 1;
                else currentPage++;
            }
            else if (sender == BBack)
            {
                if (currentPage == 1)
                    currentPage = MaxPage;
                else currentPage--;
            }
            else if (sender == BFullNext)
            {
                if (currentPage == MaxPage)
                    currentPage = 1;
                else currentPage = MaxPage;
            }
            else
            {
                if (currentPage == 1)
                    currentPage = MaxPage;
                else currentPage = 1;
            }

            ListTb.Text = $"{currentPage}/{MaxPage}";
            CsvGrid.ItemsSource = ingredients[currentPage - 1];

        }


        private void LinkEdIt_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Hyperlink).DataContext as Ingredient;
            NavigationService.Navigate(new AddEditPage(select));
        }

        private void LinkDelete_Click(object sender, RoutedEventArgs e)
        {

        }


       
    }

}





