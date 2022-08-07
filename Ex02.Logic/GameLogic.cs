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
        private eGameMode m_GameMode;

        public GameLogic(Player i_Player1, Player i_Player2, eGameMode i_GameMode, int i_Width, int i_Height)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_IsFirstCardSelection = true;
            r_BoardColumns = i_Width;
            r_BoardRows = i_Height;
            m_GameMode = i_GameMode;
        }

        public void RunGame()
        {
            //int totalCards = (r_BoardColumns * r_BoardRows) / 2;
            //int remainingCards = m_Player1.PlayerScore + m_Player2.PlayerScore;
            //while (remainingCards != totalCards)
            //{
            //    if (m_GameMode == eGameMode.PlayerVsComputer)
            //    {

            //    }
            //    else
            //    {

            //    }
            //}
            Console.WriteLine("Running logic here");
        }

        public Player GetPlayer1()
        {
            return m_Player1;
        }

        public Player GetPlayer2()
        {
            return m_Player2;
        }

        public void UpdateTurn(Card i_UserCardSelection)
        {
            if (m_IsFirstCardSelection)
            {
                m_PreviousCardSelection = i_UserCardSelection;
                i_UserCardSelection.IsHidden = false;
                m_IsFirstCardSelection = false;
            }
            else
            {
                m_CurrentCardSelection = i_UserCardSelection;
                i_UserCardSelection.IsHidden = false;
                m_IsFirstCardSelection = true;
                m_CardValuesMatch = m_CurrentCardSelection.CardValue == m_PreviousCardSelection.CardValue;
                if (m_CardValuesMatch)
                {
                    m_Player1.PlayerScore++;
                }
                else
                {
                    m_PreviousCardSelection.IsHidden = true;
                    m_CurrentCardSelection.IsHidden = true;
                }
            }
        }

        private void computerTurn()
        {
            Random random = new Random();
            int randomRow = random.Next(r_BoardRows);
            int randomColumn = random.Next(r_BoardColumns);
        }


    }
}
