using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;
using Ex02.Enums;
using Ex02.Logic;

namespace Ex02.UserInterface
{
    internal class Board
    {
        private static readonly char[] sr_ColumnIdentifier = { 'A', 'B', 'C', 'D', 'E', 'F' };
        private readonly int r_BoardWidth;
        private readonly int r_BoardHeight;
        private eGameMode m_GameMode;
        private Card[,] m_Board;
        private GameLogic m_GameLogic;

        public Board(Player i_Player1, Player i_Player2, eGameMode i_GameMode, int i_BoardWidth, int i_BoardHeight)
        {
            m_GameMode = i_GameMode;
            r_BoardWidth = i_BoardWidth;
            r_BoardHeight = i_BoardHeight;
            m_Board = new Card[i_BoardWidth, i_BoardHeight];
            m_GameLogic = new GameLogic(i_Player1, i_Player2, i_GameMode, i_BoardWidth, i_BoardHeight);
            runGame();
        }

        private void runGame()
        {
            generateRandomCharValuesOnBoard();
            PrintBoard();
            int totalPossibleScore = (r_BoardWidth * r_BoardHeight) / 2;
            while (m_GameLogic.Player1.PlayerScore + m_GameLogic.Player2.PlayerScore != totalPossibleScore)
            {
                if (m_GameMode == eGameMode.PlayerVsComputer)
                {
                    m_GameLogic.ComputerTurn();
                }
                else
                {
                    humanPlayerTurn();
                }
            }

            gameOver();
        }

        public void PrintBoard()
        {
            printScoreBoard();
            Console.Write("   ");
            for (int i = 0; i < r_BoardWidth; i++)
            {
                Console.Write("  " + sr_ColumnIdentifier[i].ToString() + "  ");
            }

            Console.Write("\n");

            for (int i = 0; i < r_BoardHeight * 2; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write("   ");
                    for (int j = 0; j < r_BoardWidth * 5; j++)
                    {
                        Console.Write("=");
                    }

                    Console.Write("=");
                }
                else
                {
                    Console.Write(" " + ((i + 1) / 2).ToString() + " ");
                    for (int j = 0; j < r_BoardWidth; j++)
                    {
                        Console.Write("| ");
                        if (m_Board[j, i / 2].IsHidden)
                        {
                            Console.Write("   ");
                        }
                        else
                        {
                            Console.Write(" " + m_Board[j, i / 2].CardValue.ToString() + " ");
                        }
                    }

                    Console.Write("|");
                }

                Console.Write("\n");
            }

            Console.Write("   ");
            for (int j = 0; j < r_BoardWidth * 5; j++)
            {
                Console.Write("=");
            }

            Console.Write("=");
            Console.Write("\n");
        }

        public void CheckIfValidCard(ref string i_UserCardChoice)
        {
            bool isCardChosen = false;
            bool isLegalCard = isCardLocationInputValid(i_UserCardChoice);
            if (isLegalCard)
            {
                isCardChosen = isCardAlreadyChosen(i_UserCardChoice);
            }

            while (!isLegalCard || isCardChosen)
            {
                if (!isLegalCard)
                {
                    Console.WriteLine("No such card exists on the board, please choose again: ");
                }
                else if (isCardChosen)
                {
                    Console.WriteLine("Card has already been discovered, please choose again: ");
                }

                i_UserCardChoice = Console.ReadLine();
                isLegalCard = isCardLocationInputValid(i_UserCardChoice);
                if (isLegalCard)
                {
                    isCardChosen = isCardAlreadyChosen(i_UserCardChoice);
                }
            }
        }

