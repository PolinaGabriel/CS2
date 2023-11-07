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
        private int _vict; //победы в драках (для восстановления здоровья)

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
                    Statistics(aliveChar, deadChar);
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
                    Ill(aliveChar);
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
            InfoInName(aliveChar);
            InfoInXpPunch();
            InfoInTeam();
            InfoInXY(aliveChar);
        }

        /// <summary>
        /// Заполнение имени персонажа
        /// </summary>
        /// <param name="aliveChar"></param>
        private void InfoInName(List<Character> aliveChar)
        {
            Console.Write("Введите имя персонажа (не может остаться пустым): ");
            string name = Console.ReadLine();
            if (name == "")
            {
                InfoInName(aliveChar);
            }
            foreach (Character character in aliveChar)
            {
                if (name == character._name)
                {
                    Console.WriteLine("Персонаж с таким именем уже есть. Придумайте другое.");
                    InfoInName(aliveChar);
                }
            }
            this._name = name;
        }

        /// <summary>
        /// Заполнение здоровья и силы удара персонажа
        /// </summary>
        private void InfoInXpPunch()
        {
            Console.Write("Введите здоровье персонажа (должно быть больше нуля): ");
            int xpMax = Convert.ToInt32(Console.ReadLine());
            if (xpMax <= 0)
            {
                InfoInXpPunch();
            }
            else
            {
                this._xpMax = xpMax;
                this._xpCur = xpMax;
                Random random = new Random();
                this._punch = random.Next(1, this._xpMax);
            } 
        }

        /// <summary>
        /// Заполнение команды персонажа
        /// </summary>
        private void InfoInTeam()
        {
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
                    InfoInTeam();
                    break;
                }
            }
        }

        /// <summary>
        /// Заполнение координат расположения персонажа
        /// </summary>
        /// <param name="aliveChar"></param>
        private void InfoInXY(List<Character> aliveChar)
        {
            Console.WriteLine("Расположите персонажа на поле: ");
            Console.Write("x: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("y: ");
            int y = Convert.ToInt32(Console.ReadLine());
            foreach (Character character in aliveChar)
            {
                if (character._team != this._team && character._x == x && character._y == y)
                {
                    Console.WriteLine("Это расположение занято противником. Выберите другое.");
                    InfoInXY(aliveChar);
                }
            }
            this._x = x;
            this._y = y;
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
        private void Statistics(List<Character> aliveChar, List<Character> deadChar)
        {
            if (aliveChar.Count(c => c._team == true) == 0 && aliveChar.Count(c => c._team == false) > 0)
            {
                Console.WriteLine("Игра окончена. Победила команда 2.\n");
                Console.WriteLine("Команда 1:");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Character character in aliveChar)
                {
                    if (character._team == true)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Character character in deadChar)
                {
                    if (character._team == true)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Команда 2:");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Character character in aliveChar)
                {
                    if (character._team == false)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Character character in deadChar)
                {
                    if (character._team == false)
                    {
                        Console.WriteLine(character._name);
                    }

                }
                Console.ResetColor();
                Console.WriteLine();
            }
            else if (aliveChar.Count(c => c._team == true) > 0 && aliveChar.Count(c => c._team == false) == 0)
            {
                Console.WriteLine("Игра окончена. Победила команда 1.\n");
                Console.WriteLine("Команда 1:");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Character character in aliveChar)
                {
                    if (character._team == true)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Character character in deadChar)
                {
                    if (character._team == true)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Команда 2:");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Character character in aliveChar)
                {
                    if (character._team == false)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Character character in deadChar)
                {
                    if (character._team == false)
                    {
                        Console.WriteLine(character._name);
                    }

                }
                Console.ResetColor();
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Игра окончена. Перемирие.\n");
                Console.WriteLine("Команда 1:");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Character character in aliveChar)
                {
                    if (character._team == true)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Character character in deadChar)
                {
                    if (character._team == true)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Команда 2:");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Character character in aliveChar)
                {
                    if (character._team == false)
                    {
                        Console.WriteLine(character._name);
                    }
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Character character in deadChar)
                {
                    if (character._team == false)
                    {
                        Console.WriteLine(character._name);
                    }

                }
                Console.ResetColor();
                Console.WriteLine();
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
                Console.Write("Переместиться по вертикали на: ");
                int x = Convert.ToInt32(Console.ReadLine());
                this._x += x;
                Console.WriteLine("Ваше новое расположение: " + this._x + ";" + this._y);
                List<int> fightChar = new List<int>();
                foreach (Character character in aliveChar)
                {
                    if (character._name != this._name && character._x == this._x && character._y == this._y)
                    {
                        if (TeamCheck(character) == 0)
                        {
                            Console.WriteLine("Здесь стоит Ваш союзник " + character._name);
                        }
                        else
                        {
                            fightChar.Add(aliveChar.IndexOf(character));
                        }
                    }
                }
                if (fightChar.Count > 0)
                {
                    foreach (int index in fightChar)
                    {
                        Console.WriteLine("Здесь стоит Ваш враг " + aliveChar[index]._name + ".");
                        this.Fight(aliveChar, deadChar, index);
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
                List<int> fightChar = new List<int>();
                foreach (Character character in aliveChar)
                {
                    if (character._name != this._name && character._x == this._x && character._y == this._y)
                    {
                        if (TeamCheck(character) == 0)
                        {
                            Console.WriteLine("Здесь стоит Ваш союзник " + character._name);
                        }
                        else
                        {
                            fightChar.Add(aliveChar.IndexOf(character));
                        }
                    }
                }
                if (fightChar.Count > 0)
                {
                    foreach (int index in fightChar)
                    {
                        Console.WriteLine("Здесь стоит Ваш враг " + aliveChar[index]._name + ".");
                        this.Fight(aliveChar, deadChar, index);
                    }

                }
            }
            else
            {
                Console.WriteLine("Вы не можете двигаться (здоровье пусто).");
            }
        }

        /// <summary>
        /// Проверка команды встреченного персонажа
        /// </summary>
        /// <param name="character"></param>
        private int TeamCheck(Character character)
        {
            if (this._team == character._team)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
		/// Драка
		/// </summary>
		/// <param name="name"></param>
		/// <param name="xp"></param>
		private void Fight(List<Character> aliveChar, List<Character> deadChar, int index)
        {
            Console.WriteLine("Чтобы подраться, нажмите Enter. Если не хотите, введите 'нет' и нажмите Enter.");
            string answ = Console.ReadLine();
            if (answ != "нет")
            {
                if (this._punch <= aliveChar[index]._xpCur)
                {
                    this.Damage(aliveChar[index], aliveChar, deadChar, index);
                }
                else
                {
                    this.Kill(aliveChar, deadChar, index);
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
        /// Нанесение урона
        /// </summary>
        /// <param name="character"></param>
        /// <param name="aliveChar"></param>
        /// <param name="deadChar"></param>
        /// <param name="index"></param>
        private void Damage(Character character, List<Character> aliveChar, List<Character> deadChar, int index)
        {
            character._xpCur -= this._punch;
            Console.WriteLine(character._name + " получил урон.");
            Fight(aliveChar, deadChar, index);
        }

        /// <summary>
        /// Убийство
        /// </summary>
        /// <param name="aliveChar"></param>
        /// <param name="deadChar"></param>
        /// <param name="index"></param>
        private void Kill(List<Character> aliveChar, List<Character> deadChar, int index)
        {
            aliveChar[index]._xpCur -= this._punch;
            this._vict++;
            Console.WriteLine(aliveChar[index]._name + " убит.");
            deadChar.Add(aliveChar[index]);
            aliveChar.Remove(aliveChar[index]);
        }

        /// <summary>
        /// Поиск больных
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xp"></param>
        private void Ill(List<Character> aliveChar)
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
                    int index = Convert.ToInt32(Console.ReadLine()) - 1;
                    this.Heal(aliveChar, index);
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
        /// Лечение
        /// </summary>
        /// <param name="aliveChar"></param>
        /// <param name="index"></param>
        private void Heal(List<Character> aliveChar, int index)
        {
            if (this._xpCur > 0)
            {
                Console.Write("Введите количество единиц лечения: ");
                int xp = Convert.ToInt32(Console.ReadLine());
                if (xp < 0)
                {
                    Console.WriteLine("Вы не можете лечить на 0 единиц или менее.");
                    this.Heal(aliveChar, index);
                }
                if (xp > this._xpCur)
                {
                    Console.WriteLine("У Вас недостаточно единиц здоровья.");
                    this.Heal(aliveChar, index);
                }
                if ((aliveChar[index]._xpCur + xp) >= aliveChar[index]._xpMax)
                {
                    Console.WriteLine("Вы не можете лечить на большее количество единиц здоровья, чем это необходимо.");
                    this.Heal(aliveChar, index);
                }
                aliveChar[index]._xpCur += xp;
                this._xpCur -= xp;
                Console.WriteLine(aliveChar[index]._name + " получил лечение. Здоровье: " + aliveChar[index]._xpCur);
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
            if (this._vict >= 5)
            {
                this._xpCur = this._xpMax;
                this._vict -= 5;
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