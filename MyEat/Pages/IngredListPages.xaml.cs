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

namespace MyEat.Pages
{
    /// <summary>
    /// Логика взаимодействия для IngredListPages.xaml
    /// </summary>
    public partial class IngredListPages : Page
    {
        int sours = 5;
        int List = 1;
        public IngredListPages()
        {
            InitializeComponent();
            ListTb.Text = "1/4";
            var ingredientBuffer = App.DB.Ingredient;
            CsvGrid.ItemsSource = ingredientBuffer.Where(x => x.Id <= sours).ToList();
            TBQuantity.Text = $"{ingredientBuffer.ToList().Count().ToString()} наименований";
            TBQuantity.Text = $"{ingredientBuffer.ToList().Count().ToString()} наименований";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (List > 1)
            {
                List -= 1;
            }
        
            if (List == 1)
            {
                ListTb.Text = "1/4";
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 5).ToList();

            }
            else if (List == 2)
            {

                
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 11 && x.Id > 5).ToList();
                ListTb.Text = "2/4";
            }
            else if (List == 3)
            {
        
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 17 && x.Id > 11).ToList();
                ListTb.Text = "3/4";
            }
            else if (List == 4)
            {
              
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 22 && x.Id > 17).ToList();
                ListTb.Text = "4/4";

            }
            //if(sours-5 > 0)
            //{
            //    sours = sours - 5;
            //    CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= sours && x.Id > sours - 5).ToList();
            //    List -= 1;
            //    ListTb.Text = List.ToString();
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (List < 4)
            {
                List += 1;
            }
            if (List == 1)
            {
                
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 5).ToList();
                ListTb.Text = "1/4";
            }
            
           else if (List == 2)
            {
                
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 11 && x.Id > 5).ToList();
                ListTb.Text = "2/4";
            }
            else if(List == 3)
            {
                
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 17 && x.Id > 11).ToList();
                ListTb.Text = "3/4";
            }
            else if (List == 4)
            {
    
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 22 && x.Id > 17).ToList();
                ListTb.Text = "4/4";
            }
            //if (sours +5 < 26)
            //{
            //    sours = sours + 5;
            //    CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= sours && x.Id > sours-5).ToList();

            //    List += 1;
            //    ListTb.Text = List.ToString();
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
         
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 5).ToList();
            ListTb.Text = "1/4";
            List = 1;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {   
                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 11 && x.Id > 5).ToList();
            ListTb.Text = "2/4";
            List = 2;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 17 && x.Id > 11).ToList();
            ListTb.Text = "3/4";
            List = 3;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

                CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 22 && x.Id > 17).ToList();
            ListTb.Text = "4/4";
            List = 4;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
           
            CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 22 && x.Id > 17).ToList();
            ListTb.Text = "4/4";
            List = 4;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            CsvGrid.ItemsSource = App.DB.Ingredient.Where(x => x.Id <= 5).ToList();
            ListTb.Text = "1/4";
            List = 1;
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


