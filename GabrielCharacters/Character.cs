using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GabrielCharacters
{
    internal class Character
    {
        private string _name; //имя персонажа
        private int _xpMax; //максимально возможное количество жизней
        private int _xpCur; //текущее количество жизней
        private int _punch; //сила удара
        private bool _team; //принадлежность к команде
        private int _x; //координата расположения x
        private int _y; //координата расположения y
        private int _vict; //победы в драках
        private int _victChange; //победы в драках (для восстановления здоровья)

        /// <summary>
        /// Создание персонажа
        /// </summary>
        public Character()
        {
            this._name = "";
            this._xpMax = 0;
            this._xpCur = 0;
            this._punch = 0;
            this._team = true;
            this._x = 0;
            this._y = 0;
            this._vict = 0;
            this._victChange = 0;
        }

        /// <summary>
        /// Выбор приватного метода
        /// </summary>
        /// <param name="actCoice"></param>
        /// <param name="aliveChar"></param>
        /// <param name="deadChar"></param>
        public void Choose(int actCoice, List<Character> aliveChar, List<Character> deadChar)
        {
            switch (actCoice)
            {
                case 11: //заполнение информации о персонаже
                {
                    InfoIn(aliveChar);
                    break;
                }

                case 12: //вывод информации о своей команде
                {
                    TeamOut(aliveChar);
                    break;
                }

                case 13: //вывод статистики
                {
                    Statistics(aliveChar);
                    break;
                }

                case 1: //вывод информации о своём персонаже
                {
                    InfoOut();
                    break;
                }

                case 2: //перемещение своего персонажа по горизонтали
                {
                    MoveX(aliveChar, deadChar);
                    break;
                }

                case 3: //перемещение своего персонажа по вертикали
                {
                    MoveY(aliveChar, deadChar);
                    break;
                }

                case 4: //лечение другого персонажа
                {
                    Heal(aliveChar);
                    break;
                }

                case 5: //восстановление здоровья своего персонажа
                {
                    TotalHeal();
                    break;
                }

                case 6: //смена команды своего персонажа
                {
                    TeamChange();
                    break;
                }
			}
        }

        /// <summary>
        /// Заполнение информации о персонаже
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xpMax"></param>
        /// <param name="team"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void InfoIn(List<Character> aliveChar)
        {
            Name:
            Console.Write("Введите имя персонажа (не может остаться пустым): ");
            string name = Console.ReadLine();
            if (name == "")
            {
                goto Name;
            }
            foreach (Character character in aliveChar)
            {
                if (name == character._name)
                {
                    Console.WriteLine("Персонаж с таким именем уже есть. Придумайте другое.");
                    goto Name;
                } 
            }
            this._name = name;

            while (this._xpMax <= 0)
            {
                Console.Write("Введите здоровье персонажа (должно быть больше нуля): ");
                int xpMax = Convert.ToInt32(Console.ReadLine());
                this._xpMax = xpMax;
                this._xpCur = xpMax;
            }

            Random random = new Random();
            this._punch = random.Next(1, this._xpMax);

            Console.WriteLine("Выберите команду: 1 2");
            int team = Convert.ToInt32(Console.ReadLine());
            switch (team)
            {
                case 1:
                {
                    this._team = true;
                    break;
                }

                case 2:
                {
                    this._team = false;
                    break;
                }

                default:
                {
                    Console.WriteLine("Такой команды нет.\nПерсонаж автоматически определён в команду 1 (это можно изменить позже).");
                    break;
                }
            }

            Geo:
            Console.WriteLine("Расположите персонажа на поле: ");
            Console.Write("x: ");
            int x = Convert.ToInt32(Console.ReadLine());
            this._x = x;
            Console.Write("y: ");
            int y = Convert.ToInt32(Console.ReadLine());
            this._y = y;
            foreach (Character character in aliveChar)
            {
                if (character._team != this._team && character._x == this._x && character._y == this._y)
                {
                    Console.WriteLine("Это расположение занято противником. Выберите другое.");
                    goto Geo;
                }
            }
        }

        /// <summary>
        /// Вывод информации о своей команде
        /// </summary>
        /// <param name="aliveChar"></param>
        private void TeamOut(List<Character> aliveChar)
        {
            int team = aliveChar.Count(c => c._team == this._team && c._name != this._name);
            if (team > 0)
            {
                int i = 1;
                Console.WriteLine("Ваша команда:");
                foreach (Character character in aliveChar)
                {
                    if (character._team == this._team && character._name != this._name)
                    {
                        Console.WriteLine(i + ". " + character._name + "(" + character._punch + "), " + character._xpCur + "/" + character._xpMax + ", " + character._x + ";" + character._y);
                        i++;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Вывод статистики
        /// </summary>
        /// <param name="aliveChar"></param>
        /// <param name="deadChar"></param>
        private void Statistics(List<Character> aliveChar)
        {
            int one = aliveChar.Count(c => c._team == true);
            int two = aliveChar.Count(c => c._team == false);
            if (one == 0 && two > 0)
            {
                Console.WriteLine("Игра окончена. Победила команда 2.");
            }
            else if (one > 0 && two == 0)
            {
                Console.WriteLine("Игра окончена. Победила команда 1.");
            }
            else
            {
                Console.WriteLine("Игра окончена. Перемирие.");
            }
        }

        /// <summary>
        /// Вывод информации о персонаже
        /// </summary>
        private void InfoOut()
        {
            Console.WriteLine("Имя: " + this._name);
            Console.WriteLine("Здоровье: " + this._xpCur + "/" + this._xpMax);
            Console.WriteLine("Сила удара: " + this._punch);
            if (this._team == true)
            {
                Console.WriteLine("Команда: 1");
            }
            else
            {
                Console.WriteLine("Команда: 2");
            }
            Console.WriteLine("Побед: " + this._vict);
            if (this._xpCur >= 0)
            {
                Console.WriteLine("Координаты расположения: " + this._x + ";" + this._y);
            }
            else
            {
                Console.WriteLine("Убит.");
            }
        }

        /// <summary>
        /// Передвижение по горизонтали
        /// </summary>
        /// <param name="x"></param>
        private void MoveX(List<Character> aliveChar, List<Character> deadChar)
        {
            if (this._xpCur > 0)
            {
                Console.Write("Переместиться по горизонтали на: ");
                int x = Convert.ToInt32(Console.ReadLine());
                this._x += x;
                Console.WriteLine("Ваше новое расположение: " + this._x + ";" + this._y);
                foreach (Character character in aliveChar)
                {
                    if (character._name != this._name && character._team != this._team && character._x == this._x && character._y == this._y)
                    {
                        Console.WriteLine("Драка с " + character._name + ".");
                        Fight(aliveChar, deadChar, aliveChar.IndexOf(character));
                    }
                }
            }
            else
            {
                Console.WriteLine("Вы не можете двигаться (здоровье пусто).");
            }
        }

        /// <summary>
        /// Передвижение по вертикали
        /// </summary>
        /// <param name="y"></param>
        private void MoveY(List<Character> aliveChar, List<Character> deadChar)
        {
            if (this._xpCur > 0)
            {
                Console.Write("Переместиться по вертикали на: ");
                int y = Convert.ToInt32(Console.ReadLine());
                this._y += y;
                Console.WriteLine("Ваше новое расположение: " + this._x + ";" + this._y);
                int a = 999999999;
                foreach (Character character in aliveChar)
                {
                    if (character._name != this._name && character._team != this._team && character._x == this._x && character._y == this._y)
                    {
                        a = aliveChar.IndexOf(character);
                        goto A;
                    }
                }
                A:
                if (a != 999999999)
                {
                    Console.WriteLine("Драка с " + aliveChar[a]._name + ".");
                    Fight(aliveChar, deadChar, a);
                }
            }
            else
            {
                Console.WriteLine("Вы не можете двигаться (здоровье пусто).");
            }
        }

        /// <summary>
		/// Драка
		/// </summary>
		/// <param name="name"></param>
		/// <param name="xp"></param>
		private void Fight(List<Character> aliveChar, List<Character> deadChar, int index)
        {
            Round:
            Console.WriteLine("Чтобы подраться, нажмите Enter. Если не хотите, введите 'нет' и нажмите Enter.");
            string answ = Console.ReadLine();
            if (answ != "нет")
            {
                if (this._punch <= aliveChar[index]._xpCur)
                {
                    aliveChar[index]._xpCur -= this._punch;
                    Console.WriteLine(aliveChar[index]._name + " получил урон.");
                    goto Round;
                }
                else
                {
                    aliveChar[index]._xpCur -= this._punch;
                    this._vict++;
                    this._victChange++;
                    Console.WriteLine(aliveChar[index]._name + " убит.");
                    deadChar.Add(aliveChar[index]);
                    aliveChar.Remove(aliveChar[index]);
                }
            }
            else
            {
                Console.WriteLine("Бегите.");
                this.MoveX(aliveChar, deadChar);
                this.MoveY(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Лечение
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xp"></param>
        private void Heal(List<Character> aliveChar)
        {
            if (this._xpCur > 0)
            {
                int ill = aliveChar.Count(c => c._team == this._team && c._name != this._name && c._xpCur < c._xpMax);
                if (ill > 0)
                {
                    Console.Write("Выберите, кого собираетесь лечить: ");
                    foreach (Character character in aliveChar)
                    {
                        if (character._team == this._team && character._name != this._name && character._xpCur < character._xpMax)
                        {
                            Console.WriteLine((aliveChar.IndexOf(character) + 1) + ". " + character._name);
                        }
                    }
                    int numb = Convert.ToInt32(Console.ReadLine()) - 1;
                    Heal:
                    Console.Write("Введите количество единиц лечения: ");
                    int xp = Convert.ToInt32(Console.ReadLine());
                    if (xp < 0)
                    {
                        Console.WriteLine("Вы не можете лечить на 0 единиц или менее.");
                        goto Heal;
                    }
                    if (xp > this._xpCur)
                    {
                        Console.WriteLine("У Вас недостаточно единиц здоровья.");
                        goto Heal;
                    }
                    if ((aliveChar[numb]._xpCur + xp) >= aliveChar[numb]._xpMax)
                    {
                        Console.WriteLine("Вы не можете лечить на большее количество единиц здоровья, чем это необходимо.");
                        goto Heal;
                    }
                    aliveChar[numb]._xpCur += xp;
                    this._xpCur -= xp;
                    Console.WriteLine(aliveChar[numb]._name + " получил лечение. Здоровье: " + aliveChar[numb]._xpCur);
                }
                else
                {
                    Console.WriteLine("Лечить некого.");
                }
            }
            else
            {
                Console.WriteLine("Вы не можете лечить (здоровье пусто).");
            }
        }

		/// <summary>
        /// Восстановление здоровья
        /// </summary>
		private void TotalHeal()
		{
            if (this._victChange >= 5)
            {
                this._xpCur = this._xpMax;
                this._victChange -= 5;
                Console.WriteLine("Здоровье восстановлено.");
            }
            else
            {
                Console.WriteLine("Для восстановления здоровья необходимо победить хотя бы 5 врагов.");
            }
        }

        /// <summary>
        /// Смена команды
        /// </summary>
        /// <param name="team"></param>
        private void TeamChange()
        {
            if (this._team == true)
            {
                this._team = false;
            }
            else
            {
                this._team = true;
            }
            Console.WriteLine("Вы сменили команду.");
		}
    }
}