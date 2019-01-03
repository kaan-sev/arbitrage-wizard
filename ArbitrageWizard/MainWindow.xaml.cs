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
            mbrDataGrid.ItemsSource = LoadCollectionData();  
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
        private void backOdds2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            doubleChecker(backOdds2, e);
        }
        private void backOutlay2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            doubleChecker(backOutlay2, e);
        }

        private void layOdds2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            doubleChecker(layOdds2, e);
        }
        private void weightingDoubleChecker(TextBox box, TextCompositionEventArgs e)
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
            else if (testVal > 1)
            {
                e.Handled = true;
            }
            else
            {
                weighting2.Text = (1 - testVal).ToString();
            }
        }
        private void weighting1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            weightingDoubleChecker(weighting1, e);
        }

        private void weighting1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Back) || (e.Key == Key.Delete))
            {
                double testVal;
                if (!Double.TryParse(weighting1.Text, out testVal))
                {
                    e.Handled = true;
                }
                else
                {
                    weighting2.Text = (1 - testVal).ToString();
                }
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            double parsedLayOdds;
            double parsedBackOutlay;
            double parsedBackOdds;
            double parsedDiscountRate = discountRate.Value;
            double parsedMBR = mbRate.Value;
            if (Double.TryParse(layOdds.Text, out parsedLayOdds) && Double.TryParse(backOdds.Text, out parsedBackOdds) && 
                Double.TryParse(backOutlay.Text, out parsedBackOutlay))
            {
                double commission = 0;
                if (isMBR.IsChecked ?? false) 
                {
                    if (isDiscountRate.IsChecked ?? false)
                    {
                        commission = ((100-parsedDiscountRate)*parsedMBR)/10000;
                    }
                    else
                    {
                        commission = parsedMBR/100;
                    }
                }
                double impliedOddsNum = (1 + (1 - commission)*(1 / ((parsedLayOdds - 1))));
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

        private void CalculateCustom_Click(object sender, RoutedEventArgs e)
        {
            double parsedLayOdds;
            double parsedBackOutlay;
            double parsedBackOdds;
            double parsedDiscountRate = discountRate.Value;
            double parsedMBR = mbRate.Value;
            double parsedBackWeighting;
            double parsedLayWeighting;
            if (Double.TryParse(layOdds2.Text, out parsedLayOdds) && Double.TryParse(backOdds2.Text, out parsedBackOdds) &&
                Double.TryParse(backOutlay2.Text, out parsedBackOutlay) && Double.TryParse(weighting1.Text, out parsedBackWeighting) &&
                Double.TryParse(weighting2.Text, out parsedLayWeighting))
            {
                double commission = 0;
                if (isMBR2.IsChecked ?? false)
                {
                    if (isDiscountRate2.IsChecked ?? false)
                    {
                        commission = ((100 - parsedDiscountRate) * parsedMBR) / 10000;
                    }
                    else
                    {
                        commission = parsedMBR / 100;
                    }
                }
                double impliedOddsNum = (1 + (1 - commission) * (1 / ((parsedLayOdds - 1))));
                double maxWin = (parsedBackOdds * parsedBackOutlay);
                double liabilityNum = maxWin * (1-parsedBackWeighting) / ((parsedBackWeighting)*(impliedOddsNum-2)+1);
                double backersStakeNum = maxWin - liabilityNum;
                double totalOutlayNum = liabilityNum + parsedBackOutlay;
                double profitBackNum = maxWin - totalOutlayNum;
                double profitLayNum = liabilityNum * impliedOddsNum - totalOutlayNum;
                if (isBonusBet2.IsChecked ?? false)
                {
                    maxWin = maxWin - parsedBackOutlay;
                    liabilityNum = maxWin * (1 - parsedBackWeighting) / ((parsedBackWeighting) * (impliedOddsNum - 2) + 1);
                    backersStakeNum = maxWin - liabilityNum;
                    totalOutlayNum = liabilityNum;
                    profitBackNum = maxWin - totalOutlayNum;
                    profitLayNum = liabilityNum * impliedOddsNum - totalOutlayNum;
                }
                impliedOdds2.Content = Math.Round(impliedOddsNum, 2);
                backersStake2.Content = Math.Round(backersStakeNum, 2);
                liability2.Content = Math.Round(liabilityNum, 2);
                totalOutlay2.Content = Math.Round(totalOutlayNum, 2);
                profitBack.Content = Math.Round(profitBackNum, 2);
                profitLay.Content = Math.Round(profitLayNum, 2);
            }
        }
        private List<Region> LoadCollectionData()
        {
            List<Region> regions = new List<Region>();
            regions.Add(new Region()
            {
                Location = "NSW",
                Thoroughbred = "10%",
                Harness = "8%",
                Greyhound = "6%",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "VIC",
                Thoroughbred = "6%",
                Harness = "6%",
                Greyhound = "7%",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "QLD",
                Thoroughbred = "6%",
                Harness = "6%",
                Greyhound = "6%",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "SA",
                Thoroughbred = "6%",
                Harness = "8%",
                Greyhound = "6%",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "WA",
                Thoroughbred = "8%",
                Harness = "8%",
                Greyhound = "8%",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "NT",
                Thoroughbred = "6%",
                Harness = "N/A",
                Greyhound = "6%",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "ACT",
                Thoroughbred = "7%",
                Harness = "8%",
                Greyhound = "N/A",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "NZL",
                Thoroughbred = "6%",
                Harness = "6%",
                Greyhound = "6%",
                Sports = "5%"
            });
            regions.Add(new Region()
            {
                Location = "INTL",
                Thoroughbred = "5%",
                Harness = "5%",
                Greyhound = "5%",
                Sports = "5%"
            });

            return regions;
        }  
    }
}
