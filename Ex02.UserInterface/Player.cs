using Ex02.Enums;

namespace Ex02.UserInterface
{
    internal class Player
    {
        private ePlayerType m_Type;
        private string m_PlayerName;
        private int m_PlayerScore;

        public Player(ePlayerType i_Type, string i_PlayerName)
        {
            m_Type = i_Type;
            m_PlayerName = i_PlayerName;
            m_PlayerScore = 0;
        }

        public string Name
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }
    }
}
