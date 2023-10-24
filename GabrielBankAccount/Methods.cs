using System;
using System.Collections.Generic;
using System.Linq;
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
        public int accCheck; //счётчик проверки корректности введённого получателя
        public int sumCheck; //счётчик проверки корректности введённой суммы

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
            this.nom = nom;
            this.name = name;
            this.sum = Math.Round(sum,2);
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
            this.sum += Math.Round(sum, 2);
        }

        /// <summary>
        /// Снятие средств со счёта
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        private void Umen(double sum)
        {
            if (sum <= this.sum)
            {
                this.sum -= Math.Round(sum, 2);
                this.sumCheck = 1;
            }
        }

        /// <summary>
        /// Обнуление счёта
        /// </summary>
        private void Obnul()
        {
            this.sum = 0;
        }

		/// <summary>
        /// Транзакция
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="nom"></param>
        /// <param name="sum"></param>
		private void Trans(Account[] accounts, int nom, double sum)
		{
			foreach (Account account in accounts)
			{
                if (account.nom == nom)
                {
                    this.accCheck = 1;

                    if (sum <= this.sum)
                    {
                        this.sumCheck = 1;
                        this.sum -= Math.Round(sum, 2);
                        account.sum += Math.Round(sum, 2);
                    }
                }
			}
		}
    }
}
