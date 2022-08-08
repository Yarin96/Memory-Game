namespace Ex02.Logic
{
    public class Player
    {
        private ePlayerType m_PlayerType;
        private string m_PlayerName;
        private int m_PlayerScore;

        public Player(ePlayerType i_Type, string i_PlayerName)
        {
            m_PlayerType = i_Type;
            m_PlayerName = i_PlayerName;
            m_PlayerScore = 0;
        }

        public ePlayerType PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public string PlayerName
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }

        public int PlayerScore
        {
            get { return m_PlayerScore; }
            set { m_PlayerScore = value; }
        }
    }
}
