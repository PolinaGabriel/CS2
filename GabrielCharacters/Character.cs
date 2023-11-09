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
        private int _hpMax; //максимально возможное количество жизней
        private int _hpCur; //текущее количество жизней
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
            this._hpMax = 0;
            this._hpCur = 0;
            this._punch = 0;
            this._team = true;
            this._x = 0;
            this._y = 0;
            this._vict = 0;
        }

        /// <summary>
        /// Выбор приватного метода
        /// </summary>
        /// <param name="actCoice">case метода в switch</param>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        public void Play(int actCoice, List<Character> aliveChar, List<Character> deadChar)
        {
            switch (actCoice)
            {
				case 1:
			    {
					this.TwoCharacters(aliveChar);
					break;
				}

				case 2:
				{
                    this.NextCharacters(aliveChar);
					break;
				}

				case 3:
				{
                    this.ChooseCharacter(aliveChar);
					break;
				}

				case 4:
				{
					this.ChooseAction(aliveChar, deadChar);
					break;
				}
			}
        }

		/// <summary>
		/// Создание минимального для игры количества персонажей
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		private void TwoCharacters(List<Character> aliveChar)
		{
			Console.WriteLine("Для начала игры создайте хотя бы двух персонажей.\n");
			while (aliveChar.Count < 2)
			{
				aliveChar.Add(new Character());
				Character last = aliveChar.Last();
				last.InfoIn(aliveChar);
				Console.WriteLine();
			}
		}

		/// <summary>
		/// Создание дополнительных персонажей, если это требуется
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		private void NextCharacters(List<Character> aliveChar)
		{
			Console.WriteLine("Чтобы начать игру, нажмите Enter. Если хотите создать ещё персонажа, введите 'ещё' и нажмите Enter.");
			string answ = Console.ReadLine();
			Console.WriteLine();
            if (answ == "ещё")
            {
                aliveChar.Add(new Character());
                Character last = aliveChar.Last();
                last.InfoIn(aliveChar);
                Console.WriteLine();
                NextCharacters(aliveChar);
            }
            else
            {
				Console.WriteLine("Игра началась.\n");
			}
		}

		/// <summary>
		/// Выбор персонажа
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		private void ChooseCharacter(List<Character> aliveChar)
		{
			Console.WriteLine("Выберите персонажа:\n");
			foreach (Character character in aliveChar)
			{
				Console.WriteLine((aliveChar.IndexOf(character) + 1) + ".");
				character.InfoOut();
				Console.WriteLine();
			}
			int charChoice = Convert.ToInt32(Console.ReadLine()) - 1;
			foreach (Character character in aliveChar)
			{
				if (charChoice == aliveChar.IndexOf(character))
				{
					this._name = character._name;
					this._hpMax = character._hpMax;
					this._hpCur = character._hpCur;
					this._punch = character._punch;
					this._team = character._team;
					this._x = character._x;
					this._y = character._y;
					this._vict = character._vict;
				}
			}
			Console.WriteLine();
		}

        /// <summary>
        /// Выбор действия
        /// </summary>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
		private void ChooseAction(List<Character> aliveChar, List<Character> deadChar)
		{
            this.TeamOut(aliveChar);
			Console.WriteLine("\nВыберите действие:\n1 - информация о персонаже\n2 - переместиться по горизонтали\n3 - переместиться по вертикали\n4 - полечить союзника\n5 - восстановить здоровье\n6 - сменить команду\n7 - сменить персонажа\nEnter - закончить игру\n");
			string actChoice = Console.ReadLine();
			Console.WriteLine();
			switch (actChoice)
			{
				case "1":
				{
                    this.InfoOut();
                    this.ChooseAction(aliveChar, deadChar);
			        break;
				}

				case "2":
				{
                    this.MoveX(aliveChar, deadChar);
					break;
				}

				case "3":
				{
                    this.MoveY(aliveChar, deadChar);
					break;
				}

				case "4":
				{
                    this.Wounded(aliveChar);
                    this.ChooseAction(aliveChar, deadChar);
					break;
				}

				case "5":
				{
                    this.TotalSelfHeal();
                    this.ChooseAction(aliveChar, deadChar);
					break;
				}

				case "6":
				{
                    this.TeamChange(aliveChar, deadChar);
					break;
				}

				case "7":
				{
                    this.ChooseCharacter(aliveChar);
                    this.ChooseAction(aliveChar, deadChar);
                    break;
				}

				default:
				{
                    this.End(aliveChar, deadChar);
				    break;
				}
			}
		}

        /// <summary>
        /// Заполнение информации о персонаже
        /// </summary>
        /// <param name="aliveChar">список живых персонажей</param>
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
        /// <param name="aliveChar">список живых персонажей</param>
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
                this._hpMax = xpMax;
                this._hpCur = xpMax;
                Random random = new Random();
                this._punch = random.Next(this._hpMax / 2, this._hpMax);
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
        /// <param name="aliveChar">список живых персонажей</param>
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
        /// <param name="aliveChar">список живых персонажей</param>
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
                        Console.WriteLine(i + ". " + character._name + "(" + character._punch + "), " + character._hpCur + "/" + character._hpMax + ", " + character._x + ";" + character._y);
                        i++;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Вывод информации о персонаже
        /// </summary>
        private void InfoOut()
        {
            Console.WriteLine("Имя: " + this._name);
            Console.WriteLine("Здоровье: " + this._hpCur + "/" + this._hpMax);
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
            if (this._hpCur >= 0)
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
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        private void MoveX(List<Character> aliveChar, List<Character> deadChar)
        {
            if (this._hpCur > 0)
            {
                Console.Write("Переместиться по горизонтали на: ");
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
                else
                {
                    this.ChooseAction(aliveChar, deadChar);
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
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        private void MoveY(List<Character> aliveChar, List<Character> deadChar)
        {
            if (this._hpCur > 0)
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
                else
                {
                    this.ChooseAction(aliveChar, deadChar);
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
        /// <param name="character">встреченный персонаж</param>
        /// <returns>0 если союзник, 1 если противник</returns>
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
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        /// <param name="index">номер противника в списке</param>
		private void Fight(List<Character> aliveChar, List<Character> deadChar, int index)
        {
            Console.WriteLine("Чтобы подраться, нажмите Enter. Если не хотите, введите 'нет' и нажмите Enter.");
            string answ = Console.ReadLine();
            if (answ != "нет")
            {
                if (this._punch <= aliveChar[index]._hpCur)
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
        /// <param name="character">противник</param>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        /// <param name="index">номер противника в списке</param>
        private void Damage(Character character, List<Character> aliveChar, List<Character> deadChar, int index)
        {
            character._hpCur -= this._punch;
            Console.WriteLine(character._name + " получил урон.");
            this.Fight(aliveChar, deadChar, index);
        }

        /// <summary>
        /// Убийство
        /// </summary>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        /// <param name="index">номер противника в списке</param>
        private void Kill(List<Character> aliveChar, List<Character> deadChar, int index)
        {
            aliveChar[index]._hpCur -= this._punch;
            this._vict++;
            Console.WriteLine(aliveChar[index]._name + " убит.");
            deadChar.Add(aliveChar[index]);
            aliveChar.Remove(aliveChar[index]);
            if (aliveChar.Count(c => c._team == true) == 0 || aliveChar.Count(c => c._team == false) == 0)
            {
                this.End(aliveChar, deadChar);
            }
            else
            {
                this.ChooseAction(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        private void End(List<Character> aliveChar, List<Character> deadChar)
        {
			if (aliveChar.Count(c => c._team == true) == 0 && aliveChar.Count(c => c._team == false) > 0)
			{
				Console.WriteLine("\nИгра окончена. Победила команда 2.\n");
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
				Console.WriteLine("\nИгра окончена. Победила команда 1.\n");
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
				Console.WriteLine("\nИгра окончена. Перемирие.\n");
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
			Console.Write("Для выхода нажмите Enter.");
			Console.ReadLine();
		}

        /// <summary>
        /// Поиск раненых союзников
        /// </summary>
        /// <param name="aliveChar">список живых персонажей</param>
        private void Wounded(List<Character> aliveChar)
        {
            if (this._hpCur > 0)
            {
                int wound = aliveChar.Count(c => c._team == this._team && c._name != this._name && c._hpCur < c._hpMax);
                if (wound > 0)
                {
                    Console.Write("Выберите, кого собираетесь лечить: ");
                    foreach (Character character in aliveChar)
                    {
                        if (character._team == this._team && character._name != this._name && character._hpCur < character._hpMax)
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
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="index">номер раненого союзника в списке</param>
        private void Heal(List<Character> aliveChar, int index)
        {
            Console.Write("Введите количество единиц лечения: ");
            int hp = Convert.ToInt32(Console.ReadLine());
            if (hp <= 0)
            {
                Console.WriteLine("Вы не можете лечить на 0 единиц или менее.");
                this.Heal(aliveChar, index);
            }
            if (hp > this._hpCur)
            {
                Console.WriteLine("У Вас недостаточно единиц здоровья.");
                this.Heal(aliveChar, index);
            }
            if ((aliveChar[index]._hpCur + hp) >= aliveChar[index]._hpMax)
            {
                Console.WriteLine("Вы не можете лечить на большее количество единиц здоровья, чем это необходимо.");
                this.Heal(aliveChar, index);
            }
            aliveChar[index]._hpCur += hp;
            this._hpCur -= hp;
            Console.WriteLine(aliveChar[index]._name + " получил лечение. Здоровье: " + aliveChar[index]._hpCur);
        }

        /// <summary>
        /// Восстановление здоровья
        /// </summary>
        private void TotalSelfHeal()
		{
            if (this._vict >= 5)
            {
                this._hpCur = this._hpMax;
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
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        private void TeamChange(List<Character> aliveChar, List<Character> deadChar)
        {
            foreach (Character character in aliveChar)
            {
                if (character._name == this._name)
                {
                    if (this._team == true && character._team == true)
                    {
                        this._team = false;
                        character._team = false;
                    }
                    else
                    {
                        this._team = true;
                        character._team = true;
                    }
                }
            }
            Console.WriteLine("Вы сменили команду.");
            if (aliveChar.Count(c => c._team == true) == 0 || aliveChar.Count(c => c._team == false) == 0)
            {
                this.End(aliveChar, deadChar);
            }
            else
            {
                this.ChooseAction(aliveChar, deadChar);
            }
        }
    }
}