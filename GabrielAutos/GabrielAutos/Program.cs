using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Autos
{
	internal class Program
	{
		static void Main()
		{
			Avto[] avtos = new Avto[2];
			avtos[0] = new Avto();
			avtos[1] = new Avto();
			string next1 = "";
			while (next1 != "выход")
			{
				Console.WriteLine("Выберите машину:\n1\n2\n");
				int avtoChoice = Convert.ToInt32(Console.ReadLine()) - 1;
				Console.WriteLine("\n");
		                string next2 = "";
				while (next2 != "сменить")
				{
			                Console.WriteLine("Выберите действие с машиной:\n1 - ввод информации об авто\n2 - вывод информации об авто\n3 - заправка бензобака\n4 - поездка\n5 - расчёт количества возможных аварий\n6 - вернуться назад\n");
			                int operChoice = Convert.ToInt32(Console.ReadLine());
			                Console.WriteLine("\n");
			                switch (operChoice)
			                {
				                case 1:
				                {
					                Console.Write("Введите номер авто: ");
					                string nom = Console.ReadLine();
					                Console.Write("Введите литраж бензобака: ");
					                double bak = Convert.ToDouble(Console.ReadLine());
					                Console.Write("Введите расход топлива на 100 км: ");
					                double ras = Convert.ToDouble(Console.ReadLine());
					                Console.Write("Введите пробег: ");
					                double prob = Convert.ToDouble(Console.ReadLine());
					                Console.WriteLine();
					                avtos[avtoChoice].Methods(operChoice, nom, bak, ras, prob, 0, 0, 0, 0, avtos);
					                break;
				                }
			
				                case 2:
				                {
					                avtos[avtoChoice].Methods(operChoice, "", 0, 0, 0, 0, 0, 0, 0, avtos);
					                break;
				                }
				
				                case 3:
				                {
					                avtos[avtoChoice].Methods(operChoice, "", 0, 0, 0, 0, 0, 0, 0, avtos);
					                break;
				                }
				
				                case 4:
				                {
					                Console.WriteLine("Введите координаты начала пути:");
					                Console.Write("Введите x1: ");
					                double x1 = Convert.ToDouble(Console.ReadLine());
					                Console.Write("Введите y1: ");
					                double y1 = Convert.ToDouble(Console.ReadLine());
					                Console.WriteLine("Введите координаты конца пути:");
					                Console.Write("Введите x2: ");
					                double x2 = Convert.ToDouble(Console.ReadLine());
					                Console.Write("Введите y2: ");
					                double y2 = Convert.ToDouble(Console.ReadLine());
					                Console.WriteLine();
					                avtos[avtoChoice].Methods(operChoice, "", 0, 0, 0, x1, y1, x2, y2, avtos);
					                break;
				                }
				
				                case 5:
				                {
					                Console.Write("Введите номер авто, с траекторией которого хотите свериться: ");
					                string nom = Console.ReadLine();
					                avtos[avtoChoice].Methods(operChoice, nom, 0, 0, 0, 0, 0, 0, 0, avtos);
					                break;
				                }
				
				                case 6:
				                {
				                	break;
				                }
			                }
			                Console.WriteLine("\n\nДля продолжения работы с текущей машиной нажмите Enter.\nДля смены машины введите 'сменить' и нажмите Enter.\n");
			        	next2 = Console.ReadLine();
			                Console.WriteLine("\n");
		                }
				Console.WriteLine("\n\nДля продолжения работы нажмите Enter.\nДля окончания введите 'выход' и нажмите Enter.\n");
				next1 = Console.ReadLine();
		                Console.WriteLine("\n");
	            	}
		}
	}
}
