using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_seabattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Player, enter your name: ");
            string player = Console.ReadLine();
            Console.Clear();
            int count_score = 0;
            int count_score_comp = 0;
            string computer = "Bot";
            
            char[] letters = { ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            int[] nums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            char[,] battleFieldarray = new char[10, 10];
            char[,] cbattleFieldarray = new char[10, 10];
            char[,] monarr1 = new char[10, 10];
            char[,] monarr2 = new char[10, 10];
            char[,] battleField = createbattlefield(battleFieldarray);
            char[,] compbattleField = createbattlefield(cbattleFieldarray);
            char[,] monitorplayer = createbattlefield(monarr1);
            char[,] monitorcomp = createbattlefield(monarr2);
            
            drawfield(letters, nums, battleFieldarray,player);
            drawComputerField(letters, nums,cbattleFieldarray,computer);
            Settingship(battleField,letters,nums,cbattleFieldarray, computer,player);
            SettinComputership(compbattleField,letters,nums,cbattleFieldarray,computer,monitorcomp);

            Console.Write($"Enter the difficulty level\n1.Easy\n2.Hard\n");
            int difficulty = int.Parse(Console.ReadLine());

            bool endgame = false;

            while (ending(monitorplayer, monitorcomp, player, computer))
            {
                playermove(player, monitorcomp, monitorplayer, letters, nums, compbattleField, ref count_score, endgame, computer);
                if (endgame == true || ending(monitorplayer, monitorcomp, player, computer) == false)
                {
                    break;
                }
                if (difficulty == 1)
                {
                    botmovedif1(computer, monitorplayer,monitorcomp, letters, nums, battleField, ref count_score_comp, endgame,player);
                }else if (difficulty==2)
                {
                    botmovedif2(computer, monitorplayer,monitorcomp, letters, nums, battleField, ref count_score_comp, endgame,player);
                }
                if (endgame == true || ending(monitorplayer, monitorcomp, player, computer) == false)
                {
                    break;
                }
            }

            Console.ReadLine();
        }
        static char[,] createbattlefield(char[,] fieldarr)
        {
            for (int i = 0; i < fieldarr.GetLength(0); i++)
            {
                for (int j = 0; j < fieldarr.GetLength(1); j++)
                {
                    fieldarr[i, j] = '_';

                }
            }
            return fieldarr;
        }
        static void Settingship(char[,] battleField, char[] letters, int[] nums, char[,]cbattleFieldarray, string computer,string player)
        {
            bool bot = false;
            bool exc = true;
            int inputship = 1;
            int iterarecond = 1;
            bool exception = false;
            bool exc_cross = false;
            for (int i = 4; i >= 1; i--)
            {
                while (iterarecond <= Math.Abs(i - (i - inputship)))
                {
                    do
                    {
                        try
                        {
                            Console.Write($"Player 1: Enter horizontal position for {i}-deck ship (a-j): ");
                            char hletter = Convert.ToChar(Console.ReadLine());
                            int h = letterToNum(hletter);
                            Console.Write($"Player 1: Enter vertical position for {i}-deck ship (0 - 9): ");
                            int v = Convert.ToInt32(Console.ReadLine());
                            Console.Write($"Enter rotation: \n1.Verical\n2.Horizontal\n");
                            int rotate = Convert.ToInt32(Console.ReadLine());
                            switch (rotate)
                            {
                                case 1:
                                    rotateHor(ref battleField, h, v, i, i, ref exception, ref exc_cross, bot);
                                    exc = true;
                                    break;
                                case 2:
                                    rotateVert(ref battleField, h, v, i, i, ref exception, ref exc_cross, bot);
                                    exc = true;
                                    break;
                                default:
                                    Console.WriteLine("Wrong rotation! Enter 1 or 2");
                                    exc = false;
                                    break;
                            }
                            if (exception != true && exc_cross != true && exc != false)
                            {
                                Console.Clear();
                                drawfield(letters, nums, battleField, player);
                                drawComputerField(letters, nums, cbattleFieldarray,computer);
                            }
                            if (exc != false)
                            {
                                exc = true;
                            }
                        }
                        catch
                        {
                            exc = false;
                            Console.WriteLine("Please, enter correct input:(");
                        }


                    } while (exception == true || exc_cross == true || exc == false);
                    iterarecond++;
                }
                inputship++;
                iterarecond = 1;
            }
        }
        static void SettinComputership(char[,] battleField, char[] letters, int[] nums, char[,]cbattleFieldarray,string computer, char[,] monitorcomp)
        {
            bool bot = true;
            Random randcor = new Random();
            int inputship = 1;
            int iteratecond = 1;
            bool exception = false;
            bool exc_cross = false;
            for (int i = 4; i >= 1; i--)
            {
                while (iteratecond <= Math.Abs(i - (i - inputship)))
                {
                    do
                    {
                        int i1 = randcor.Next(0, 10);
                        int i2 = randcor.Next(0, 10);
                        int rotate = randcor.Next(1, 3);
                        if (rotate == 2)
                        {
                            rotateVert(ref battleField, i1, i2, i, i, ref exception, ref exc_cross,bot);
                        }
                        if (rotate == 1)
                        {
                            rotateHor(ref battleField, i1, i2, i, i, ref exception, ref exc_cross, bot);
                        }

                    } while (exception == true || exc_cross == true);
                    iteratecond++;
                }
                inputship++;
                iteratecond = 1;
            }
            drawComputerField(letters, nums, monitorcomp,computer);
        }
        static void drawfield(char[] letters, int[] nums, char[,] battleFieldarray, string player)
        {
            Console.WriteLine(player);
            for (int i = 0; i < letters.Length; i++)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(letters[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < battleFieldarray.GetLength(0); i++)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(nums[i] + " ");
                for (int j = 0; j < battleFieldarray.GetLength(1); j++)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(battleFieldarray[i, j] + " ");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        static void drawComputerField(char[] letters, int[] nums, char[,] cbattleFieldarray, string bot)
        {
            Console.SetCursorPosition(30, 0);
            Console.WriteLine(bot);
            Console.SetCursorPosition(30, 1);
            for (int i = 0; i < letters.Length; i++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(letters[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < cbattleFieldarray.GetLength(0); i++)
            {
                Console.SetCursorPosition(30, i + 2);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(nums[i] + " ");
                for (int j = 0; j < cbattleFieldarray.GetLength(1); j++)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(cbattleFieldarray[i, j] + " ");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        static int letterToNum(char horposition)
        {
            int horposnum = 0;
            switch (horposition)
            {
                case 'a':
                    horposnum = 0;
                    break;
                case 'b':
                    horposnum = 1;
                    break;
                case 'c':
                    horposnum = 2;
                    break;
                case 'd':
                    horposnum = 3;
                    break;
                case 'e':
                    horposnum = 4;
                    break;
                case 'f':
                    horposnum = 5;
                    break;
                case 'g':
                    horposnum = 6;
                    break;
                case 'h':
                    horposnum = 7;
                    break;
                case 'i':
                    horposnum = 8;
                    break;
                case 'j':
                    horposnum = 9;
                    break;
                default:
                    horposnum = 10;
                    break;
            }
            return horposnum;
        }
        static void rotateVert(ref char[,] field, int h, int v, int i, int j, ref bool exc, ref bool exc_c, bool bot)
        {
            int plus = 0;
            try
            {
                while (j > 0)
                {
                    if (field[v, h + plus] != '#' && availability(i, field, h, v, 2) == true)
                    {
                        exc_c = false;
                        plus++;
                        j--;
                        continue;
                    }
                    else
                    {
                        exc_c = true;
                        if (bot == false)
                        {
                            Console.WriteLine("Your new ship crosses the area of a previous one:( Please, re-enter coordinates or rotation!");
                        }
                        break;
                    }
                }
                plus = 0;
                while (i > 0 && exc_c == false)
                {
                    field[v, h + plus] = '#';
                    plus++;
                    i--;
                    exc = false;
                }
            }
            catch
            {
                exc = true;
                if (bot == false)
                {
                    Console.WriteLine("Re-entner the coordinates of your ship:)");
                }
            }
        }
        static void rotateHor(ref char[,] field, int h, int v, int i, int j, ref bool exc, ref bool exc_c, bool bot)
        {
            int plus = 0;
            try
            {
                while (j > 0)
                {
                    if (field[(v + plus), h] != '#' && availability(i, field, h, v, 1) == true)
                    {
                        exc_c = false;
                        plus++;
                        j--;
                        continue;
                    }
                    else
                    {
                        exc_c = true;
                        if (bot == false)
                        {
                            Console.WriteLine("Your new ship crosses the previous one:( Please, re-enter coordinates or rotation!");
                        }
                        break;
                    }
                }
                plus = 0;
                while (i > 0 && exc_c == false)
                {

                    field[(v + plus), h] = '#';
                    plus++;
                    i--;
                    exc = false;

                }
            }
            catch
            {
                exc = true;
                if (bot)
                {
                    Console.WriteLine("Re-entner the coordinates of your ship:)");
                }
            }
        }
        static bool availability(int deck, char[,] battleField, int h, int v, int rotation)
        {
            int vertavail = 0;
            int horavail = 0;
            for (int i = 0; i < deck; i++)
            {
                if (rotation == 1)
                {
                    vertavail = i;
                    if (v + vertavail < 10 && h + 1 < 10 && h - 1 >= 0)
                    {
                        if (battleField[v + vertavail, h + 1] == '#' || battleField[v + vertavail, h - 1] == '#')
                        {
                            return false;
                        }
                    }
                    if (h + 1 >= 10 && v + vertavail < 10)
                    {
                        if (battleField[v + vertavail, h - 1] == '#')
                        {
                            return false;
                        }
                    }
                    if (h - 1 < 0 && v + vertavail < 10)
                    {
                        if (battleField[v + vertavail, h + 1] == '#')
                        {
                            return false;
                        }
                    }
                    if (v - 1 >= 0 && v + vertavail + 1 < 10)
                    {
                        if (battleField[v + vertavail + 1, h] == '#' || battleField[v - 1, h] == '#')
                        {
                            return false;
                        }
                    }
                    if (v - 1 < 0)
                    {
                        if (battleField[v + vertavail + 1, h] == '#')
                        {
                            return false;
                        }
                    }
                    if (v + vertavail + 1 >= 10)
                    {
                        if (battleField[v - 1, h] == '#')
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    horavail = i;
                    if (h + horavail < 10 && v + 1 < 10 && v - 1 >= 0)
                    {
                        if (battleField[v + 1, h + horavail] == '#' || battleField[v - 1, h + horavail] == '#')
                        {
                            return false;
                        }
                    }
                    if (v + 1 >= 10 && h + horavail < 10)
                    {
                        if (battleField[v - 1, h + horavail] == '#')
                        {
                            return false;
                        }
                    }
                    if (v - 1 < 0 && h + horavail < 10)
                    {
                        if (battleField[v + 1, h + horavail] == '#')
                        {
                            return false;
                        }
                    }
                    if (h - 1 >= 0 && h + horavail + 1 < 10)
                    {
                        if (battleField[v, h - 1] == '#' || battleField[v, h + horavail + 1] == '#')
                        {
                            return false;
                        }
                    }
                    if (h - 1 < 0)
                    {
                        if (battleField[v, h + horavail + 1] == '#')
                        {
                            return false;
                        }
                    }
                    if (h + horavail + 1 >= 10)
                    {
                        if (battleField[v, h - 1] == '#')
                        {
                            return false;
                        }
                    }

                }
            }
            return true;
        }
        static void playermove(string player, char[,] monitorcomp,char[,]monitorplayer, char[] letters, int[] nums, char[,] battlefieldcomp, ref int countscore, bool endgame, string computer)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"{player}, your turn!!!");
                    Console.Write("Enter horizontal coordinate for your move (a - j): ");
                    char hletter = Convert.ToChar(Console.ReadLine());
                    int h = letterToNum(hletter);
                    Console.Write("Enter vertical coordinate for your move (0 - 9): ");
                    int v = Convert.ToInt32(Console.ReadLine());
                    if (battlefieldcomp[v, h] == '_')
                    {
                        monitorcomp[v, h] = 'o';
                        Console.Clear();
                        drawfield(letters, nums, monitorplayer,player);
                        drawComputerField(letters, nums, monitorcomp,computer);
                        break;
                    }
                    else if (battlefieldcomp[v, h] == 'o')
                    {
                        Console.WriteLine("Please, re-enter the coordinate)");
                        continue;
                    }
                    else if (battlefieldcomp[v, h] == '#')
                    {
                        monitorcomp[v, h] = 'X';
                        Console.Clear();
                        drawfield(letters, nums, monitorplayer, player);
                        drawComputerField(letters, nums, monitorcomp,computer);
                        countscore++;
                        if (countscore == 20)
                        {
                            endgame = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Your input is incorrect. Please, change it:)");
                    continue;
                }
            }
        }
        static void botmovedif1(string comp, char[,] monitorplayer,char[,]monitorcomp, char[] letters, int[] nums, char[,] battlefieldplayer, ref int count, bool endgame, string player)
        {
            while (true)
            {
                Console.WriteLine($"{comp}, your turn!!!");
                Random rand = new Random();
                int h = rand.Next(0, 10);
                int v = rand.Next(0, 10);
                if (battlefieldplayer[v, h] == '_')
                {
                    monitorplayer[v, h] = 'o';
                    Console.Clear();
                    drawfield(letters, nums, monitorplayer, player);
                    drawComputerField(letters, nums, monitorcomp, comp);
                    break;
                }
                else if (battlefieldplayer[v, h] == 'o')
                {
                    continue;
                }
                else if (battlefieldplayer[v, h] == '#')
                {
                    monitorplayer[v, h] = 'X';
                    count++;
                    Console.Clear();
                    drawfield(letters, nums, monitorplayer, player);
                    drawComputerField(letters, nums, monitorcomp, comp);
                    if (count == 20)
                    {
                        endgame = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        static void botmovedif2(string comp, char[,] monitorplayer,char[,] monitorcomp, char[] letters, int[] nums, char[,] battlefieldplayer, ref int count, bool endgame, string player)
        {
            Random rand = new Random();
            int h = rand.Next(0, 10);
            int v = rand.Next(0, 10);
            
            while (true)
            {
                try {
                    if (battlefieldplayer[v, h] == '_')
                    {
                        monitorplayer[v, h] = 'o';
                        Console.Clear();
                        drawfield(letters, nums, monitorplayer, player);
                        drawComputerField(letters, nums, monitorcomp, comp);
                        break;
                    }
                    else if (battlefieldplayer[v, h] == 'o')
                    {
                        continue;
                    }
                    else if (battlefieldplayer[v, h] == '#')
                    {
                        int direction = rand.Next(1, 3);
                        monitorplayer[v, h] = 'X';
                        count++;
                        if (count == 20)
                        {
                            Console.Clear();
                            drawfield(letters, nums, monitorplayer, player);
                            drawComputerField(letters, nums, monitorcomp, comp);
                            endgame = true;
                            break;
                        }
                        for (int i = 1; ; i++)
                        {
                            if (direction == 1 && v + i < 10)
                            {
                                if (battlefieldplayer[v + i, h] == '_')
                                {
                                    monitorplayer[v + i, h] = 'o';
                                    break;
                                }
                                else if (battlefieldplayer[v + i, h] == '#')
                                {
                                    monitorplayer[v + i, h] = 'X';
                                    count++;
                                    if (count == 20)
                                    {
                                        endgame = true;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                            if (direction == 2 && h + i < 10)
                            {
                                if (battlefieldplayer[v, h + i] == '_')
                                {
                                    monitorplayer[v, h + i] = 'o';
                                    break;
                                }
                                else if (battlefieldplayer[v, h + i] == '#')
                                {
                                    monitorplayer[v, h + i] = 'X';
                                    count++;
                                    if (count == 20)
                                    {
                                        endgame = true;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        Console.Clear();
                        drawfield(letters, nums, monitorplayer, player);
                        drawComputerField(letters, nums, monitorcomp, comp);
                        break;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
        static bool ending(char[,] monitorplayer, char[,] monitorcomp, string player, string comp)
        {
            int countplayer = 0;
            int countcomp = 0;
            for (int i = 0; i < monitorplayer.GetLength(0); i++)
            {
                for (int j = 0; j < monitorplayer.GetLength(1); j++)
                {
                    if (monitorplayer[i, j] == 'X')
                    {
                        countplayer++;
                    }
                }
            }
            for (int i = 0; i < monitorcomp.GetLength(0); i++)
            {
                for (int j = 0; j < monitorcomp.GetLength(1); j++)
                {
                    if (monitorcomp[i, j] == 'X')
                    {
                        countcomp++;
                    }
                }
            }
            if (countplayer == 20)
            {
                Console.WriteLine($"{comp}, congratulations!!! You are the winner:)");
                return false;
            }
            if (countcomp == 20)
            {
                Console.WriteLine($"{player}, congratulations!!! You are the winner:)");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
