using System;
using System.Collections.Generic;
using Ex02.Enums;
using Ex02.Logic;

namespace Ex02.UserInterface
{
    public enum eColumns
    {
        COLUMN_1 = 'A',
        COLUMN_2 = 'B',
        COLUMN_3 = 'C',
        COLUMN_4 = 'D',
        COLUMN_5 = 'E',
        COLUMN_6 = 'F',
    }

    public enum eRows
    {
        ROW_1 = '1',
        ROW_2 = '2',
        ROW_3 = '3',
        ROW_4 = '4',
        ROW_5 = '5',
        ROW_6 = '6',
    }

    internal class Board
    {
        private static readonly char[] sr_ColumnIdentifier = { 'A', 'B', 'C', 'D', 'E', 'F' };
        private Card[,] m_Board;
        private int m_BoardWidth;
        private int m_BoardHeight;

        public Board(Player i_Player1, Player i_Player2, eGameMode i_GameMode, int i_BoardWidth, int i_BoardHeight)
        {
            m_BoardWidth = i_BoardWidth;
            m_BoardHeight = i_BoardHeight;
            m_Board = new Card[i_BoardWidth, i_BoardHeight];
            generateRandomCharValuesOnBoard();
        }

        private void generateRandomCharValuesOnBoard()
        {
            List<char> couplesOfLetters = new List<char>(m_BoardWidth * m_BoardHeight);
            addCouplesOfLettersToList(ref couplesOfLetters);
            shuffleList(ref couplesOfLetters);
            assignLettersToList(ref couplesOfLetters);
        }

        private void addCouplesOfLettersToList(ref List<char> i_List)
        {
            Random randomValue = new Random();
            for (int i = 0; i < i_List.Count / 2; i++)
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
            for (int i = 0; i < m_BoardWidth; i++)
            {
                for (int j = 0; j < m_BoardHeight; j++)
                {
                    m_Board[i, j].CardValue = i_List[indexInList];
                    indexInList++;
                }
            }
        }

        public void PrintBoard()
        {
            Console.Write("   ");
            for (int i = 0; i < m_BoardWidth; i++)
            {
                Console.Write("  " + sr_ColumnIdentifier[i].ToString() + "  ");
            }

            Console.Write("\n");

            for (int i = 0; i < m_BoardHeight * 2; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write("   ");
                    for (int j = 0; j < m_BoardWidth * 5; j++)
                    {
                        Console.Write("=");
                    }

                    Console.Write("=");
                }
                else
                {
                    Console.Write(" " + ((i + 1) / 2).ToString() + "  ");
                    for (int j = 0; j < m_BoardWidth; j++)
                    {
                        Console.Write("|");
                        Console.Write(m_Board[i, j].CardValue); // if i want to hide something, show this, else show the item i want to show
                    }

                    Console.Write("|");
                }

                Console.Write("\n");
            }

            Console.Write("   ");
            for (int j = 0; j < m_BoardWidth * 5; j++)
            {
                Console.Write("=");
            }

            Console.Write("=");
            Console.Write("\n");
        }

        public bool IsCardValueEqual(Card i_Card1, Card i_Card2)
        {
            bool validFlag = true;
            return validFlag;
        }
    }
}
