using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GabrielBankAccount
{
	public class Account
	{
		private int nom; //номер счёта
		private string name; //ФИО владельца
		private double sum; //сумма на счету
		private int accCheck; //счётчик проверки корректности введённого получателя
			
		/// <summary>
		/// Выбор метода
		/// </summary>
		/// <param name="a"></param>
		public void Methods(int operChoice, int nom, string name, double sum, Account[] accounts)
		{
			switch (operChoice)
			{
				case 1:
				{
					Otk(nom, name, sum);
					break;
				}
				
				case 2:
				{
					Out();
					break;
				}
				
				case 3:
				{
					Dob(sum);
					break;
				}
				
				case 4:
				{
					Umen(sum);
					break;
				}
				
				case 5:
				{
					Obnul();
					break;
				}
				
				case 6:
				{
					Trans(accounts, nom, sum);
					break;
				}
			}
		}
		
		/// <summary>
		/// Открытие счёта
		/// </summary>
		/// <param name="nom"></param>
		/// <param name="name"></param>
		/// <param name="sum"></param>
		private void Otk(int nom, string name, double sum)
		{
			if (sum > 0)
			{
				this.nom = nom;
				this.name = name;
				this.sum = Math.Round(sum, 2);
				Console.WriteLine("Счёт создан.");
			}
			else
			{
				Console.WriteLine("Сумма не может быть меньше нуля. Повторите ввод.");
			}
		}
		
		/// <summary>
		/// Вывод информации о счёте
		/// </summary>
		private void Out()
		{
			Console.WriteLine("Номер счёта: " + this.nom);
			Console.WriteLine("Владелец: " + this.name);
			Console.WriteLine("Сумма: " + this.sum);
		}
		
		/// <summary>
		/// Внесение средств на счёт
		/// </summary>
		/// <param name="sum"></param>
		private void Dob(double sum)
		{
			if (sum > 0)
			{
				this.sum += Math.Round(sum, 2);
				Console.WriteLine("Средства внесены.");
			}
			else
			{
				Console.WriteLine("Сумма не может быть меньше нуля. Повторите ввод.");
			}
		}
			
		/// <summary>
		/// Снятие средств со счёта
		/// </summary>
		/// <param name="sum"></param>
		/// <returns></returns>
		private void Umen(double sum)
		{
			if (sum > 0)
			{
				if (sum <= this.sum)
				{
					this.sum -= Math.Round(sum, 2);
					Console.WriteLine("Средства сняты.");
				}
				else
				{
					Console.WriteLine("Вы не можете снять больше средств, чем есть на счету.");
				}
			}
			else
			{
				Console.WriteLine("Сумма не может быть меньше нуля. Повторите ввод.");
			}
		}
		
		/// <summary>
		/// Обнуление счёта
		/// </summary>
		private void Obnul()
		{
			this.sum = 0;
			Console.WriteLine("Счёт обнулён.");
		}
		
		/// <summary>
		/// Транзакция
		/// </summary>
		/// <param name="accounts"></param>
		/// <param name="nom"></param>
		/// <param name="sum"></param>
		private void Trans(Account[] accounts, int nom, double sum)
		{
			if (sum > 0)
			{
				if (this.nom == nom)
				{
					Console.WriteLine("Вы не можете перевести деньги на счёт, с которого осуществляется перевод.");
				}
				else
				{
					foreach (Account account in accounts)
					{
						if (account.nom == nom)
						{
							this.accCheck = 1;
							if (sum <= this.sum)
							{
								this.sum -= Math.Round(sum, 2);
								account.sum += Math.Round(sum, 2);
								Console.WriteLine("Перевод доставлен.");
							}
							else
							{
								Console.WriteLine("Вы не можете перевести больше средств, чем есть на счету.");
							}
						}
					}
					if (this.accCheck == 0)
					{
						Console.WriteLine("Номер счёта получателя не существует или введён неверно.");
					}
					this.accCheck = 0;
				}
			}
			else
			{
				Console.WriteLine("Сумма не может быть меньше нуля. Повторите ввод.");
			}
		}
	}
}
