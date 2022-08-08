using System;

namespace Ex02.Logic
{
    public class GameLogic
    {
        private readonly int r_BoardHeight;
        private readonly int r_BoardWidth;
        private bool m_IsFirstCardSelection;
        private bool m_CardValuesMatch;
        private Card m_CurrentCardSelection;
        private Card m_PreviousCardSelection;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private Card[,] m_Board;

        public GameLogic(
            Player i_Player1,
            Player i_Player2,
            int i_BoardWidth,
            int i_BoardHeight,
            Card[,] i_Board)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_CurrentPlayer = i_Player1;
            r_BoardWidth = i_BoardWidth;
            r_BoardHeight = i_BoardHeight;
            m_IsFirstCardSelection = true;
            m_Board = i_Board;
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }
        }

        public bool CardValuesMatch
        {
            get
            {
                return m_CardValuesMatch;
            }
        }

        public Card PreviousCard
        {
            get
            {
                return m_PreviousCardSelection;
            }
        }

        public Card CurrentCard
        {
            get
            {
                return m_CurrentCardSelection;
            }
        }

        public void UpdateNextTurn(Card i_UserCardSelection)
        {
            if (m_IsFirstCardSelection)
            {
                m_PreviousCardSelection = i_UserCardSelection;
                m_PreviousCardSelection.IsHidden = false;
                m_IsFirstCardSelection = false;
            }
            else
            {
                m_CurrentCardSelection = i_UserCardSelection;
                m_CurrentCardSelection.IsHidden = false;
                m_IsFirstCardSelection = true;
                m_CardValuesMatch = m_CurrentCardSelection.CardValue == m_PreviousCardSelection.CardValue;
                if (m_CardValuesMatch)
                {
                    m_CurrentPlayer.PlayerScore++;
                }
                else
                {
                    m_CurrentPlayer = m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
                }
            }
        }

        public void ComputerTurn()
        {
            Random random = new Random();
            Card currentComputerChoice = m_Board[random.Next(r_BoardHeight), random.Next(r_BoardWidth)];
            while (!currentComputerChoice.IsHidden)
            {
                currentComputerChoice = m_Board[random.Next(r_BoardHeight), random.Next(r_BoardWidth)];
            }

            UpdateNextTurn(currentComputerChoice);
        }

        public bool IsCardLocationInputValid(string i_UserCardInput)
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

        public bool IsCardAlreadyChosen(string i_UserCardInput)
        {
            int colLetter = ConvertLetterToInt(i_UserCardInput[0]);
            int rowNum = ConvertNumberCharacterToInt(i_UserCardInput[1]);
            return !m_Board[colLetter - 1, rowNum - 1].IsHidden;
        }

        public int ConvertLetterToInt(char i_Char)
        {
            return (int)char.ToUpper(i_Char) - 64;
        }

        public int ConvertNumberCharacterToInt(char i_Char)
        {
            int.TryParse(i_Char.ToString(), out int numberIndexOnBoard);
            return numberIndexOnBoard;
        }
    }
}