using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _034_CasinoMegaChallenge
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				leftImage.ImageUrl = "~/Plum.png";
				middleImage.ImageUrl = "~/Clover.png";
				rightImage.ImageUrl = "~/HorseShoe.png";

				double moneyTotal = 100.0;
				moneyLabel.Text = $"{moneyTotal:N2}";
			}
		}

		protected void leverButton_Click(object sender, EventArgs e)
		{
			//generate random images
			Random random = new Random();
			generateImages(random);

			//get player's bet & subtract from money total:
			double bet = 0;
			if (!Double.TryParse(betBox.Text, out bet))
			{
				resultLabel.Text = "Your input doesn't make sense, no bet made.";
				return;
			}
			subtractBet(bet);

			//display error message if bet is more than remaining total
			if (resultLabel.Text == "You don't have enough money for that!")
				return;

			//check if there's a bar and display lose message
			else if (checkForBar(bet))
				if (double.Parse(moneyLabel.Text) == 0)
					zeroBalanceRefresh();
				else return;

			//check if there's all 7's, display win message & add winnings to total
			else if (checkForSevens(bet))
				return;

			//check if there's one or more cherries (no bar), display win message & add winnings to total
			else if (checkForCherries(bet))
				return;

			else //generate losing message 
				if (double.Parse(moneyLabel.Text) == 0)
					zeroBalanceRefresh();
				else
					resultLabel.Text = $"Sorry, you lost {bet:C}. Better luck next time!";
		}

		private void generateImages(Random random)
		{
			string[] images = new string[12] { "~/Bar.png", "~/Bell.png", "~/Cherry.png",
				"~/Clover.png", "~/Diamond.png", "~/HorseShoe.png", "~/Lemon.png",
				"~/Orange.png", "~/Plum.png", "~/Seven.png", "~/Strawberry.png",
				"~/Watermellon.png" };

			leftImage.ImageUrl = images[random.Next(12)];
			middleImage.ImageUrl = images[random.Next(12)];
			rightImage.ImageUrl = images[random.Next(12)];
		}

		private void subtractBet(double bet)
		{
			double moneyTotal = double.Parse(moneyLabel.Text);
			if (moneyTotal >= bet)
			{
				moneyTotal -= bet;
				moneyLabel.Text = $"{moneyTotal:N2}";
			}
			else
				resultLabel.Text = "You don't have enough money for that!";
		}

		private bool checkForBar(double bet)
		{
			if (leftImage.ImageUrl == "~/Bar.png" || middleImage.ImageUrl == "~/Bar.png" || rightImage.ImageUrl == "~/Bar.png")
			{
				resultLabel.Text = $"Sorry, you lost {bet:C}. Better luck next time!";
				return true;
			}
			else return false;
		}

		private bool checkForSevens(double bet)
		{
			if (leftImage.ImageUrl == "~/Seven.png" && middleImage.ImageUrl == "~/Seven.png" && rightImage.ImageUrl == "~/Seven.png")
			{
				displayWinMessage(bet, 100);
				return true;
			}
			else return false;
		}

		private bool checkForCherries(double bet)
		{
			if (leftImage.ImageUrl == "~/Cherry.png" && middleImage.ImageUrl == "~/Cherry.png" && rightImage.ImageUrl == "~/Cherry.png")
			{
				displayWinMessage(bet, 4);
				return true;
			}
			else if ((leftImage.ImageUrl == "~/Cherry.png" && middleImage.ImageUrl == "~/Cherry.png") || (leftImage.ImageUrl == "~/Cherry.png" && rightImage.ImageUrl == "~/Cherry.png") || (middleImage.ImageUrl == "~/Cherry.png" && rightImage.ImageUrl == "~/Cherry.png"))
			{
				displayWinMessage(bet, 3);
				return true;
			}
			else if (leftImage.ImageUrl == "~/Cherry.png" || middleImage.ImageUrl == "~/Cherry.png" || rightImage.ImageUrl == "~/Cherry.png")
			{
				displayWinMessage(bet, 2);
				return true;
			}
			else return false;
		}

		private void displayWinMessage(double bet, int winMultiplier)
		{
			double winAmount = bet * winMultiplier;
			resultLabel.Text = $"You bet {bet:C} and won {winAmount:C}!";
			addWinnings(winAmount);
		}

		private void addWinnings(double winAmount)
		{
			double newTotal = double.Parse(moneyLabel.Text);
			newTotal += winAmount;
			moneyLabel.Text = $"{newTotal:N2}";
		}

		private void zeroBalanceRefresh()
		{
			//if no money left - display message, wait 5 seconds and refresh page
			resultLabel.Text = "You're out of money! (refreshing page...)";
			//Response.Redirect(Request.RawUrl); // just refreshes immediately...
			Response.AddHeader("REFRESH", "2;URL=Default.aspx");
		}
}
}
