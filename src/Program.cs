using C_TicTacToe;

namespace C_TicTacToe
{
    internal class Program
    {
        public const char EMPTY = '.';
        static void Main(string[] args)
        {
            char[] field = {EMPTY, EMPTY, EMPTY,
                            EMPTY, EMPTY, EMPTY,
                            EMPTY, EMPTY, EMPTY};

            Game.Game game = new Game.Game();
            Computer.Computer computer = new Computer.Computer();
            Player.Player player = new Player.Player();
            Player.Player.Player2 player2 = new Player.Player.Player2();

            Console.WriteLine("\t\t ИГРА КРЕСТИКИ - НОЛИКИ");
            Console.WriteLine("Выбор соперника: 1 - компьютер; 2 - второй игрок;");
            int сhoosingAnOpponent = Int32.Parse(Console.ReadLine());

            while (!(сhoosingAnOpponent == 1 || сhoosingAnOpponent == 2))
            {
                Console.WriteLine("Выбор соперника: 1 - компьютер; 2 - второй игрок;");
                сhoosingAnOpponent = Int32.Parse(Console.ReadLine());
            }
            bool isFirstPlayer;

            if (сhoosingAnOpponent == 1)
            {
                isFirstPlayer = player.DetermineFirstPlayer();

                while (true)
                {
                    if (isFirstPlayer)
                    {
                        player.SetPositionForPlayer(field, 'X', "Ход игрока!");
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫ ВЫИГРАЛИ!!!");
                        computer.SelectPositionForComputer(field, '0');
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫИГРАЛ КОМПЬЮТЕР!!!");
                    }
                    else
                    {
                        computer.SelectPositionForComputer(field, 'X');
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫИГРАЛ КОМПЬЮТЕР!!!");
                        player.SetPositionForPlayer(field, '0', "Ход игрока!");
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫ ВЫИГРАЛИ!!!");
                    }
                }
            }

            if(сhoosingAnOpponent == 2)
            {
                isFirstPlayer = player2.DetermineFirstPlayer();

                while (true)
                {
                    if (isFirstPlayer)
                    {
                        player.SetPositionForPlayer(field, 'X', "Ход 1 игрока!");
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫИГРАЛ 1 ИГРОК!!!");
                        player2.SetPositionForPlayer(field, '0', "Ход 2 игрока!");
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫИГРАЛ 2 ИГРОК!!!");
                    }
                    else
                    {
                        player2.SetPositionForPlayer(field, 'X', "Ход 2 игрока!");
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫИГРАЛ 2 ИГРОК!!!");
                        player.SetPositionForPlayer(field, '0', "Ход 1 игрока!");
                        game.ShowField(field);
                        Console.WriteLine("*********");
                        game.Winner(field, "ВЫИГРАЛ 1 ИГРОК!!!");
                    }
                }
            }
        }
    }
}

namespace Player
{
    public class Player
    {
        Game.Game game = new Game.Game();
        
        public virtual bool DetermineFirstPlayer()
        {
            Console.WriteLine("Определение первого хода, " +
                "сделайте бросок кубика (нажмите Enter):");
            Console.ReadLine();
            int cubeUser = game.ThrowDice();
            Console.WriteLine($"Ваш бросок {cubeUser}");
            int cubeComp = game.ThrowDice();
            Console.WriteLine($"Бросок компьютера {cubeComp}");
            return cubeUser >= cubeComp;
        }

        public void SetPositionForPlayer(char[] field, char crossOrTic, 
            string message)
        {
            Console.WriteLine(message);
            int selection;
            do
            {
                Console.WriteLine("Выберете позицию на поле (от 1 до 9): ");
                selection = int.Parse(Console.ReadLine());

                if (selection < 1 || selection > 9)
                {
                    Console.WriteLine("Введите число от 1 до 9:");
                    continue;
                }

                if (!game.IsPositionEmpty(field, selection))
                {
                    Console.WriteLine("Поставьте крестик или нолик " +
                        "в пустую позицию");
                    continue;
                }

            } while ((selection < 1 || selection > 9) || 
                    !game.IsPositionEmpty(field, selection));

            game.SetPosition(field, crossOrTic, selection);
        }

        public class Player2 : Player
        {
            public override bool DetermineFirstPlayer()
            {
                Console.WriteLine("Определение первого хода, " +
                    "сделайте бросок кубика (нажмите Enter):");
                Console.ReadLine();
                int cubeUser1 = game.ThrowDice();
                Console.WriteLine($"Бросок первого игрока {cubeUser1}");
                int cubeUser2 = game.ThrowDice();
                Console.WriteLine($"Бросок второго игрока {cubeUser2}");
                return cubeUser1 >= cubeUser2;
            }
        }
    }
}

