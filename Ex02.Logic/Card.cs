namespace Ex02.Logic
{
    public class Card
    {
        private char m_CardValue;
        private bool m_IsHidden;
        private int m_ColPosition;
        private int m_RowPosition;

        public Card(char i_CardValue, bool i_IsHidden, int i_ColPosition, int i_RowPosition)
        {
            m_CardValue = i_CardValue;
            m_IsHidden = i_IsHidden;
            m_ColPosition = i_ColPosition;
            m_RowPosition = i_RowPosition;
        }

        public char CardValue
        {
            get { return m_CardValue; }
            set { m_CardValue = value; }
        }

        public bool IsHidden
        {
            get { return m_IsHidden; }
            set { m_IsHidden = value; }
        }

        internal static void foldCard()
        {
        }

        internal static void unfoldCard()
        {
        }

        internal bool IsCardValueEqual(Card i_Card1, Card i_Card2)
        {
            bool validFlag = true;
            return validFlag;
        }
    }
}
