using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleScorerV1._1._0
{
	internal static class PrimaryDisplay
	{
		internal static bool GameSet = false;
        internal static bool RecordtoExcel = false;

		//Window Fields
		public static readonly int UIHeight = 40;
		public static readonly int UIWidth = 118;
		public static readonly int InstructionDisplayLines = 3;

		//Display Line Fields
		public static readonly int MainTitleLine = 4;
		public static readonly int MessageLine = 8;
		public static readonly int InstructionsLine = 12;
		public static readonly int WordLine = 18;
		public static readonly int LetterMultiLine = 20;
		public static readonly int BlankLine = 22;
		public static readonly int WordMultiLine = 24;
		public static readonly int WordScoreLine = 26;
		public static readonly int MainGuideLine = 8;
		public static readonly int MainTextLine = 23;
		public static readonly char[] MultiContents = {'2','3'};
		public static readonly char[] BlankContents = { 'B' };

		//Display Border Fields
		public static readonly int MainFloor = 28;
		public static readonly int MainInterface = 96;


		public static int backSpace = 0;
		public static readonly string ClearLine = "                                                                         ";
		internal static string[] instruction = {
					"                                                               ",
					"                                                               ",
					"                                                               "};
		public static string BackSpaceBuffer
		{
			get
			{
				string BackSpaceBuffer = "";
				for (int i = 0; i < backSpace; ++i)
				{
					BackSpaceBuffer += "  ";
				}
				return BackSpaceBuffer;
			}
		}

		internal static void BackgroundSet()
		{
			for (int height = 2; height < Console.WindowHeight; ++height)
			{
				string horzBorder = "*";
				Console.SetCursorPosition(2, height);
				if (height == 2 || height == (MainTitleLine+MessageLine)/2 || height == (MessageLine+InstructionsLine)/2 || height == WordLine-2 /*WordLine is the Top of Mainframe*/ || height == MainFloor || height == MainFloor+1 || height == Console.WindowHeight - 2 || height == Console.WindowHeight-1)
				{
					for (int i = 0; i < (MainInterface / 2); ++i)
					{
						horzBorder += " *";
					}
					Console.Write(horzBorder);
				}
				else if (height == 3 || height == 4 || height == 5)
				{
					Console.WriteLine(horzBorder);
				}
				else
				{
					for (int i = 0; i < MainInterface-3; ++i)
					{
						horzBorder += " ";
					}
					horzBorder += "*";
					Console.WriteLine(horzBorder);
				}
			}
			Cleaner();
		}

		internal static void Dashboard()
		{
			Console.SetCursorPosition(40/*centered*/, MainTitleLine); Console.Write("SCRABBLE SCORER C# - By R.L. Palabasan");
			Console.SetCursorPosition(MainGuideLine, MessageLine); Console.Write("MESSAGE:");
			Console.SetCursorPosition(MainGuideLine, InstructionsLine); Console.Write("INSTRUCTIONS:");
			Console.SetCursorPosition(MainGuideLine, WordLine); Console.Write("LETTERS:");
			Console.SetCursorPosition(MainGuideLine, LetterMultiLine); Console.Write("LETTER MULTI:");
			Console.SetCursorPosition(MainGuideLine, BlankLine); Console.Write("BLANK:");
			Console.SetCursorPosition(MainGuideLine, WordMultiLine); Console.Write("WORD MULTI:");
			Console.SetCursorPosition(MainGuideLine, WordScoreLine); Console.Write("WORD SCORE:");
			MessageBox("Welcome to the Scrabble Scoring C# App - Press Any Key to Continue"); 
			Cleaner();
			Console.ReadKey();
			Cleaner();
		}

        internal static void ToggleExcel()
        {
            string choice = string.Empty;
            bool valid = false;
            while (valid == false)
            {
                instruction[0] = "Would you like to record your game?                            ";
                instruction[1] = "                                                               ";
                instruction[2] = "Press Y (Yes) or N (No)                                        ";
                InstructionsBox(instruction);

                choice = Console.ReadKey().KeyChar.ToString().ToUpper();

                if (choice == "Y" || choice == "N")
                {
                    valid = true;
                }
            }

            if (choice == "Y") RecordtoExcel = true;
        }

		internal static void Cleaner()
		{
			Console.SetCursorPosition(1, 1);
			Console.Write(" ");
			Console.SetCursorPosition(1, 1);
		}

		internal static void InstructionsBox(string[] sentence)
		{
			for (int i = 0; i < InstructionDisplayLines; ++i)
			{
				Console.SetCursorPosition(MainTextLine, InstructionsLine + i);
				Console.Write(ClearLine);
				Console.SetCursorPosition(MainTextLine, InstructionsLine + i);
				Console.Write(sentence[i]);
				Cleaner();
			}
		}

		internal static void MessageBox(string sentence)
		{
			Console.SetCursorPosition(MainTextLine, MessageLine);
			Console.Write(ClearLine);
			Console.SetCursorPosition(MainTextLine, MessageLine);
			Console.Write(sentence);
			Cleaner();
		}

		internal static void WordBox(int letterPosition, string sentence)
		{
			Console.SetCursorPosition(MainTextLine + letterPosition, WordLine);
			Console.Write(sentence);
			Cleaner();
		}

		internal static void LetterMultiBox(int letterPosition, string sentence)
		{
			Console.SetCursorPosition(MainTextLine + letterPosition, LetterMultiLine);
			Console.Write(sentence);
			Cleaner();
		}

		internal static void BlankBox(int letterPosition, string sentence)
		{
			Console.SetCursorPosition(MainTextLine + letterPosition, BlankLine);
			Console.Write(sentence);
			Cleaner();
		}

		internal static void WordMultiBox(int letterPosition, string sentence)
		{
			Console.SetCursorPosition(MainTextLine + letterPosition, WordMultiLine);
			Console.Write(sentence);
			Cleaner();
		}

		internal static void WordScoreBox(int letterPosition, string sentence)
		{
			Console.SetCursorPosition(MainTextLine + letterPosition, WordScoreLine);
			Console.Write(sentence);
			Cleaner();
		}

		internal static string BonusString(int boxLine, int letterPosition)
		{

			string specialString = "";
			for(int i = 0; i <= letterPosition;++i)
			{
				if (boxLine == LetterMultiLine && MultiContents.Contains(WordScoring.typed[i, 1]))
				{
					specialString += $"{WordScoring.MultiIndicator(WordScoring.typed[i, 1].ToString())} ";
				}
				else if (boxLine == BlankLine && BlankContents.Contains(WordScoring.typed[i, 1]))
				{
					specialString += $"{WordScoring.MultiIndicator(WordScoring.typed[i, 1].ToString())} ";
				}
				else
				{
					specialString += "  ";
				}
			}
			specialString += "      ";
			return specialString;
		}

		internal static void AllClear()
		{
			WordBox(0, ClearLine);
			LetterMultiBox(0, ClearLine);
			BlankBox(0, ClearLine);
			WordMultiBox(0, ClearLine);
			WordScoreBox(0, ClearLine);
			backSpace = 0;
		}
	}
}
