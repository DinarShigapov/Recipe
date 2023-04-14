using MyEat.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MainListPages.xaml
    /// </summary>
    public partial class MainListPages : Page
    {
      
        public MainListPages()
        {
            InitializeComponent();
            var prod = App.DB.Category.ToList();
            prod.Insert(0, new Category
            {
                Name = "Все"
            });
            CategoriCb.ItemsSource = prod;
            CategoriCb.SelectedIndex = 0;
            LVprod.ItemsSource = App.DB.Dish.ToList() ;
            Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void NameCB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
        
      
        public void Refresh()
        {

          
            var filterProduct = App.DB.Dish.ToList();
            if (CategoriCb.SelectedIndex > 0)
            {
                var a = CategoriCb.SelectedIndex.ToString();
                filterProduct = filterProduct.Where(x => x.CategoryId.ToString().Contains(a.ToLower())).ToList(); 
            }
            if (NameCB.Text.Length > 0)
            {
                filterProduct = filterProduct.Where(x => x.Name.ToLower().StartsWith(NameCB.Text.ToLower())).ToList();
            }
            LVprod.ItemsSource = filterProduct.ToList();
            CBShow.IsChecked = false;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var dish = App.DB.Dish.ToList();
            List<Dish> list = new List<Dish>();


            foreach (var item in dish)
            {
                bool isCheck = true;
                var cookingStage = App.DB.CookingStage.Where(x => x.DishId == item.Id);

                foreach (var item2 in cookingStage)
                {
                    var ios = App.DB.IngredientOfStage.Where(x => x.CookingStageId == item2.Id).ToList();

                    foreach (var item3 in ios)
                    {
                        if (App.DB.Ingredient.FirstOrDefault(x => x.Id == item3.IngredientId).AvailableCount < item3.Quantity)
                        {
                            isCheck = false; break;
                        };
                    }

                }
                if (isCheck) { list.Add(item); }

            }
            CategoriCb.SelectedIndex = 0;
            LVprod.ItemsSource = list;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ingr = (sender as Border).DataContext as Dish;

            NavigationService.Navigate(new ReceptPage(ingr));

        }
    }


}

