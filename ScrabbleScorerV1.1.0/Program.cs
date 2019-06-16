using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace ScrabbleScorerV1._1._0
{
	class Program
	{

		static void Main(string[] args)
		{
			Console.SetWindowSize(PrimaryDisplay.UIWidth, PrimaryDisplay.UIHeight);
			PrimaryDisplay.BackgroundSet();
			PlayerDisplay.BackgroundSet();
			HistoryDisplay.BackgroundSet();
			HistoryDisplay.Dashboard();
			PrimaryDisplay.Dashboard();
			PlayerDisplay.Dashboard();

			while (PrimaryDisplay.GameSet != true)
			{
				for (int i = 0; i < PlayerDisplay.NumberOfParticipants; ++i)
				{
					WordScoring.WordScoringAlgorithm(PlayerDisplay.PlayerList[i]);
                    PlayerDisplay.PlayerList[i].TurnCount++;
                    PlayerDisplay.TotalTurnsElapsed++;
                    
                    if (PrimaryDisplay.GameSet == true) break;
				}
			}
            if (PrimaryDisplay.RecordtoExcel == true)
                ExcelStream.ExcelLoad();
            else ExcelStream.Exit();
        }
	}
}
