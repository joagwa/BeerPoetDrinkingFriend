using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrinkingFriend
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<string> drinkList = new ObservableCollection<string>();
        List<TimeSpan> drinkTimes = new List<TimeSpan>();

        double drinkTotal = 0;

        public MainPage()
        {
            InitializeComponent();
            DrinksList.ItemsSource = drinkList;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            drinkTotal = 0;
            drinkList.Add(AlcoholContentEntry.Text);
            drinkTimes.Add(DrinkTimeDatePicker.Time);
            DoCalulate();

        }

        private double CalculateBAC(double standardDrinks, double drinkingPeriod)
        {
            double bloodAlcoholContent = 
                (0.806 * standardDrinks * 1.2) 
                / (0.58 * 80) 
                - (0.015 * drinkingPeriod);
            var result = Math.Round(bloodAlcoholContent, 4);
            return result;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DoCalulate();
        }

        private void DoCalulate()
        {
            foreach (var drink in drinkList)
            {
                var x = double.Parse(drink);
                drinkTotal += x;
            }
            var drinkingPeriod = drinkTimes.Max() - drinkTimes.Min();
            var bAC = CalculateBAC(drinkTotal, drinkingPeriod.TotalHours);
            DrunkLevelLabel.Text = $"You are currently at {bAC}% BAC";
        }
    }
}
