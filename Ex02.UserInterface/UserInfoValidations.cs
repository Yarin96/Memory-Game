namespace Ex02.UserInterface
{
    internal class UserInfoValidations
    {
        public static bool CheckIfValidName(string i_Name)
        {
            bool isValidFlag = true;
            for (int i = 0; i < i_Name.Length; i++)
            {
                if (!char.IsLetter(i_Name[i]))
                {
                    isValidFlag = false;
                    break;
                }
            }

            return i_Name != string.Empty && isValidFlag;
        }

        public static bool CheckIfValidGameModeInput(string i_GameModeChoice)
        {
            return i_GameModeChoice.ToUpper() == "P" || i_GameModeChoice.ToUpper() == "C";
        }

        public static bool CheckIfValidBoardSize(string i_BoardSize)
        {
            bool isValidFlag = false;
            if (int.TryParse(i_BoardSize, out int i_SizeNumber))
            {
                isValidFlag = i_SizeNumber >= 4 && i_SizeNumber <= 6;
            }

            return isValidFlag;
        }

        public static bool CheckIfOddMultiplication(string i_NumberStr1, string i_NumberStr2)
        {
            return (int.Parse(i_NumberStr1) * int.Parse(i_NumberStr2)) % 2 != 0;
        }
    }
}
