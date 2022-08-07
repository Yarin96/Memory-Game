using System;
using System.Collections.Generic;
using Ex02.Enums;
using Ex02.Logic;

namespace Ex02.UserInterface
{
    public enum eColumns
    {
        Column1 = 'A',
        Column2 = 'B',
        Column3 = 'C',
        Column4 = 'D',
        Column5 = 'E',
        Column6 = 'F',
    }

    public enum eRows
    {
        Row1 = '1',
        Row2 = '2',
        Row3 = '3',
        Row4 = '4',
        Row5 = '5',
        Row6 = '6',
    }

    internal class Board
    {
        private static readonly char[] sr_ColumnIdentifier = { 'A', 'B', 'C', 'D', 'E', 'F' };
        private readonly int r_BoardWidth;
        private readonly int r_BoardHeight;
        private Card[,] m_Board;
        private GameLogic m_GameLogic;

        public Board(Player i_Player1, Player i_Player2, eGameMode i_GameMode, int i_BoardWidth, int i_BoardHeight)
        {
            r_BoardWidth = i_BoardWidth;
            r_BoardHeight = i_BoardHeight;
            m_Board = new Card[i_BoardWidth, i_BoardHeight];
            generateRandomCharValuesOnBoard();
            m_GameLogic = new GameLogic(i_Player1, i_Player2, i_GameMode, i_BoardWidth, i_BoardHeight);
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
                    m_Board[i, j] = new Card(i_List[indexInList], true, j, i);
                    indexInList++;
                }
            }
        }

        public void PrintBoard()
        {
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
                    for(int j = 0; j < r_BoardWidth; j++)
                    {
                        Console.Write("| ");
                        if(m_Board[j, i / 2].IsHidden)
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

        public bool isCardLocationInputValid(string i_Input)
        {
            bool isValid = false;
            if (i_Input.Length != 2)
            {
                isValid = false;
            }
            else
            {
                if (char.IsLetter(i_Input[0]) && char.IsDigit(i_Input[1]))
                {
                    int endingLetter = r_BoardWidth + 64;
                    int chosenLetter = (int)char.ToUpper(i_Input[0]);
                    int.TryParse(i_Input[1].ToString(), out int rowNum);
                    isValid = chosenLetter <= endingLetter && chosenLetter > 64 && rowNum <= r_BoardHeight && rowNum > 0;
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public bool isCardAlreadyChosen(string i_Input)
        {
            int colLetter = (int)char.ToUpper(i_Input[0]) - 64;
            int.TryParse(i_Input[1].ToString(), out int rowNum);
            return !m_Board[colLetter, rowNum].IsHidden;
        }
    }
}
