using System;
using Ex02.Enums;

namespace Ex02.Logic
{
    public class GameLogic
    {
        private readonly int r_BoardRows;
        private readonly int r_BoardColumns;
        private bool m_IsFirstCardSelection;
        private bool m_CardValuesMatch;
        private Card m_CurrentCardSelection;
        private Card m_PreviousCardSelection;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private eGameMode m_GameMode;

        public GameLogic(Player i_Player1, Player i_Player2, eGameMode i_GameMode, int i_BoardWidth, int i_BoardHeight)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_CurrentPlayer = i_Player1;
            m_GameMode = i_GameMode;
            r_BoardColumns = i_BoardWidth;
            r_BoardRows = i_BoardHeight;
            m_IsFirstCardSelection = true;
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public bool CardValuesMatch
        {
            get { return m_CardValuesMatch; }
        }

        public Card PreviousCard
        {
            get { return m_PreviousCardSelection; }
        }

        public Card CurrentCard
        {
            get { return m_CurrentCardSelection; }
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
            int randomRow = random.Next(r_BoardRows);
            int randomColumn = random.Next(r_BoardColumns);
        }
    }
}