namespace Computer
{
    public class Computer
    {
        public void SelectPositionForComputer(char[] field, char crossOrTic)
        {
            Console.WriteLine("Ход компьютера!");
            Thread.Sleep(500);

            if (field[4] == C_TicTacToe.Program.EMPTY)
            {
                field[4] = crossOrTic;
            }
            else if (field[4] != C_TicTacToe.Program.EMPTY &&
                field[4] == field[7] && field[1] == C_TicTacToe.Program.EMPTY)
            {
                field[1] = crossOrTic;
            }
            else if (field[1] != C_TicTacToe.Program.EMPTY &&
                field[1] == field[4] && field[7] == C_TicTacToe.Program.EMPTY)
            {
                field[7] = crossOrTic;
            }
            else if (field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[3] && field[6] == C_TicTacToe.Program.EMPTY)
            {
                field[6] = crossOrTic;
            }
            else if (field[3] != C_TicTacToe.Program.EMPTY && 
                field[3] == field[6] && field[0] == C_TicTacToe.Program.EMPTY)
            {
                field[0] = crossOrTic;
            }
            else if (field[2] != C_TicTacToe.Program.EMPTY && 
                field[2] == field[5] && field[8] == C_TicTacToe.Program.EMPTY)
            {
                field[8] = crossOrTic;
            }
            else if (field[5] != C_TicTacToe.Program.EMPTY && 
                field[5] == field[8] && field[2] == C_TicTacToe.Program.EMPTY)
            {
                field[2] = crossOrTic;
            }
            else if (field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[1] && field[2] == C_TicTacToe.Program.EMPTY)
            {
                field[2] = crossOrTic;
            }
            else if (field[1] != C_TicTacToe.Program.EMPTY && 
                field[1] == field[2] && field[0] == C_TicTacToe.Program.EMPTY)
            {
                field[0] = crossOrTic;
            }
            else if (field[3] != C_TicTacToe.Program.EMPTY && 
                field[3] == field[4] && field[5] == C_TicTacToe.Program.EMPTY)
            {
                field[5] = crossOrTic;
            }
            else if (field[4] != C_TicTacToe.Program.EMPTY && 
                field[4] == field[5] && field[3] == C_TicTacToe.Program.EMPTY)
            {
                field[3] = crossOrTic;
            }
            else if (field[6] != C_TicTacToe.Program.EMPTY && 
                field[6] == field[7] && field[8] == C_TicTacToe.Program.EMPTY)
            {
                field[8] = crossOrTic;
            }
            else if (field[7] != C_TicTacToe.Program.EMPTY && 
                field[7] == field[8] && field[6] == C_TicTacToe.Program.EMPTY)
            {
                field[6] = crossOrTic;
            }
            else if (field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[4] && field[8] == C_TicTacToe.Program.EMPTY)
            {
                field[8] = crossOrTic;
            }
            else if (field[4] != C_TicTacToe.Program.EMPTY && 
                field[4] == field[8] && field[0] == C_TicTacToe.Program.EMPTY)
            {
                field[0] = crossOrTic;
            }
            else if (field[2] != C_TicTacToe.Program.EMPTY &&
                field[2] == field[4] && field[6] == C_TicTacToe.Program.EMPTY)
            {
                field[6] = crossOrTic;
            }
            else if (field[4] != C_TicTacToe.Program.EMPTY && 
                field[4] == field[6] && field[2] == C_TicTacToe.Program.EMPTY)
            {
                field[2] = crossOrTic;
            }
            else if (field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[2] && field[1] == C_TicTacToe.Program.EMPTY)
            {
                field[1] = crossOrTic;
            }
            else if (field[6] != C_TicTacToe.Program.EMPTY && 
                field[6] == field[8] && field[7] == C_TicTacToe.Program.EMPTY)
            {
                field[7] = crossOrTic;
            }
            else if (field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[6] && field[3] == C_TicTacToe.Program.EMPTY)
            {
                field[3] = crossOrTic;
            }
            else if (field[2] != C_TicTacToe.Program.EMPTY && 
                field[2] == field[8] && field[5] == C_TicTacToe.Program.EMPTY)
            {
                field[5] = crossOrTic;
            }
            else
            {
                for (int i = 0; i < field.Length; i++)
                {
                    if (field[i] == C_TicTacToe.Program.EMPTY)
                    {
                        field[i] = crossOrTic;
                        break;
                    }
                }
            }
        }
    }
}

namespace Game
{
    public class Game
    {
        public int ThrowDice()
        {
            Random rnd = new Random();
            return rnd.Next(1, 7);
        }
        public void ShowField(char[] field)
        {
            for (int i = 0; i < field.Length; i++)
            {
                Console.Write(field[i] + " ");

                if (i == 2 || i == 5)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        public bool IsPositionEmpty(char[] field, int selection)
        {
            for (int i = 0; i < field.Length; i++)
            {
                if (i == (selection - 1) && field[i] == C_TicTacToe.Program.EMPTY)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetPosition(char[] field, char crossOrTic, int selection)
        {
            for (int i = 0; i < field.Length; i++)
            {
                if (i == (selection - 1))
                {
                    field[i] = crossOrTic;
                }
            }
        }

        public bool Matches(char[] field)
        {
            if (field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[1] && field[1] == field[2] ||
                field[3] != C_TicTacToe.Program.EMPTY && 
                field[3] == field[4] && field[4] == field[5] ||
                field[6] != C_TicTacToe.Program.EMPTY && 
                field[6] == field[7] && field[7] == field[8] ||
                field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[3] && field[3] == field[6] ||
                field[1] != C_TicTacToe.Program.EMPTY && 
                field[1] == field[4] && field[4] == field[7] ||
                field[2] != C_TicTacToe.Program.EMPTY && 
                field[2] == field[5] && field[5] == field[8] ||
                field[0] != C_TicTacToe.Program.EMPTY && 
                field[0] == field[4] && field[4] == field[8] ||
                field[2] != C_TicTacToe.Program.EMPTY && 
                field[2] == field[4] && field[4] == field[6])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemainingMoves(char[] field)
        {
            for (int i = 0; i < field.Length; i++)
            {
                if (field[i] == C_TicTacToe.Program.EMPTY)
                {
                    return true;
                }
            }
            return false;
        }

        public void Winner(char[] field, string message)
        {
            if (Matches(field))
            {
                Console.WriteLine(message);
                Environment.Exit(0);    
            }
            if (!RemainingMoves(field))
            {
                Console.WriteLine("НИЧЬЯ!!!");
                Environment.Exit(0);
            }
        }
    }
}
