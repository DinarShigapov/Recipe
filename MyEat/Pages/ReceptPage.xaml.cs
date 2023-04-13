﻿using System;
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
    /// Логика взаимодействия для ReceptPage.xaml
    /// </summary>
    public partial class ReceptPage : Page
    {
        int Quan = 1;

        Dish contextDish;

        public ReceptPage(Dish dish)
        {
            InitializeComponent();
            contextDish = dish;
            TBCategory.DataContext = dish;
            TBQuantity.Text = $"{Quan}";
            TBDish.DataContext = dish;


            LVDescription.ItemsSource = App.DB.CookingStage.Where(x => x.DishId == dish.Id).ToList();

            List<Ingredient> ingredientList = new List<Ingredient>();
            List<CookingStage> cooking = new List<CookingStage>();
            var cookingStages = App.DB.CookingStage.Where(x => x.DishId == dish.Id).ToList();

            foreach (var item in cookingStages)
            {
                var ingredientOfStages = App.DB.IngredientOfStage.Where(x => x.CookingStageId == item.Id).ToList();
                cooking.Add(item);
                foreach (var item2 in ingredientOfStages)
                {
                    var ingredient = App.DB.Ingredient.FirstOrDefault(x => x.Id == item2.IngredientId);
                    ingredientList.Add(ingredient);
                }
            }
            CsvGrid.ItemsSource = ingredientList;


            TBCookingTime.Text = $"Время на приготовление: {(int)cooking.Sum(x => x.TimeInMinutes)} мин.";
           
            TBGeneralSum.Text = $"Запасов в холодильнике на сумму (руб.): {dish.ServingPrice * int.Parse(TBQuantity.Text)} руб.";

        }

        private void LVDescription_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == BPlus)
            {
                Quan++;
                TBQuantity.Text = $"{Quan}";
            }
            else 
            {
                if (Quan != 1)
                {
                    Quan--;
                    TBQuantity.Text = $"{Quan}";
                }

            }
            TBGeneralSum.Text = $"Запасов в холодильнике на сумму (руб.): {contextDish.ServingPrice * int.Parse(TBQuantity.Text)} руб.";

        }
    }
}