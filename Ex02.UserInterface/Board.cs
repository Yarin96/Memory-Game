using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;
using Ex02.Logic;

namespace Ex02.UserInterface
{
    internal enum eColumnIndex
    {
        A,
        B,
        C,
        D,
        E,
        F,
    }

    internal class Board
    {
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
            m_GameLogic = new GameLogic(i_Player1, i_Player2, i_BoardWidth, i_BoardHeight, m_Board);
            runGame();
        }

        private void runGame()
        {
            generateRandomCharValuesOnBoard();
            printBoard();
            int totalPossibleScore = (r_BoardWidth * r_BoardHeight) / 2;
            while (m_GameLogic.Player1.PlayerScore + m_GameLogic.Player2.PlayerScore != totalPossibleScore)
            {
                if (m_GameMode == eGameMode.PlayerVsComputer)
                {
                    if(m_GameLogic.CurrentPlayer.PlayerType == ePlayerType.Computer)
                    {
                        computerPlayerTurn();
                    }
                    else
                    {
                        humanPlayerTurn();
                    }
                }
                else
                {
                    humanPlayerTurn();
                }
            }

            gameOver();
        }

        private void checkIfValidCard(ref string i_UserCardChoice)
        {
            bool isCardChosen = false;
            bool isLegalCard = m_GameLogic.IsCardLocationInputValid(i_UserCardChoice);
            if (isLegalCard)
            {
                isCardChosen = m_GameLogic.IsCardAlreadyChosen(i_UserCardChoice);
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
                isLegalCard = m_GameLogic.IsCardLocationInputValid(i_UserCardChoice);
                if (isLegalCard)
                {
                    isCardChosen = m_GameLogic.IsCardAlreadyChosen(i_UserCardChoice);
                }
            }
        }

        private void printBoard()
        {
            printScoreBoard();
            Console.Write("     ");
            int counterOfColumnIndex = r_BoardWidth;
            foreach (eColumnIndex enumValue in Enum.GetValues(typeof(eColumnIndex)))
            {
                if (counterOfColumnIndex > 0)
                {
                    Console.Write($"{enumValue}   ");
                    counterOfColumnIndex--;
                }
                else
                {
                    break;
                }
            }

            Console.Write("\n");
            for (int i = 0; i < r_BoardHeight * 2; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write("   ");
                    for (int j = 0; j < r_BoardWidth * 4; j++)
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
                            Console.Write("  ");
                        }
                        else
                        {
                            Console.Write(m_Board[j, i / 2].CardValue.ToString() + " ");
                        }
                    }

                    Console.Write("|");
                }

                Console.Write("\n");
            }

            Console.Write("   ");
            for (int j = 0; j < r_BoardWidth * 4; j++)
            {
                Console.Write("=");
            }

            Console.Write("=");
            Console.Write("\n");
        }

        private void computerPlayerTurn()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("\n   Computer is thinking...");
                wait(2000);
                m_GameLogic.ComputerTurn();
                printUpdatedBoard();
            }

            if (!m_GameLogic.CardValuesMatch)
            {
                Console.WriteLine("\n   Computer was wrong! You're up next!");
                wait(2000);
                m_GameLogic.PreviousCard.IsHidden = true;
                m_GameLogic.CurrentCard.IsHidden = true;
            }
            else
            {
                Console.WriteLine("\n   * Computer got a point! *");
                wait(2000);
            }

            printUpdatedBoard();
        }

        private void humanPlayerTurn()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(
                        "\n-> {0}'s turn, please choose an option: " +
                        "\n     -> Type an index representing the choice of your card, in the format of [CapitalLetter|Number]." +
                        "\n     -> Type 'Q' to exit the program.", m_GameLogic.CurrentPlayer.PlayerName);
                if (i == 0)
                {
                    Console.Write("\n-> Enter first choice: ");
                }
                else
                {
                    Console.Write("\n-> Enter second choice: ");
                }

                string cardChoice = Console.ReadLine();
                if (cardChoice.ToUpper() == "Q")
                {
                    exitProgram();
                }
                else
                {
                    checkIfValidCard(ref cardChoice);
                    int capitalLetterIndexOnBoard = m_GameLogic.ConvertLetterToInt(cardChoice[0]);
                    int numberIndexOnBoard = m_GameLogic.ConvertNumberCharacterToInt(cardChoice[1]);
                    m_GameLogic.UpdateNextTurn(m_Board[capitalLetterIndexOnBoard - 1, numberIndexOnBoard - 1]);
                    printUpdatedBoard();
                }
            }

            if (!m_GameLogic.CardValuesMatch)
            {
                Console.WriteLine("\n   Wrong choice, try next time!");
                wait(2000);
                m_GameLogic.PreviousCard.IsHidden = true;
                m_GameLogic.CurrentCard.IsHidden = true;
            }
            else
            {
                Console.WriteLine("\n   * {0} got a point! *", m_GameLogic.CurrentPlayer.PlayerName);
                wait(2000);
            }

            printUpdatedBoard();
        }

        private void printUpdatedBoard()
        {
            Screen.Clear();
            printBoard();
        }

        private void generateRandomCharValuesOnBoard()
        {
            List<char> couplesOfLetters = new List<char>(r_BoardWidth * r_BoardHeight);
            addCouplesOfLettersToList(ref couplesOfLetters);
            shuffleList(ref couplesOfLetters);
            assignLettersFromListToBoard(ref couplesOfLetters);
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

        private void assignLettersFromListToBoard(ref List<char> i_List)
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
            Console.WriteLine(
                "   | " +
                m_GameLogic.Player1.PlayerName +
                "'s score: " +
                m_GameLogic.Player1.PlayerScore +
                "   -   " +
                m_GameLogic.Player2.PlayerName +
                "'s score: " +
                m_GameLogic.Player2.PlayerScore +
                " |\n");
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
            Console.WriteLine("\n ~~~~~ Game Over! Final Results: ~~~~~\n");
            printScoreBoard();
            Console.WriteLine("-> Press R to restart the game or press Enter to exit.");
            string isGameOver = Console.ReadLine();
            while (isGameOver != string.Empty && isGameOver != "R")
            {
                Console.WriteLine("Wrong input! Press R to restart the game or press Enter to exit.");
                isGameOver = Console.ReadLine();
            }

            if (isGameOver != "R")
            {
                exitProgram();
            }
            else
            {
                restartGame();
            }
        }

        private void restartGame()
        {
            Screen.Clear();
            new UserInterface().InitiateGame();
        }

        private void wait(int i_TimeMiliSeconds)
        {
            System.Threading.Thread.Sleep(i_TimeMiliSeconds);
        }
    }
}