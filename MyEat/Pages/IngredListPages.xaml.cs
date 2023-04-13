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
            
            Refresh();
            GenButton();

        }

        private void Refresh()
        {
            ingredients = new List<List<Ingredient>>();
            MaxPage = 0;
            var ingredientBuffer = App.DB.Ingredient.Where(x => x.IsDelete == false).ToList();
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

            double stocks = 0;

            foreach (var item in ingredientBuffer)
            {
                stocks += (double)item.Cost * item.CostForCount;
            }


            TBQuantity.Text = $"{ingredientBuffer.ToList().Count()} наименований";
            double sum = ingredientBuffer.Sum(x => x.Price * x.AvailableCount);
            TBStocks.Text = $"Запасов в холодильнике на сумму (руб.): {sum:N2} руб.";
            ListTb.Text = $"{currentPage}/{MaxPage}";
            GenButton();
        }

 
        private void GenButton() 
        {
            SPButton.Children.Clear();
            for (int i = 1; i <= MaxPage; i++)
            {
                Button myBtn = new Button();
                myBtn.Content = i;
                myBtn.Margin = new Thickness(5, 0, 0, 0);
                myBtn.Width = 30;
                myBtn.Click += MyBtn_Click;
                SPButton.Children.Add(myBtn);
            }
        }

        private void MyBtn_Click(Object sender, EventArgs e)
        {
            var ds = sender as Button;

            ListTb.Text = $"{ds.Content.ToString()}/{MaxPage}";
            CsvGrid.ItemsSource = ingredients[int.Parse(ds.Content.ToString()) - 1];
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
            GenButton();
        }


        private void LinkEdIt_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Hyperlink).DataContext as Ingredient;
            if (select == null)
            {
                MessageBox.Show("Выберите ингредиент");
                return;
            }

            NavigationService.Navigate(new AddEditPage(select));
        }

        private void LinkDelete_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Hyperlink).DataContext as Ingredient;
            if (select == null)
            {
                MessageBox.Show("Выберите ингредиент");
                return;
            }
            select.IsDelete = true;
            App.DB.SaveChanges();
            Refresh();

        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddEditPage(new Ingredient()));
        }
    }

}





