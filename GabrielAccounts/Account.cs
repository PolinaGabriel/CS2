using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GabrielAccounts
{
	internal class Account
	{
		private int _numb; //номер счёта
		private string _name; //ФИО владельца
		private double _money; //сумма на счету

		/// <summary>
		/// Создание счёта
		/// </summary>
		public Account()
		{
			this._numb = 0;
			this._name = "";
			this._money = 0;
		}

		/// <summary>
		/// Выбор приватного метода
		/// </summary>
		/// <param name="accounts">список счетов</param>
		public void Act(List<Account> accounts)
		{
			this.AccountAppear(accounts);
		}

		/// <summary>
		/// Открытие счёта
		/// </summary>
		/// <param name="accounts">список счетов</param>
		private void AccountAppear(List<Account> accounts)
		{
			if (accounts.Count < 1)
			{
				Console.WriteLine("Для начала работы создайте хотя бы один счёт.\n");
			}
			accounts.Add(new Account());
			Account last = accounts.Last();
			last.InfoIn(accounts);
			Console.WriteLine();
			Console.WriteLine("Чтобы начать работу, нажмите Enter. Если хотите создать ещё счёт, введите '+' и нажмите Enter.");
			string answ = Console.ReadLine();
			Console.WriteLine();
			if (answ == "+")
			{
				this.AccountAppear(accounts);
			}
			else
			{
				this.ChooseAccount(accounts);
			}
		}

		/// <summary>
		/// Выбор счёта
		/// </summary>
		/// <param name="accounts">список счетов</param>
		private void ChooseAccount(List<Account> accounts)
		{
			Console.WriteLine("Выберите счёт:\n");
			foreach (Account account in accounts)
			{
				account.InfoOut();
				Console.WriteLine();
			}
			int accChoice = Convert.ToInt32(Console.ReadLine());
			foreach (Account account in accounts)
			{
				if (accChoice == account._numb)
				{
					account.ChooseAction(accounts);
					break;
				}
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Выбор действия
		/// </summary>
		/// <param name="accounts">список счетов</param>
		private void ChooseAction(List<Account> accounts)
		{
			Console.WriteLine("\nВыберите действие:\n1 - информация о счёте\n2 - внести средства\n3 - снять средства\n4 - обнулить счёт\n5 - перевести средства\n6 - выбрать другой счёт\n7 - открыть новый счёт\nEnter - выход\n");
			string actChoice = Console.ReadLine();
			Console.WriteLine();
			switch (actChoice)
			{
				case "1":
					this.InfoOut();
					this.ChooseAction(accounts);
					break;

				case "2":
					this.MoreMoney();
					this.ChooseAction(accounts);
					break;

				case "3":
					this.LessMoney();
					this.ChooseAction(accounts);
					break;

				case "4":
					this.Zero();
					this.ChooseAction(accounts);
					break;

				case "5":
					this.Transaction(accounts);
					this.ChooseAction(accounts);
					break;

				case "6":
					this.ChooseAccount(accounts);
					break;

				case "7":
					this.AccountAppear(accounts);
					break;

				default:
					break;	
			}
		}

		/// <summary>
		/// Заполнение информации о счёте
		/// </summary>
		/// <param name="accounts">список счетов</param>
		private void InfoIn(List<Account> accounts)
		{
			this.InfoInNumb(accounts);
			this.InfoInName();
			this.InfoInMoney();
		}

		/// <summary>
		/// Заполнение номера счёта
		/// </summary>
		/// <param name="accounts">список счетов</param>
		private void InfoInNumb(List<Account> accounts)
		{
			Console.Write("Введите номер счёта (не может быть меньше нуля): ");
			int numb = Convert.ToInt32(Console.ReadLine());
			if (numb < 0)
			{
				this.InfoInNumb(accounts);
			}
			int a = 0;
			foreach (Account account in accounts)
			{
				if (numb == account._numb)
				{
					Console.WriteLine("Счёт с таким номером уже существует.");
					a++;
					this.InfoInNumb(accounts);
				}
			}
			if (a == 0)
			{
				this._numb = numb;
			}
		}

		/// <summary>
		/// Заполнение ФИО владельца счёта
		/// </summary>
		private void InfoInName()
		{
			Console.Write("Введите ФИО владельца (не может остаться пустым): ");
			string name = Console.ReadLine();
			if (name == "")
			{
				this.InfoInName();
			}
			else
			{
				this._name = name;
			}
		}

		/// <summary>
		/// Заполнение суммы на счету
		/// </summary>
		private void InfoInMoney()
		{
			Console.Write("Положите на счёт деньги (сумма должна быть больше нуля): ");
			double money = Convert.ToDouble(Console.ReadLine());
			if (money < 0)
			{
				this.InfoInMoney();
			}
			else
			{
				this._money = Math.Round(money, 2);
			}
		}

		/// <summary>
		/// Вывод информации о счёте
		/// </summary>
		private void InfoOut()
		{
			Console.WriteLine("№" + this._numb);
			Console.WriteLine("Владелец: " + this._name);
			Console.WriteLine("Сумма: " + this._money);
		}

		/// <summary>
		/// Внесение средств на счёт
		/// </summary>
		private void MoreMoney()
		{
			Console.Write("Введите сумму внесения (должна быть больше нуля): ");
			double money = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
			if (money > 0)
			{
				this._money += Math.Round(money, 2);
				Console.WriteLine("Средства внесены.");
			}
			else
			{
				this.MoreMoney();
			}
		}

		/// <summary>
		/// Снятие средств со счёта
		/// </summary>
		private void LessMoney()
		{
			Console.Write("Введите сумму снятия (должна быть больше нуля): ");
			double money = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
			if (money > 0)
			{
				this._money -= Math.Round(money, 2);
				Console.WriteLine("Средства сняты.");
			}
			else
			{
				this.MoreMoney();
			}
		}

		/// <summary>
		/// Обнуление счёта
		/// </summary>
		private void Zero()
		{
			this._money = 0;
			Console.WriteLine("Счёт обнулён.");
		}

		/// <summary>
		/// Транзакция
		/// </summary>
		/// <param name="accounts"></param>
		/// <param name="nom"></param>
		/// <param name="sum"></param>
		private void Transaction(List<Account> accounts)
		{
			Console.Write("Введите сумму перевода (должна быть больше нуля): ");
			double money = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
			if (money > 0)
			{
				if (money > this._money)
				{
					Console.Write("Вы не можете перевести больше средств, чем есть на счету.");
				}
				else
				{
					Console.WriteLine("Выберите счёт:\n");
					foreach (Account account in accounts)
					{
						if (account._numb != this._numb)
						{
							account.InfoOut();
							Console.WriteLine();
						}
					}
					int accChoice = Convert.ToInt32(Console.ReadLine());
					foreach (Account account in accounts)
					{
						if (accChoice == account._numb)
						{
							this._money -= money;
							account._money += money;
							Console.WriteLine("Перевод доставлен.");
						}
					}
					Console.WriteLine();
				}
			}
			else
			{
				this.Transaction(accounts);
			}
		}
	}
}
