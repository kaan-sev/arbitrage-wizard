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

namespace ArbitrageWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private void doubleChecker(TextBox box, TextCompositionEventArgs e)
        {
            int cursorPos = box.CaretIndex;
            string nextText;
            if (cursorPos > 0)
            {
                nextText = box.Text.Substring(0, cursorPos) + e.Text + box.Text.Substring(cursorPos);
            }
            else
            {
                nextText = box.Text + e.Text;
            }
            double testVal;
            if (!Double.TryParse(nextText, out testVal))
            {
                e.Handled = true;
            }
        }
        private void backOdds_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            doubleChecker(backOdds, e);
        }
        private void backOutlay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            doubleChecker(backOutlay, e);
        }

        private void layOdds_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            doubleChecker(layOdds, e);
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            double parsedLayOdds;
            double parsedBackOutlay;
            double parsedBackOdds;
            if (Double.TryParse(layOdds.Text, out parsedLayOdds) && Double.TryParse(backOdds.Text, out parsedBackOdds) && Double.TryParse(backOutlay.Text, out parsedBackOutlay))
            {
                double impliedOddsNum = (1 + (1 / (parsedLayOdds - 1)));
                double maxWin = (parsedBackOdds * parsedBackOutlay);
                double liabilityNum = maxWin / impliedOddsNum;
                double backersStakeNum = maxWin - liabilityNum;
                double totalOutlayNum = liabilityNum + parsedBackOutlay;
                double maxProfit = maxWin - totalOutlayNum;
                if (isBonusBet.IsChecked ?? false)
                {
                    maxWin = maxWin - parsedBackOutlay;
                    liabilityNum = maxWin / impliedOddsNum;
                    backersStakeNum = maxWin - liabilityNum;
                    totalOutlayNum = liabilityNum;
                    maxProfit = maxWin - totalOutlayNum;
                }
                impliedOdds.Content = Math.Round(impliedOddsNum, 2);
                backersStake.Content = Math.Round(backersStakeNum, 2);
                liability.Content = Math.Round(liabilityNum, 2);
                totalOutlay.Content = Math.Round(totalOutlayNum, 2);
                profit.Content = Math.Round(maxProfit, 2);
            }
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {

        }
        private void quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void help_Click(object sender, RoutedEventArgs e)
        {

        }
        private void commissionRates_Click(object sender, RoutedEventArgs e)
        {

        }
        private void discountRate_Click(object sender, RoutedEventArgs e)
        {

        }
        private void advSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
