using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var errormessage = "";
            if (NameTb.Text.Trim() == "")
            {
                errormessage += "Заполните название \n";
            }
            if (CostTb.Text.Trim() == "")
            {
                errormessage += "Заполните цену \n";
            }
            if (CostForCountTb.Text.Trim() == "")
            {
                errormessage += "Заполните цена за количество \n";
            }
            if (CBQuant.SelectedIndex < 0)
            {
                errormessage += "Выберите одно измерение \n";
            }
            if (AvailableCountTb.Text.Trim() == "")
            {
                errormessage += "Заполните количество в холодильнике \n";
            }

            if (errormessage == "")
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
                MessageBox.Show(errormessage);
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IngredListPages());
        }

        private void CostTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (Regex.IsMatch(e.Text, @"[0-9.]") == false)
                e.Handled = true;
            if (e.Text == "." && textBox.Text.Contains('.'))
                e.Handled = true;
            if (textBox.Text == "" && e.Text == "." && (textBox.Text.Last() != '.' || textBox.Text[textBox.Text.Length - 1] != '.'))
                e.Handled = true;
            if (textBox.Text.Length >= 4)
            {
                if (textBox.Text[textBox.Text.Length - 3] == '.')
                    e.Handled = true;
            }


            textBox.CaretIndex = textBox.Text.Length;
        }

        private void CostForCountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
                e.Handled = true;
        }

        private void CostTb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
