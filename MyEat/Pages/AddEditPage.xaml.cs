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
using MyEat.Components;

namespace MyEat.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        Ingredient contextingredient;
        public AddEditPage(Ingredient ingredient)
        {
            InitializeComponent();
            CBQuant.ItemsSource = App.DB.Unit.ToList();
            contextingredient = ingredient;
            DataContext = contextingredient;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var erormasage = "";
            if (NameTb.Text.Trim() == "")
            {
                erormasage += "Заполните название \n";
            }
            if (CostTb.Text.Trim() == "")
            {
                erormasage += "Заполните цену \n";
            }
            if (CostForCountTb.Text.Trim() == "")
            {
                erormasage += "Заполните цена за количество \n";
            }
            if (CBQuant.SelectedIndex < 0)
            {
                erormasage += "Выберете идин измерения \n";
            }
            if (AvailableCountTb.Text.Trim() == "")
            {
                erormasage += "Заполните количество в холодильнике \n";
            }
            if (erormasage == "")
            {
                if (contextingredient.Id == 0)
                {
                    App.DB.Ingredient.Add(contextingredient);
                }

                App.DB.SaveChanges();

                NavigationService.Navigate(new IngredListPages());


            }
            else
            {
                MessageBox.Show(erormasage);
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IngredListPages());
        }
    }
}
