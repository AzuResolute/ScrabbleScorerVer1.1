using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleScorerV1._1._0
{
	public class Player
	{
		public string Name { get; set; }
		public int PlayerNumber { get; internal set; }
		public int Score { get; internal set; }
		public int TurnCount { get; internal set; }
		public List<string> WordList = new List<string>(15);
        public string[,] scoreSheet = new string[30, 3];
        public ConsoleColor Color { get; internal set; }
		public int DisplayPosition
		{
			get
			{
				return (PlayerDisplay.PlayerScreenWidth * PlayerNumber) - PlayerDisplay.PlayerScreenWidth + 8;
			}
		}
		public Player (string name) //Used to create new player
		{
			Name = name;
			Score = 0;
			TurnCount = 0;
		}
	}
}
