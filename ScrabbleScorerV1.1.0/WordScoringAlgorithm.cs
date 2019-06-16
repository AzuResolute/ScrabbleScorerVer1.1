using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleScorerV1._1._0
{
	public static class WordScoring //As of 10.03.2018 - The foundation of this algorithm is Ver1.0.0.
	{
		//Main Scoring Algorithm - Refactored with newer classes

		public static char[,] typed = new char[12,2];

		public static void WordScoringAlgorithm(Player player)
		{
			bool exit = false;
			int totalScoreThisTurn = 0;
			while (exit == false)
			{
				Console.ForegroundColor = player.Color;
				HistoryDisplay.HistoryAlgorithm(player);
				//Initial Values
				string typedOutput = "";
				int rawWordScore = 0;
				int nmultiplier = 1;
				string multiplier = "1";
				int letterCount = 0;
				char input = ' ';
				int finalMultiplier = 1;

				//Instructions
				PrimaryDisplay.instruction[0] = "Please enter your first Letter.                                ";
				PrimaryDisplay.instruction[1] = "                                                               ";
				PrimaryDisplay.instruction[2] = "If you want to skip, press Tab.                                ";
				PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
				PrimaryDisplay.MessageBox($"{player.Name}'s Turn");
				PrimaryDisplay.WordBox(0, typedOutput);
				input = Console.ReadKey().KeyChar;   //First Input
				PrimaryDisplay.Cleaner();

				//Letter Loop
				for (int i = 0; i < 12; ++i)
				{
					if (input == '\t' && i == 0)
					{
						break;
					}

					if (input == '\t' && i != 0) break;
					if (input == '\b' && i != 0)
					{
						++PrimaryDisplay.backSpace;
						//Score
						--i;

						rawWordScore -= BackSpace(typed[i, 0],typed[i, 1]);//Fixed
						typed[i, 0] = typed[i,1] = ' ';
						PrimaryDisplay.WordScoreBox(0, $"{rawWordScore.ToString()}   "); //Output New Score
						//Letters
						typedOutput = $"{typedOutput.Substring(0, typedOutput.Length - 2)}";
						PrimaryDisplay.WordBox(0, $"{ typedOutput}{ PrimaryDisplay.BackSpaceBuffer}"); //Output Completed Letters
						//Blanks
						PrimaryDisplay.BlankBox(0, PrimaryDisplay.BonusString(PrimaryDisplay.BlankLine,i));
						//Multiplier
						PrimaryDisplay.LetterMultiBox(0, PrimaryDisplay.BonusString(PrimaryDisplay.LetterMultiLine, i));

						input = Console.ReadKey().KeyChar;  //Give them another chance at the same letter
						--i;
						continue;//Backspace Method
					}

					//if(PrimaryDisplay.backSpace>0)--PrimaryDisplay.backSpace;
					string sInputLetter = input.ToString().ToUpper();

					letterCount = sInputLetter.Count(char.IsLetter);
					if (letterCount <= 0)
					{
						PrimaryDisplay.MessageBox("Invalid Character. Please Try Again.    ");
						PrimaryDisplay.Cleaner();
						--i;
						input = Console.ReadKey().KeyChar;  //Give them another chance at the same letter
						continue;
						//do while loop			
					}

					//Input Letter passed
					typed[i,0] = input;
					typedOutput += sInputLetter + " "; //Add current Letter into Completed Letters
					PrimaryDisplay.WordBox(0, typedOutput); //Output Completed Letters
					rawWordScore += LetterScore(sInputLetter, "1");   //Calculate New Score by adding current to previous scores
					PrimaryDisplay.WordScoreBox(0, rawWordScore.ToString()); //Output New Score
					PrimaryDisplay.MessageBox($"Letters so far: {typedOutput} with {rawWordScore}");

					PrimaryDisplay.instruction[0] = "If the letter is double or triple, please press 2 or 3.";
					PrimaryDisplay.instruction[1] = "If you used a Blank tile, please press the Spacebar.";
					PrimaryDisplay.instruction[2] = "Otherwise, please enter your next letter or tab to finish.";
					PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);

					input = Console.ReadKey().KeyChar;  //Next Keyword then loop back unless... <see below>
					typed[i, 1] = ' ';
					if (input == ' ')   //... if the next char is Space, then...
					{
						rawWordScore -= LetterScore(sInputLetter, "1");
						typed[i, 1] = 'B';
						PrimaryDisplay.BlankBox(0, PrimaryDisplay.BonusString(PrimaryDisplay.BlankLine, i));
						PrimaryDisplay.MessageBox($"Letters so far: {typedOutput} with {rawWordScore}");
						PrimaryDisplay.WordScoreBox(0, "                                  ");    //Fixes Double Digit Bug
						PrimaryDisplay.WordScoreBox(0, rawWordScore.ToString());


						PrimaryDisplay.instruction[0] = "Please enter your next Letter.                                     ";
						PrimaryDisplay.instruction[1] = "If you finished your word, please press Tab to continue.           ";
						PrimaryDisplay.instruction[2] = "                                                                   ";
						PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);

						PrimaryDisplay.Cleaner();
						input = Console.ReadKey().KeyChar;  //Return a letter or tab and reloop
						continue;
					}

					multiplier = input.ToString();  //formatting to make the code work...
					int.TryParse(multiplier, out nmultiplier);

					if (nmultiplier == 2 || nmultiplier == 3)   //... if the next char is either 2 or 3, then...
					{
						multiplier = input.ToString();
						typed[i, 1] = input;
						PrimaryDisplay.LetterMultiBox(0, PrimaryDisplay.BonusString(PrimaryDisplay.LetterMultiLine, i));    //Output Letter Multiplier Indicator
						rawWordScore += (LetterScore(sInputLetter, multiplier) - LetterScore(sInputLetter, "1")); //New Score is Old RawScore plus new letter score with multiplier minus old letter score <avoid double count>
						PrimaryDisplay.MessageBox($"Letters so far: {typedOutput} with {rawWordScore}");
						PrimaryDisplay.WordScoreBox(0, rawWordScore.ToString());


						PrimaryDisplay.instruction[0] = "Please enter your next Letter.                                   ";
						PrimaryDisplay.instruction[1] = "If you finished your word, please press Tab to continue.         ";
						PrimaryDisplay.instruction[2] = "                                                                 ";
						PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);

						PrimaryDisplay.Cleaner();
						input = Console.ReadKey().KeyChar;  //Return a letter or tab and reloop
					}
				}

				//Word Multiplier
				PrimaryDisplay.MessageBox($"You typed {typedOutput} worth {rawWordScore} points. Any word multiplers?");
				PrimaryDisplay.instruction[0] = "If the word is double or triple, please press 2 or 3.            ";
				PrimaryDisplay.instruction[1] = "                                                                 ";
				PrimaryDisplay.instruction[2] = "Otherwise, please any other key to finalize your word.           ";
				PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
				input = Console.ReadKey().KeyChar;
				//Enter Anything else and we'll continue
				multiplier = input.ToString();
				int.TryParse(multiplier, out finalMultiplier);
				if (finalMultiplier == 2 || finalMultiplier == 3)
				{
					rawWordScore *= finalMultiplier;
					PrimaryDisplay.WordMultiBox(0, MultiIndicator(multiplier));
				}
				PrimaryDisplay.MessageBox($"Your Total Word Score for {typedOutput} is {rawWordScore}");
				PrimaryDisplay.WordScoreBox(0, rawWordScore.ToString());  //FINAL SCORE

				//Confirm Put In
				PrimaryDisplay.instruction[0] = "Please confirm that the Word and Score are Correct.            ";
				PrimaryDisplay.instruction[2] = "Press Tab to Confirm.                                          ";
				PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);

				input = Console.ReadKey(true).KeyChar;
				if (input != '\t')
				{
					PrimaryDisplay.AllClear();
					continue;
				}

                if (rawWordScore != 0)
                {
                    HistoryDisplay.HistoryAlgorithm(typedOutput.Replace(" ", string.Empty),rawWordScore,player);
				    PlayerDisplay.UpdateScore(player, rawWordScore);

                    totalScoreThisTurn += rawWordScore;
                    player.scoreSheet[player.TurnCount, 0] = $"{player.Name}";
                    player.scoreSheet[player.TurnCount, 1] += $"{typedOutput.Replace(" ", string.Empty)} ";
                    player.scoreSheet[player.TurnCount, 2] = $"{totalScoreThisTurn}";
                }

                PrimaryDisplay.instruction[0] = "Please press TAB to enter another word.                          ";
				PrimaryDisplay.instruction[1] = "Or, Please Press 'Q' to end the game.                            ";
				PrimaryDisplay.instruction[2] = "Otherwise, press any key to pass this on to the next player.     ";

				PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
				input = Console.ReadKey().KeyChar;
				if (input.ToString().ToUpper() == "Q")
				{
					exit = true;
					PrimaryDisplay.GameSet = true;
					PrimaryDisplay.AllClear();
				}
				else if (input != '\t')  //Press Tab to Move to Next Player
				{
					exit = true;
					PrimaryDisplay.AllClear();
				}
				else
				{
					PrimaryDisplay.AllClear();
				}
            }
		}

        static void Deductions(Player player)
        {
            PrimaryDisplay.instruction[0] = "Please enter your leftover words.                                ";
            PrimaryDisplay.instruction[1] = "                                                                 ";
            PrimaryDisplay.instruction[2] = "                                                                 ";

        }

		static int BackSpace (char letter, char multiplier)
		{
			return LetterScore(letter.ToString().ToUpper(), multiplier.ToString());
		}

		//field - Scoring
		internal static int RawScore { get; set; }
		internal static string MultiplierIndicator { get; set; }

		//Methods - Scoring
		internal static int LetterScore(string letter, string multiplier)
		{
			int score = 0;
			int multiplierBasis = 1;
			switch (letter)
			{
				case "A": score = 1; break;
				case "B": score = 3; break;
				case "C": score = 3; break;
				case "D": score = 2; break;
				case "E": score = 1; break;
				case "F": score = 4; break;
				case "G": score = 2; break;
				case "H": score = 4; break;
				case "I": score = 1; break;
				case "J": score = 8; break;
				case "K": score = 5; break;
				case "L": score = 1; break;
				case "M": score = 3; break;
				case "N": score = 1; break;
				case "O": score = 1; break;
				case "P": score = 3; break;
				case "Q": score = 10; break;
				case "R": score = 1; break;
				case "S": score = 1; break;
				case "T": score = 1; break;
				case "U": score = 1; break;
				case "V": score = 4; break;
				case "W": score = 4; break;
				case "X": score = 8; break;
				case "Y": score = 4; break;
				case "Z": score = 10; break;
				case " ": score = 0; break;
				default: score = 0; break;
			}
			switch (multiplier)
			{
				case "B": multiplierBasis = 0; break;
				case "2": multiplierBasis = 2; break;
				case "3": multiplierBasis = 3; break;
				default: multiplierBasis = 1; break;
			}
			return RawScore = score * multiplierBasis;
		}

		internal static string MultiIndicator(string multiplier)
		{
			switch (multiplier)
			{
				case "0": MultiplierIndicator = "B"; break;
				case "2": MultiplierIndicator = "D"; break;
				case "3": MultiplierIndicator = "T"; break;
				default: MultiplierIndicator = " "; break;
			}
			return MultiplierIndicator;
		}
	}
}