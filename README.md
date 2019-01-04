# Arbitrage Wizard

The purpose of this application is to calculate profit when potential arbitrage positions occur between two bookmakers. Primarily this will be a bookmaker for the backing side and Betfair for the laying side. 

Coded in C#

## How to use

The 'Equal Profit' tab requires the user to input back odds, what the outlay is on the backing side, and the lay odds. Based on these factors it will calculate what the required outlay is on the lay side and what the profit (or loss) will be.

Users can factor in the market base rate (Betfair commission), the data for which is available on the Market Base Rate Lookup Table tab.

The 'Custom Profit' tab is the same as the 'Equal Profit' tab but the user is able to select weighting (from 0 to 1) so that different outcomes have different profit results. This is useful for example if users want a zero loss situation for one side of the outcome and larger profits for the other side (which is possible if using bonus bets).

## Built With

* [Visual Studio 2013](https://visualstudio.microsoft.com/vs/)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
