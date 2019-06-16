using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleScorerV1._1._0
{
	public static class HistoryDisplay
	{
		//Fields
		public static readonly int Region = 20;
		public static readonly int TitleLine = 8;
		public static readonly int LeftTextLine = PrimaryDisplay.MainInterface + (Region+1)/2;
		public static readonly int CenterTextLine = PrimaryDisplay.MainInterface + 5;
		public static List<string> HistoryArray = new List<string>(10);
		public static readonly string ClearLine = "                 ";

		//Set Up
		internal static void BackgroundSet()
		{
			for (int height = 2; height < Console.WindowHeight; ++height)
			{
				string horzBorder = "*";
				Console.SetCursorPosition(PrimaryDisplay.MainInterface, height);
				if (height == 2 || height == 6 || height == Console.WindowHeight - 1)
				{
					for (int i = 0; i < Region / 2; ++i)
					{
						horzBorder += " *";
					}
					Console.Write(horzBorder);
				}
				else if(height == 3 || height == 4 || height == 5)
				{
					horzBorder = " ";
					for (int i = 0; i < Region - 1; ++i)
					{
						horzBorder += " ";
					}
					horzBorder += "*";
					Console.WriteLine(horzBorder);
				}
				else
				{
					for (int i = 0; i < Region - 1; ++i)
					{
						horzBorder += " ";
					}
					horzBorder += "*";
					Console.WriteLine(horzBorder);
				}
			}
			PrimaryDisplay.Cleaner();
		}

		internal static void Dashboard()
		{
			Console.SetCursorPosition(CenterTextLine, TitleLine); Console.Write("WORD HISTORY");
			PrimaryDisplay.Cleaner();
		}

		internal static void HistoryAlgorithm(Player player)
		{
			Clear(player);
			HistoryBox(player);
		}

		internal static void HistoryAlgorithm(string word, int score, Player player)
		{
			AddToArray(word,score,player);
			Clear(player);
			HistoryBox(player);
		}

		internal static void AddToArray(string word, int score, Player player)
		{
			player.WordList.Insert(0,$"{word} - {score}");
			PrimaryDisplay.Cleaner();
		}

		internal static void Clear(Player player)
		{
			for (int i = 0; i < 15; ++i)
			{
				Console.SetCursorPosition(LeftTextLine - (Region - 2) / 2, TitleLine + 2 + (i * 2));
				Console.Write(ClearLine);
			}
		}

		internal static void HistoryBox(Player player)
		{
			for (int i = 0; i < player.WordList.Count; ++i)
			{
				Console.SetCursorPosition(LeftTextLine - player.WordList[i].Length/2, TitleLine+2+(i*2));
				Console.Write(player.WordList[i]);
			}
			PrimaryDisplay.Cleaner();
		}
	}
}
