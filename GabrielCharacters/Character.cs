using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
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
		private int _vict; //победы в драках

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
		public void Play(List<Character> aliveChar, List<Character> deadChar)
		{
			this.CharactersAppear(aliveChar, deadChar);
		}

		/// <summary>
		/// Создание персонажей
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void CharactersAppear(List<Character> aliveChar, List<Character> deadChar)
		{
			if (aliveChar.Count < 2)
			{
				Console.WriteLine("Для начала игры создайте хотя бы двух персонажей.\n");
				while (aliveChar.Count < 2)
				{
					this.CharacterAppear(aliveChar);
				}
			}
			Console.WriteLine("Чтобы начать игру, нажмите Enter. Если хотите создать ещё персонажа, введите '+' и нажмите Enter.");
			string answ = Console.ReadLine();
			Console.WriteLine();
			if (answ == "+")
			{
				this.CharacterAppear(aliveChar);
				this.CharactersAppear(aliveChar, deadChar);
			}
			else
			{
				Console.WriteLine("Игра началась.\n");
				this.ChooseCharacter(aliveChar, deadChar);
			}
		}

		/// <summary>
		/// Создание персонажа
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		private void CharacterAppear(List<Character> aliveChar)
		{
			aliveChar.Add(new Character());
			Character last = aliveChar.Last();
			last.InfoIn(aliveChar);
			Console.WriteLine();
		}

		/// <summary>
		/// Выбор персонажа
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void ChooseCharacter(List<Character> aliveChar, List<Character> deadChar)
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
					Console.WriteLine();
					character.ChooseAction(aliveChar, deadChar);
					break;
				}
			}
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
						this.Wounded(aliveChar, deadChar);
						this.ChooseAction(aliveChar, deadChar);
						break;
					}

				case "5":
					{
						this.TotalSelfHeal(aliveChar, deadChar);
						break;
					}

				case "6":
					{
						this.TeamChange(aliveChar, deadChar);
						break;
					}

				case "7":
					{
						this.ChooseCharacter(aliveChar, deadChar);
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
			this.InfoInName(aliveChar);
			this.InfoInHp();
			this.InfoInPunch();
			this.InfoInTeam();
			this.InfoInXY(aliveChar);
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
				this.InfoInName(aliveChar);
			}
			int a = 0;
			foreach (Character character in aliveChar)
			{
				if (name == character._name)
				{
					Console.WriteLine("Персонаж с таким именем уже есть. Придумайте другое.");
					a++;
					this.InfoInName(aliveChar);
					break;
				}
			}
			if (a == 0)
			{
				this._name = name;
			}
		}

		/// <summary>
		/// Заполнение здоровья персонажа
		/// </summary>
		private void InfoInHp()
		{
			Console.Write("Введите здоровье персонажа (должно быть больше нуля): ");
			int xpMax = Convert.ToInt32(Console.ReadLine());
			if (xpMax <= 0)
			{
				this.InfoInHp();
			}
			else
			{
				this._hpMax = xpMax;
				this._hpCur = xpMax;
			}
		}

		/// <summary>
		/// Заполнение силы удара персонажа
		/// </summary>
		private void InfoInPunch()
		{
			Console.Write("Введите силу удара персонажа (должна быть не больше его здоровья и больше нуля): ");
			int punch = Convert.ToInt32(Console.ReadLine());
			if (punch <= 0 || punch > this._hpMax)
			{
				this.InfoInPunch();
			}
			else
			{
				this._punch = punch;
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
						this.InfoInTeam();
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
			int a = 0;
			foreach (Character character in aliveChar)
			{
				if (character._team != this._team && character._x == x && character._y == y)
				{
					Console.WriteLine("Это расположение занято противником. Выберите другое.");
					a++;
					this.InfoInXY(aliveChar);
				}
			}
			if (a == 0)
			{
				this._x = x;
				this._y = y;
			}
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
				Console.WriteLine("\nВаша команда:");
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
			this.SelfHealthCheck(aliveChar, deadChar);
			Console.Write("Переместиться по горизонтали на: ");
			int x = Convert.ToInt32(Console.ReadLine());
			this._x += x;
			Console.WriteLine("Ваше новое расположение: " + this._x + ";" + this._y);
			this.TeamCheck(aliveChar, deadChar);
		}

		/// <summary>
		/// Передвижение по вертикали
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void MoveY(List<Character> aliveChar, List<Character> deadChar)
		{
			this.SelfHealthCheck(aliveChar, deadChar);
			Console.Write("Переместиться по вертикали на: ");
			int y = Convert.ToInt32(Console.ReadLine());
			this._y += y;
			//aliveChar.Find(c => c._name == this._name)._y = y;
			Console.WriteLine("Ваше новое расположение: " + this._x + ";" + this._y);
			this.TeamCheck(aliveChar, deadChar);
		}

		/// <summary>
		/// Проверка здоровья персонажа
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void SelfHealthCheck(List<Character> aliveChar, List<Character> deadChar)
		{
			if (this._hpCur < 0)
			{
				Console.WriteLine("Здоровье пусто. Действие невозможно.");
				this.ChooseAction(aliveChar, deadChar);
			}
		}

		/// <summary>
		/// Проверка команды встреченного персонажа
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void TeamCheck(List<Character> aliveChar, List<Character> deadChar)
		{
			List<Character> fightChar = new List<Character>();
			List<string> friendChar = new List<string>();
			foreach (Character character in aliveChar)
			{
				if (character._name != this._name && character._x == this._x && character._y == this._y)
				{
					if (this._team == character._team)
					{
						friendChar.Add(character._name);
					}
					else
					{
						fightChar.Add(character);
					}
				}
			}
			if (friendChar.Count > 0)
			{
				int i = 1;
				Console.WriteLine("Ваши союзники в этой точке:");
				foreach (string name in friendChar)
				{
					Console.WriteLine(i + ". " + name);
					i++;
				}
			}
			friendChar.Clear();
			if (fightChar.Count > 0)
			{
				int i = 1;
				Console.WriteLine("Ваши противники в этой точке:");
				foreach (Character character in fightChar)
				{
					Console.WriteLine(i + ". " + character._name);
					i++;
				}
				this.Fight(aliveChar, deadChar, fightChar);
			}
			else
			{
				this.ChooseAction(aliveChar, deadChar);
			}
		}

		/// <summary>
		/// Драка
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		/// <param name="fightChar">список оппонентов для драки</param>
		private void Fight(List<Character> aliveChar, List<Character> deadChar, List<Character> fightChar)
		{
			Console.WriteLine("Чтобы начать драку, нажмите Enter. Если не хотите, введите 'нет' и нажмите Enter.");
			string answ = Console.ReadLine();
			if (answ != "нет")
			{
				int punch = 0;
				foreach (Character character in fightChar)
				{
					punch += character._punch;
				}
				foreach (Character character in fightChar)
				{
					if (this._vict >= 10)
					{
						this.UltDamage(character);
					}
					if (this._punch / fightChar.Count <= aliveChar.Find(c => c._name == character._name)._hpCur)
					{
						this.Damage(aliveChar.Find(c => c._name == character._name), fightChar.Count);
					}
					else
					{
						this.Kill(aliveChar, deadChar, aliveChar.Find(c => c._name == character._name), fightChar.Count);
					}
				}
				for (int i = 0; i < fightChar.Count; i++)
				{
					if (fightChar[i]._hpCur < 0)
					{
						fightChar.Remove(fightChar[i]);
					}
				}
				this._hpCur -= punch;
				if (this._hpCur > 0)
				{
					Console.WriteLine("Вы получили урон.");
					if (fightChar.Count > 0)
					{
						this.Fight(aliveChar, deadChar, fightChar);
					}
					else
					{
						this.EndCheck(aliveChar, deadChar);
					}
				}
				else if (this._hpCur == 0)
				{
					Console.WriteLine("Вы получили урон. Ваше здоровье пусто.");
					if (fightChar.Count > 0)
					{
						this.ChooseAction(aliveChar, deadChar);
					}
					else
					{
						this.EndCheck(aliveChar, deadChar);
					}
				}
				else
				{
					Console.WriteLine("Вас убили.");
					deadChar.Add(aliveChar.Find(c => c._name == this._name));
					aliveChar.Remove(aliveChar.Find(c => c._name == this._name));
					this.EndCheck(aliveChar, deadChar);
				}
			}
			else
			{
				this.Run(aliveChar, deadChar);
			}
		}

		/// <summary>
		/// Нанесение полного урона
		/// </summary>
		/// <param name="character"></param>
		private void UltDamage(Character character)
		{
			Console.WriteLine("Есть возможность нанести " + character._name + " полный урон. Чтобы сделать это, нажмите Enter. Если не хотите, введите 'нет' и нажмите Enter.");
			string answ = Console.ReadLine();
			if (answ != "нет")
			{
				character._hpCur = -1;
			}
		}

		/// <summary>
		/// Нанесение урона
		/// </summary>
		/// <param name="character">противник</param>
		/// <param name="div">общее число противников</param>
		private void Damage(Character character, int div)
		{
			int punch = this._punch / div;
			character._hpCur -= punch;
			Console.WriteLine(character._name + " получил урон.");
		}

		/// <summary>
		/// Убийство
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		/// <param name="character">противник</param>
		/// <param name="div">общее число противников</param>
		private void Kill(List<Character> aliveChar, List<Character> deadChar, Character character, int div)
		{
			character._hpCur -= this._punch / div;
			this._vict++;
			Console.WriteLine(character._name + " убит.");
			deadChar.Add(character);
			aliveChar.Remove(character);
		}

		/// <summary>
		/// Бегство
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void Run(List<Character> aliveChar, List<Character> deadChar)
		{
			Console.WriteLine("Бегите.");
			Console.Write("Переместиться по горизонтали на: ");
			int x = Convert.ToInt32(Console.ReadLine());
			this._x += x;
			Console.Write("Переместиться по вертикали на: ");
			int y = Convert.ToInt32(Console.ReadLine());
			this._y += y;
			Console.WriteLine("Ваше новое расположение: " + this._x + ";" + this._y);
			TeamCheck(aliveChar, deadChar);
		}

		/// <summary>
		/// Проверка на окончание игры
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void EndCheck(List<Character> aliveChar, List<Character> deadChar)
		{
			if (aliveChar.Count(c => c._team == true) == 0 || aliveChar.Count(c => c._team == false) == 0)
			{
				this.End(aliveChar, deadChar);
			}
			else
			{
				if (this._hpCur < 0)
				{
					this.ChooseCharacter(aliveChar, deadChar);
				}
				else
				{
					this.ChooseAction(aliveChar, deadChar);
				}
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
			}
			else if (aliveChar.Count(c => c._team == true) > 0 && aliveChar.Count(c => c._team == false) == 0)
			{
				Console.WriteLine("\nИгра окончена. Победила команда 1.\n");
			}
			else if (aliveChar.Count(c => c._team == true) > 0 && aliveChar.Count(c => c._team == false) > 0)
			{
				Console.WriteLine("\nИгра окончена. Перемирие.\n");
			}
			else
			{
				Console.WriteLine("\nИгра окончена.\n");
			}
			Console.WriteLine("Команда 1:");
			Console.ForegroundColor = ConsoleColor.Green;
			this.Statistics(aliveChar, true);
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.Red;
			this.Statistics(deadChar, true);
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Команда 2:");
			Console.ForegroundColor = ConsoleColor.Green;
			this.Statistics(aliveChar, false);
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.Red;
			this.Statistics(deadChar, false);
			Console.ResetColor();
			Console.WriteLine();
			Console.Write("Для выхода нажмите Enter.");
			Console.ReadLine();
		}

		/// <summary>
		/// Статистика команды после окончания игры
		/// </summary>
		/// <param name="someChar">список персонажей</param>
		/// <param name="team">команда</param>
		private void Statistics(List<Character> someChar, bool team)
		{
			foreach (Character character in someChar)
			{
				if (character._team == team)
				{
					Console.WriteLine(character._name);
				}
			}
		}

		/// <summary>
		/// Поиск раненых союзников
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void Wounded(List<Character> aliveChar, List<Character> deadChar)
		{
			this.SelfHealthCheck(aliveChar, deadChar);
			int wound = aliveChar.Count(c => c._team == this._team && c._name != this._name && c._hpCur < c._hpMax);
			if (wound > 0)
			{
				Console.WriteLine("Выберите, кого собираетесь лечить: ");
				int i = 1;
				foreach (Character character in aliveChar)
				{
					if (character._team == this._team && character._name != this._name && character._hpCur < character._hpMax)
					{
						Console.WriteLine(i + ". " + character._name);
						i++;
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
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void TotalSelfHeal(List<Character> aliveChar, List<Character> deadChar)
		{
			if (this._vict >= 5)
			{
				this._hpCur = this._hpMax;
				this._vict -= 5;
				Console.WriteLine("Здоровье восстановлено.");
				this.TeamCheck(aliveChar, deadChar);
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
			if (this._team == true)
			{
				this._team = false;
						
			}
			else
			{
				this._team = true;
						
			}
			Console.WriteLine("Вы сменили команду.");
			TeamCheck(aliveChar, deadChar);
		}
	}
}