        private bool isCardLocationInputValid(string i_UserCardInput)
        {
            bool isValid = false;
            if (i_UserCardInput.Length != 2)
            {
                isValid = false;
            }
            else
            {
                if (char.IsLetter(i_UserCardInput[0]) && char.IsDigit(i_UserCardInput[1]))
                {
                    int endingLetter = r_BoardWidth + 64;
                    int chosenLetter = (int)char.ToUpper(i_UserCardInput[0]);
                    int.TryParse(i_UserCardInput[1].ToString(), out int rowNum);
                    isValid = chosenLetter <= endingLetter && chosenLetter > 64 && rowNum <= r_BoardHeight && rowNum > 0;
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool isCardAlreadyChosen(string i_UserCardInput)
        {
            int colLetter = convertLetterToInt(i_UserCardInput[0]);
            int rowNum = convertNumberCharacterToInt(i_UserCardInput[1]);
            return !m_Board[colLetter - 1, rowNum - 1].IsHidden;
        }

        private void humanPlayerTurn()
        {
            int capitalLetterIndexOnBoard;
            int numberIndexOnBoard;
            string cardChoice;
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(
                        "\n-> {0}'s turn, please choose an option: " +
                        "\n     -> Type an index representing the choice of your card, in the format of [CapitalLetter|Number]." +
                        "\n     -> Type 'Q' to exit the program.", m_GameLogic.CurrentPlayer.PlayerName);
                if (i == 0)
                {
                    Console.Write("\nEnter first choice: ");
                }
                else
                {
                    Console.Write("\nEnter second choice: ");
                }

                cardChoice = Console.ReadLine();
                if (cardChoice.ToUpper() == "Q")
                {
                    exitProgram();
                }
                else
                {
                    CheckIfValidCard(ref cardChoice);
                    capitalLetterIndexOnBoard = convertLetterToInt(cardChoice[0]);
                    numberIndexOnBoard = convertNumberCharacterToInt(cardChoice[1]);
                    m_GameLogic.UpdateNextTurn(m_Board[capitalLetterIndexOnBoard - 1, numberIndexOnBoard - 1]);
                    printUpdatedBoard();
                }
            }

            if (!m_GameLogic.CardValuesMatch)
            {
                Console.WriteLine("\nWrong choice, try next time!");
                System.Threading.Thread.Sleep(2000);
                m_GameLogic.PreviousCard.IsHidden = true;
                m_GameLogic.CurrentCard.IsHidden = true;
                printUpdatedBoard();
            }
        }

        private int convertLetterToInt(char i_Char)
        {
            return (int)char.ToUpper(i_Char) - 64;
        }

        private int convertNumberCharacterToInt(char i_Char)
        {
            int.TryParse(i_Char.ToString(), out int numberIndexOnBoard);
            return numberIndexOnBoard;
        }

        private void printUpdatedBoard()
        {
            Screen.Clear();
            PrintBoard();
        }

        private void generateRandomCharValuesOnBoard()
        {
            List<char> couplesOfLetters = new List<char>(r_BoardWidth * r_BoardHeight);
            addCouplesOfLettersToList(ref couplesOfLetters);
            shuffleList(ref couplesOfLetters);
            assignLettersToList(ref couplesOfLetters);
        }

        private void addCouplesOfLettersToList(ref List<char> i_List)
        {
            Random randomValue = new Random();
            for (int i = 0; i < i_List.Capacity / 2; i++)
            {
                char cardValue = (char)randomValue.Next(65, 91);
                i_List.Add(cardValue);
                i_List.Add(cardValue);
            }
        }

        private void shuffleList(ref List<char> i_List)
        {
            int indexInList;
            Random randomValue = new Random();
            for (int i = i_List.Count - 1; i > 1; i--)
            {
                indexInList = randomValue.Next(i + 1);
                char randomChar = i_List[indexInList];
                i_List[indexInList] = i_List[i];
                i_List[i] = randomChar;
            }
        }

        private void assignLettersToList(ref List<char> i_List)
        {
            int indexInList = 0;
            for (int i = 0; i < r_BoardWidth; i++)
            {
                for (int j = 0; j < r_BoardHeight; j++)
                {
                    m_Board[i, j] = new Card(i_List[indexInList], true);
                    indexInList++;
                }
            }
        }

        private void printScoreBoard()
        {
            Console.WriteLine();
            Console.WriteLine("  " + m_GameLogic.Player1.PlayerName + "'s score: " + m_GameLogic.Player1.PlayerScore + "   -   " + m_GameLogic.Player2.PlayerName + "'s score: " + m_GameLogic.Player2.PlayerScore + "\n");
        }

        private void exitProgram()
        {
            Console.Write("\nExiting program.");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }

        private void gameOver()
        {
            Screen.Clear();
            Console.WriteLine("Game Over! final results:\n");
            printScoreBoard();
            Console.WriteLine("Press R to restart the game or press Enter to exit.");
            /// Implement Restart method here
            exitProgram();
        }
    }
}