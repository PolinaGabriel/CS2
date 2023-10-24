using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GabrielBankAccount
{
    public class Program
    {
        static void Main()
        {
            Account[] accounts = new Account[2];
            accounts[0] = new Account();
            accounts[1] = new Account();

            string next = "";

			while (next != "выход")
            {
                Console.WriteLine("Выберите счёт: \n1\n2\n");
                int accChoice = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.WriteLine("\n");

                Console.WriteLine("Выберите действие со счётом:\n1 - открытие счёта\n2 - вывод информации о счёте\n3 - внесение средств на счёт\n4 - снятие средств со счёта\n5 - обнуление счёта\n6 - транзакция\n7 - вернуться назад\n");
				int operChoice = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("\n");

                switch (operChoice)
                {
                        case 1:
                        {
                            Console.Write("Введите номер счёта: ");
                            int nom = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Введите ФИО владельца: ");
                            string name = Console.ReadLine();
                            Console.Write("Введите сумму: ");
                            double sum = Convert.ToDouble(Console.ReadLine());
                            accounts[accChoice].Methods(operChoice, nom, name, sum, accounts);
							Console.WriteLine("Счёт создан.");
                            break;
                        }

                        case 2:
                        { 
							accounts[accChoice].Methods(operChoice, 0, "", 0, accounts);
                            break;
                        }

                        case 3:
                        {
                            Console.Write("Введите сумму внесения: ");
                            double sum = Convert.ToDouble(Console.ReadLine());
							accounts[accChoice].Methods(operChoice, 0, "", sum, accounts);
							Console.WriteLine("Средства внесены.");
                            break;
                        }

                        case 4:
                        {
                            Console.Write("Введите сумму снятия: ");
                            double sum = Convert.ToDouble(Console.ReadLine());

                            accounts[accChoice].Methods(operChoice, 0, "", sum, accounts);

                            if (accounts[accChoice].sumCheck == 1)
                            {
                                Console.WriteLine("Средства сняты.");
                            }
                            else
                            {
                                Console.WriteLine("Вы не можете снять больше средств, чем есть на счету.");
                            }

                            accounts[accChoice].sumCheck = 0;

                            break;
                        }

                        case 5:
                        {
							accounts[accChoice].Methods(operChoice, 0, "", 0, accounts);
							Console.WriteLine("Счёт обнулён.");
                            break;
                        }

                        case 6:
                        {
                            Console.Write("Введите номер счёта получателя: ");
                            int nom = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Введите сумму перевода: ");
                            double sum = Convert.ToDouble(Console.ReadLine());

                            accounts[accChoice].Methods(operChoice, nom, "", sum, accounts);

                            if (accounts[accChoice].accCheck == 1)
                            {
                                if (accounts[accChoice].sumCheck == 1)
                                {
                                    Console.WriteLine("Перевод доставлен.");
                                }
                                else
                                {
                                    Console.WriteLine("Вы не можете перевести больше средств, чем есть на счету.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Номер счёта получателя введён неверно.");
                            }

                            accounts[accChoice].accCheck = 0;
                            accounts[accChoice].sumCheck = 0;

                            break;
						}

                        case 7:
                        {
                            break;
                        }
                }

				Console.WriteLine("\n\nДля продолжения работы нажмите Enter.\nДля окончания введите 'выход' и нажмите Enter.\n");
				next = Console.ReadLine();
                Console.WriteLine("\n");
            }
        }
    }
}