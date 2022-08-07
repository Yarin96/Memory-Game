using System;
using Ex02.ConsoleUtils;
using Ex02.Enums;

namespace Ex02.UserInterface
{
    internal class UserInterface
    {
        private string m_FirstPlayerName;
        private string m_SecondPlayerName;
        private string m_GameModeChoice;
        private string m_BoardWidth;
        private string m_BoardHeight;
        private ePlayerType m_PlayerType;
        private eGameMode m_GameMode;
        private Board m_Board;

        public UserInterface()
        {
            this.m_FirstPlayerName = string.Empty;
            this.m_SecondPlayerName = string.Empty;
            this.m_GameModeChoice = string.Empty;
            this.m_BoardWidth = string.Empty;
            this.m_BoardHeight = string.Empty;
        }

        public void InitiateGame()
        {
            gameTitle();
            collectUserInfoAndGameProps();
            Player player1 = new Player(ePlayerType.Human, m_FirstPlayerName);
            Player player2 = new Player(m_PlayerType, m_SecondPlayerName);
            m_Board = new Board(player1, player2, m_GameMode, int.Parse(m_BoardWidth), int.Parse(m_BoardHeight));
            m_Board.PrintBoard();
            playerTurn();
        }

        private void gameTitle()
        {
            string introduction = string.Format("     |~~~~~~~~~~~~~~~~~~~|\n" +
                                    "     | Memory Card Game! |\n" +
                                    "     |~~~~~~~~~~~~~~~~~~~|\n");
            Console.WriteLine(introduction);
        }

        private void collectUserInfoAndGameProps()
        {
            bool validationFlag = false;
            do
            {
                if (m_FirstPlayerName == string.Empty)
                {
                    Console.Write("-> Please enter your name: ");
                    m_FirstPlayerName = Console.ReadLine();
                    if (!UserInfoValidations.CheckIfValidName(m_FirstPlayerName))
                    {
                        Console.WriteLine("\nName is not valid. It should be without any special symbols or numbers.\n");
                        m_FirstPlayerName = string.Empty;
                        continue;
                    }

                    Console.WriteLine("\nWelcome, {0}!\n", m_FirstPlayerName);
                }

                if (m_GameModeChoice == string.Empty)
                {
                    Console.Write("-> Type to choose a Game Mode ~ (P = Vs Player) / (C = Vs Computer): ");
                    m_GameModeChoice = Console.ReadLine();
                    if (!UserInfoValidations.CheckIfValidGameModeInput(m_GameModeChoice))
                    {
                        Console.WriteLine("\nGame Mode entered is not valid. Please try again and type P or C.\n");
                        m_GameModeChoice = string.Empty;
                        continue;
                    }
                }

                if (m_GameModeChoice == "P" && m_SecondPlayerName == string.Empty)
                {
                    m_PlayerType = ePlayerType.Human;
                    m_GameMode = eGameMode.PlayerVsPlayer;
                    Console.Write("\n-> Please enter second player name: ");
                    m_SecondPlayerName = Console.ReadLine();
                    if (!UserInfoValidations.CheckIfValidName(m_SecondPlayerName))
                    {
                        Console.WriteLine("\nName is not valid. It should be without any special symbols or numbers.\n");
                        m_SecondPlayerName = string.Empty;
                        continue;
                    }

                    Console.WriteLine("\nYou are playing against {0}!\n", m_SecondPlayerName);
                }
                else
                {
                    m_PlayerType = ePlayerType.Computer;
                    m_GameMode = eGameMode.PlayerVsComputer;
                    m_SecondPlayerName = "Computer";
                    Console.WriteLine("\nYou are playing against the Computer!\n");
                }

                if (m_BoardWidth == string.Empty || m_BoardHeight == string.Empty)
                {
                    Console.Write("-> Please enter the Width of the table (Min 4, Max 6): ");
                    m_BoardWidth = Console.ReadLine();
                    Console.Write("-> Please enter the Height of the table (Min 4, Max 6): ");
                    m_BoardHeight = Console.ReadLine();
                    if (!UserInfoValidations.CheckIfValidBoardSize(m_BoardWidth)
                        || !UserInfoValidations.CheckIfValidBoardSize(m_BoardHeight))
                    {
                        string error = string.Format(
                            "\nInvalid input. May be caused because:\n" +
                            "   - Input entered is below 4 or above 6.\n" +
                            "   - Input entered is not a number.\n" +
                            "Please try again.\n");
                        Console.WriteLine(error);
                        m_BoardWidth = string.Empty;
                        m_BoardHeight = string.Empty;
                        continue;
                    }
                    else
                    {
                        if (UserInfoValidations.CheckIfOddMultiplication(m_BoardWidth, m_BoardHeight))
                        {
                            string error = string.Format(
                                "\nInvalid input. May be caused because:\n" +
                                "   - {0}x{1} results in an odd number.\n" +
                                "Please try again.\n",
                                m_BoardWidth,
                                m_BoardHeight);
                            Console.WriteLine(error);
                            m_BoardWidth = string.Empty;
                            m_BoardHeight = string.Empty;
                            continue;
                        }
                    }
                }

                validationFlag = true; /// When compiler reaches here, all info is valid.
            }
            while (!validationFlag);
            Console.WriteLine("\nWe're good to go! Press 'enter' to start the game ...");
            Console.ReadLine();
            Screen.Clear();
        }

        private void playerTurn()
        {
            Console.Write("Please choose a card (Format of [Capital Letter, Number]): ");
            string choice = Console.ReadLine();
            bool isLegal = m_Board.isCellLocationInputValid(choice);
            bool isChosen = true;
            while (!isLegal || isChosen)
            {
                if (!isLegal)
                {
                    Console.WriteLine("Not legal option, please choose again:");
                    choice = Console.ReadLine();
                    isLegal = m_Board.isCellLocationInputValid(choice);
                    continue;
                }
                else
                {
                    isChosen = m_Board.isCellAlreadyChosen(choice);
                    if (!isChosen)
                    {
                        continue;
                    }

                    Console.WriteLine("Card already been discovered, please choose again:");
                    choice = Console.ReadLine();
                    isLegal = m_Board.isCellLocationInputValid(choice);
                }
            }
        }
    }
}
