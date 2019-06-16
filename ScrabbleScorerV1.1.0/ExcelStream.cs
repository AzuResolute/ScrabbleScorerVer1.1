using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleScorerV1._1._0
{
    internal static class ExcelStream
    {
        //ScorerSpreadsheetVIFields
        internal static readonly string GameName = $"Game {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";

        //SCORE SHEET - Where our data goes
        internal static readonly int nameRow = 3;
        internal static readonly int scoreInitRow = 4;
        internal static readonly int scoreInitCol = 2;
        internal static readonly int colPerPlayer = 3;

        //STAT SHEET - Stat beginning and ends are needed to format copy macro
        internal static readonly int statGameNameCol = 26 /*col Z*/;
        internal static readonly int statTypeNameCol = 30 /*col AD*/;
        internal static readonly int statInitRow = scoreInitRow; /*Aligns*/

        //DATA SHEET - Where the Data goes after finalization
        internal static readonly int dataGameNameCol = 2 /*col B*/;
        internal static readonly int dataTypeNameCol = 6 /*col F*/;
        internal static readonly int dataInitRow = 4;
        internal static readonly int[] dataHiddenCountCell = { 1, 1 }; //Max rows at 2000
        internal static readonly int[] gameTotalTurns = { 5, 22 };

        internal static void ExcelLoad()
        {
            FileInfo scrabbleData = new FileInfo("ScrabbleDatabase.xlsx");
            using (ExcelPackage excelConnector = new ExcelPackage(scrabbleData))
            {


                ExcelWorksheet copySheet = excelConnector.Workbook.Worksheets.Copy("Template", "GameSheet");
                ExcelWorksheet gameSheet = excelConnector.Workbook.Worksheets[3];/*Usage of new sheet*/
                //Headers
                for (int i = 0; i < PlayerDisplay.NumberOfParticipants; ++i)
                {
                    gameSheet.Cells[nameRow, (i * colPerPlayer) + scoreInitCol].Value = PlayerDisplay.PlayerList[i].Name;
                }
                //Scoresheet
                for (int i = 0; i < PlayerDisplay.NumberOfParticipants; ++i)
                {
                    for (int j = 0; j < PlayerDisplay.PlayerList[i].TurnCount; ++j)
                    {
                        if (!int.TryParse(PlayerDisplay.PlayerList[i].scoreSheet[j, 2], out int result)) result = 0;
                        gameSheet.Cells[scoreInitRow + j, (i * colPerPlayer) + scoreInitCol].Value = result;
                        gameSheet.Cells[scoreInitRow + j, (i * colPerPlayer) + scoreInitCol + 1/*Next Row*/].Value = PlayerDisplay.PlayerList[i].scoreSheet[j, 1];
                    }
                }
                //Finalize Game
                gameSheet.Name = GameName;

                excelConnector.SaveAs(new FileInfo($"ScrabbleScorer {DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Year}{DateTime.Now.Hour}{DateTime.Now.Minute}.xlsx"));
                excelConnector.Dispose();
            }
            PrimaryDisplay.MessageBox($"Thank You for Playing!                    ");
            PrimaryDisplay.instruction[0] = "Thank You for Playing! Your game data has been saved as:         ";
            PrimaryDisplay.instruction[1] = $"ScrabbleScorer {DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Year}{DateTime.Now.Hour}{DateTime.Now.Minute}.xlsx                                ";
            PrimaryDisplay.instruction[2] = "Press any key to exit.                                           ";
            PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
            Console.ReadKey(); return;
        }

        internal static void Exit (ExcelPackage package)
        {
            PrimaryDisplay.MessageBox($"Thank You for Playing!                    ");
            PrimaryDisplay.instruction[0] = "Thank You for Playing!                                           ";
            PrimaryDisplay.instruction[1] = "                                                                 ";
            PrimaryDisplay.instruction[2] = "Press any key to exit.                                           ";
            PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
            package.Dispose();
            Console.ReadKey(); return;
        }

        internal static void Exit()
        {
            PrimaryDisplay.MessageBox($"Thank You for Playing!                    ");
            PrimaryDisplay.instruction[0] = "Thank You for Playing!                                           ";
            PrimaryDisplay.instruction[1] = "                                                                 ";
            PrimaryDisplay.instruction[2] = "Press any key to exit.                                           ";
            PrimaryDisplay.InstructionsBox(PrimaryDisplay.instruction);
            Console.ReadKey(); return;
        }
    }
}
