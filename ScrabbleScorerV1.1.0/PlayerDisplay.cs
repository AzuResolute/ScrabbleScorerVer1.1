using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleScorerV1._1._0
{
	public static class PlayerDisplay
	{
		//General Display Fields
		internal static readonly int StartLine = PrimaryDisplay.MainFloor;
		public static readonly int maximum = 5;
		public static readonly int PlayerScreenWidth = 9 * 2;
		internal static readonly int NameDisplay = PrimaryDisplay.MainFloor + 4;
		internal static readonly int ScoreDisplay = PrimaryDisplay.MainFloor + 6;
		public static int TotalTurnsElapsed = 0;

		//Player Fields

		internal static List<Player> PlayerList = new List<Player>(maximum);

		public static Player LastPlayer { get; private set; }

		//Current Player Properties
		public static int NumberOfParticipants { get; private set; }
		public static Player CurrentPlayer { get; private set; }

		internal static void BackgroundSet()
		{
			for (int height = StartLine+2; height < Console.WindowHeight - 2; ++height)
			{
				Console.SetCursorPosition(4, height);

				for (int i = 0; i < PrimaryDisplay.MainInterface-4; ++i)
				{
					if (i == 0 || i == PlayerScreenWidth || i == PlayerScreenWidth*2 || i == PlayerScreenWidth*3 || i == PlayerScreenWidth*4 || i == PrimaryDisplay.MainInterface - 6)
					{
						Console.Write("*");
					}
					else
					{
						Console.Write(" "); ;
					}
				}
			}
			PrimaryDisplay.Cleaner();
		}

		//Player-Altering Methods
		public static void Dashboard()
		{
			bool invalid = false;
			PrimaryDisplay.instruction[0] = "How many players will be playing today?                        ";
            PrimaryDisplay.instruction[2] = "                                                               ";
            do
			{
				PrimaryDisplay.MessageBox(PrimaryDisplay.ClearLine);
				PrimaryDisplay.MessageBox("Number of Players:  ");
				char input = Console.ReadKey(true).KeyChar;
				invalid = HowManyPlayers(input);
			}
			while (invalid == true);

			for (int i = 1; i <= NumberOfParticipants; ++i)
			{
				Console.ForegroundColor = PlayerColor(i);
				PrimaryDisplay.instruction[0] = $"Player {i}, Please enter your name.                        ";
				PrimaryDisplay.instruction[2] = "Maximum of 8 characters.                            ";
				PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
				PrimaryDisplay.MessageBox(PrimaryDisplay.ClearLine);
				PrimaryDisplay.MessageBox("Name:  ");
				do
				{
					Console.SetCursorPosition(PrimaryDisplay.MainTextLine + 10, PrimaryDisplay.MessageLine);
					Console.Write("                                    ");
					Console.SetCursorPosition(PrimaryDisplay.MainTextLine + 10, PrimaryDisplay.MessageLine);
					string name = Console.ReadLine();
					if (name.Count() > 8)
					{
						PrimaryDisplay.instruction[0] = "The name exceeds the maximum characters allowed.         ";
						PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
						invalid = true;
					}
					else
                    if (name.Contains('\t'))
                    {
                        PrimaryDisplay.instruction[0] = "The name cannot contain a Tab character.                 ";
                        PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
                        invalid = true;
                    }
                    else
                    {
						PlayerDisplay.AddPlayer(name);
						//Put into Player Display
						Console.SetCursorPosition(PlayerList[i - 1].DisplayPosition + 5 - name.Count() / 2, NameDisplay);
						Console.Write(name);
						Console.SetCursorPosition(PlayerList[i - 1].DisplayPosition + 5, ScoreDisplay);
						Console.Write(PlayerList[i - 1].Score);
						invalid = false;
					}
				}
				while (invalid == true);
			}

            PrimaryDisplay.ToggleExcel();
            PrimaryDisplay.Cleaner();
		}

		public static bool HowManyPlayers(char input)
		{
			NumberOfParticipants = 2; //Default
			PrimaryDisplay.instruction[1] = "                                                               ";
			PrimaryDisplay.instruction[2] = "Maximum of 5 players at a time.                                ";
			PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
			if (input.ToString().Count(char.IsDigit) == 0)
			{
				PrimaryDisplay.instruction[0] = "Please enter a number.                                         ";
				PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
				return true;
			}
			else if (int.Parse(input.ToString()) > 5)
			{
				PrimaryDisplay.instruction[0] = "The number exceeds the maximum number of players allowed.      ";
				PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
				return true;
			}
			else
			{
				int numInput = int.Parse(input.ToString());
				NumberOfParticipants = numInput;
				return false;
			}
		}

		public static void UpdateScore (Player player, int score)
		{
			player.Score += score;
			Console.SetCursorPosition(player.DisplayPosition + 5, ScoreDisplay);
			Console.Write(player.Score);
		}

		public static void AddPlayer(string name)
		{
			if(PlayerList.Count >= 5)
			{
				throw new Exception("Reached Maximum Number of Players");
			}
			Player player = new Player(name);
			PlayerList.Add(player);
			player.Color = PlayerColor(PlayerList.Count);
			player.PlayerNumber	= PlayerList.Count();
		}

		public static Player AssignLastPlayer(Player lastPlayer)
		{
			return LastPlayer = lastPlayer;
		}

		public static ConsoleColor PlayerColor(int playerNumber)
		{
			ConsoleColor color = new ConsoleColor();

			switch(playerNumber)
			{
				case 1: color = ConsoleColor.Blue; break;
				case 2: color = ConsoleColor.Red; break;
				case 3: color = ConsoleColor.Green; break;
				case 4: color = ConsoleColor.Yellow; break;
				default: color = ConsoleColor.Gray;break;
			}
			return color;
		}
	}
}
