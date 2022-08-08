namespace Ex02.Logic
{
    public class Card
    {
        private char m_CardValue;
        private bool m_IsHidden;

        public Card(char i_CardValue, bool i_IsHidden)
        {
            m_CardValue = i_CardValue;
            m_IsHidden = i_IsHidden;
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
    }
}
