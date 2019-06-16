# ScrabbleScorer

ScrabbleScorer is a console application made using C# and a nuget package called OpenOfficeXml.
This project logs scrabble scores for 2-6 players.

Created in October 2018, one month after beginning my programming journey.

## Features

Players:
  - Have Names
  - Have colors. This color will be the text color if its that player's turn
  - Can enable the game to be **recorded in Microsoft Excel**
  - Are provided with a box that displays Name and Current Score
  - Are able to create words once it's their turn
  - Have the ability to Double/Triple letters and words
  - Have a Word History box that contain their previous words and their scores
  - Can end the game by pressing Q after they finish their last word.

## Walkthrough

At start up, the user is asked how many players are playing. Our user indicated 3 players.
Our players are ROGER, JanCza, and JoshFox. The user is now asked if they wish to record their game in Excel. They said Yes!

![Alt text](/ScrabbleScorerV1.1.0/screenshots/RecordInExcelPrompt.png?raw=true "RecordInExcelPrompt")

Roger is about to enter his first word, "Hello." The 'H' in "Hello" is a Double Letter, marked by the D on the 'Letter Multi' row.
"Hello" is also a Double Word, as marked by the D on the 'Word Multi' row.
Once confirmed, "Hello" will be stored in Roger's Word History.

![Alt text](/ScrabbleScorerV1.1.0/screenshots/FirstCompleted.png?raw=true "FirstCompleted")

Each players take their turns. 
JanCza entered "Love" for 7 points.
JoshFox entered "Faith" for 11 points.
Roger entered "World" for 11 points. World has a Triple Letter on the letter 'L'.
Let's see our updated scores.

![Alt text](/ScrabbleScorerV1.1.0/screenshots/SecondWord.png?raw=true "SecondWord")

JanCza entered "Thanks" for 21 points.
JoshFox entered "Trust" for 21 points.
Roger entered "Bye" for 13 points.
JanCza enters "Scrabble" for 22 points!!

The players crowns JanCza as the winner with 50 points, and she is given the option to quit the game. She presses Q.

![Alt text](/ScrabbleScorerV1.1.0/screenshots/FinishGame.png?raw=true "FinishGame")

Since our user stated the game will be recorded in a Microsoft Excel sheet, the game record is generated.

![Alt text](/ScrabbleScorerV1.1.0/screenshots/ClosingScreen.png?raw=true "ClosingScreen")

The resulting Excel file is seen below:

![Alt text](/ScrabbleScorerV1.1.0/screenshots/ExcelLocation.png?raw=true "ExcelLocation")

---

![Alt text](/ScrabbleScorerV1.1.0/screenshots/ExcelSample1.png?raw=true "ExcelSample1")

---

![Alt text](/ScrabbleScorerV1.1.0/screenshots/ExcelSample2.png?raw=true "ExcelSample2")





