using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GabrielCharacters
{
    internal class Program
    {
        static void Main()
        {
            List<Character> aliveChar = new List<Character>(); //список живых (активных) персонажей
            List<Character> deadChar = new List<Character>(); //список мёртвых персонажей
			Character playChar = new Character(); //объект для игры (чтобы избежать ошибок при переборе списка)

            Console.WriteLine("Для начала игры создайте хотя бы двух персонажей.\n");
            while (aliveChar.Count < 2) //создание минимального для игры количества персонажей
            {
                aliveChar.Add(new Character());
                Character last = aliveChar.Last();
                last.Choose(11, aliveChar, deadChar);
                Console.WriteLine();
            }
            Character: //создание дополнительных персонажей, если это требуется
            Console.WriteLine("Чтобы начать игру, нажмите Enter. Если хотите создать ещё персонажа, введите 'ещё' и нажмите Enter.");
            string answ = Console.ReadLine();
            Console.WriteLine();
            if (answ == "ещё")
            {
                aliveChar.Add(new Character());
                Character last = aliveChar.Last();
                last.Choose(11, aliveChar, deadChar);
                Console.WriteLine();
                goto Character;
            }

			Console.WriteLine("Игра началась.\n");
            CharChoice:
            Console.WriteLine("Выберите персонажа:");
            foreach (Character character in aliveChar) //вывод всех доступных для выбора персонажей
            {
                Console.WriteLine((aliveChar.IndexOf(character) + 1) + ":");
                character.Choose(1, aliveChar, deadChar);
                Console.WriteLine();
            }
            int charChoice = Convert.ToInt32(Console.ReadLine()) - 1;
			foreach (Character character in aliveChar) //выбор персонажа
            {
                if (charChoice == aliveChar.IndexOf(character))
                {
                    playChar = character; 
                }
            }
            Console.WriteLine();
            ActChoice:
			playChar.Choose(12, aliveChar, deadChar); //вывод информации о команде
			Console.WriteLine("Выберите действие:\n1 - информация о персонаже\n2 - переместиться по горизонтали\n3 - переместиться по вертикали\n4 - полечить союзника\n5 - восстановить здоровье\n6 - сменить команду персонажа\n7 - сменить персонажа\nEnter - закончить игру\n");
            string actChoice = Console.ReadLine(); //выбор действия с персонажем
            Console.WriteLine();
            switch (actChoice)
            {
                case "1":
                {
					playChar.Choose(1, aliveChar, deadChar);
					Console.WriteLine();
					goto ActChoice;
                }

                case "2":
                {
					playChar.Choose(2, aliveChar, deadChar);
					Console.WriteLine();
					goto ActChoice;
                }

                case "3":
                {
					playChar.Choose(3, aliveChar, deadChar);
					Console.WriteLine();
					goto ActChoice;
                }

                case "4":
                {
					playChar.Choose(4, aliveChar, deadChar);
					Console.WriteLine();
					goto ActChoice;
                }

                case "5":
                {
					playChar.Choose(5, aliveChar, deadChar);
					Console.WriteLine();
					goto ActChoice;
                }

                case "6":
                {
					playChar.Choose(6, aliveChar, deadChar);
					Console.WriteLine();
					goto ActChoice;
                }

                case "7":
                {
					goto CharChoice;
				}

				case "":
				{
					playChar.Choose(13, aliveChar, deadChar);
                    Console.Write("Для выхода нажмите Enter."); //выход из игры
                    Console.ReadLine();
                    break;
				}
			}
        }
    }
}