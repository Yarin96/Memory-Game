using System;
using Ex02.Enums;

namespace Ex02.Logic
{
    public class GameLogic
    {
        private bool m_IsFirstCardSelection;
        private bool m_CardValuesMatch;
        private Card m_CurrentCardSelection;
        private Card m_PreviousCardSelection;
        private Player m_Player1;
        private Player m_Player2;

        public GameLogic(Player i_Player1, Player i_Player2, eGameMode i_GameMode, int i_Width, int i_Height)
        {
            m_IsFirstCardSelection = true;
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
            }
        }
    }
}
