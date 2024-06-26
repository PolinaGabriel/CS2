﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielTwentyFour
{
	internal class TwentyFour
	{
		private int _a;
		private int _b;
		private int _c;
		private int _d;

		/// <summary>
		/// Вывод правил игры и назначения приложения
		/// </summary>
		public void StartText()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Игра в 24");
			Console.ResetColor();
			Console.WriteLine("Имеется карточка с 4 целыми числами в диапазоне от 1 до 30.\nЗадача игрока - при помощи действий сложения, вычитания, умножения и деления и любого количества скобок\nсоставить с этими числами выражение, равное 24 (числа не могут повторяться).\nДанное приложение проверяет, пригоден ли набор чисел с карточки для игры.\n");
			Menu();
		}

		/// <summary>
		/// Меню
		/// </summary>
		private void Menu()
		{
			Console.WriteLine("Введите числа с карточки:");
			NumberIn('a');
			NumberIn('b');
			NumberIn('c');
			NumberIn('d');
			Search(this._a, this._b, this._c, this._d, Signs());
			Console.WriteLine("\nХотите проверить ещё карточку?\n1 - да\nEnter - нет");
			string answ = Console.ReadLine();
			Console.WriteLine();
			switch (answ)
			{
				case "1":
					Menu();
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Ввод числа с карточки
		/// </summary>
		/// <param name="x">буквенное обозначение числа</param>
		private void NumberIn(char x)
		{
			Console.Write(x + " = ");
			try
			{
				int n = Convert.ToInt32(Console.ReadLine());
				if (n < 1 || n > 30)
				{
					Console.WriteLine("Число должно принадлежать отрезку [1;30]");
					NumberIn(x);
				}
				else
				{
					switch (x)
					{
						case 'a':
							this._a = n;
							break;

						case 'b':
							this._b = n;
							break;

						case 'c':
							this._c = n;
							break;

						case 'd':
							this._d = n;
							break;

					}
				}
			}
			catch (FormatException e)
			{
				Console.WriteLine("Неверный тип данных");
				NumberIn(x);
			}
		}

		/// <summary>
		/// Составление всех возможных комбинаций знаков действий
		/// </summary>
		/// <returns>массив комбинаций знаков действий</returns>
		private string[] Signs()
		{
			string[] signs = new string[64];
			string[] sig = { "+", "-", "*", "/" };
			int i = 0;
			for (int i1 = 0; i1 < 4; i1++)
			{
				for (int i2 = 0; i2 < 4; i2++)
				{
					for (int i3 = 0; i3 < 4; i3++)
					{
						signs[i] = sig[i1] + sig[i2] + sig[i3];
						i++;
					}
				}
			}
			return signs;
		}

		/// <summary>
		/// Поиск решения карточки, равного 24
		/// </summary>
		/// <param name="a">первое число с карточки</param>
		/// <param name="b">второе число с карточки</param>
		/// <param name="c">третье число с карточки</param>
		/// <param name="d">четвёртое число с карточки</param>
		/// <param name="signs">массив комбинаций знаков действий</param>
		private void Search(int a, int b, int c, int d, string[] signs)
		{
			int i = 0;
			int[] numb = { a, b, c, d };
			for (int i1 = 0; i1 < 4; i1++)
			{
				for (int i2 = 0; i2 < 4; i2++)
				{
					for (int i3 = 0; i3 < 4; i3++)
					{
						for (int i4 = 0; i4 < 4; i4++)
						{
							if (i1 != i2 && i1 != i3 && i1 != i4 && i2 != i3 && i2 != i4 && i3 != i4)
							{
								foreach (string sign in signs)
								{
									double x1 = Step(Convert.ToDouble(numb[i1]), Convert.ToDouble(numb[i2]), sign.Substring(0, 1));
									double x2 = Step(x1, Convert.ToDouble(numb[i3]), sign.Substring(1, 1));
									double x3 = Step(x2, Convert.ToDouble(numb[i4]), sign.Substring(2));
									if (x3 == 24)
									{
										i++;
										Console.WriteLine("\nКарточка составлена корректно.\nВозможные действия:");
										Console.WriteLine("1. " + numb[i1] + sign.Substring(0, 1) + numb[i2] + "=" + x1);
										Console.WriteLine("2. " + x1 + sign.Substring(1, 1) + numb[i3] + "=" + x2);
										Console.WriteLine("3. " + x2 + sign.Substring(2) + numb[i4] + "=" + x3);
										i1 = 4;
										i2 = 4;
										i3 = 4;
										i4 = 4;
										break;
									}
								}
							}
						}
					}
				}
			}
			if (i == 0)
			{
				Console.WriteLine("\nКарточка составлена некорректно.\n");
			}
		}

		/// <summary>
		/// Одно действие подсчёта
		/// </summary>
		/// <param name="numberOne">первое число в действии</param>
		/// <param name="numberTwo">второе число в действии</param>
		/// <param name="sign">знак действия</param>
		/// <returns></returns>
		private double Step(double numberOne, double numberTwo, string sign)
		{
			switch (sign)
			{
				case "+":
					return numberOne + numberTwo;

				case "-":
					return Math.Abs(numberOne - numberTwo);

				case "*":
					return numberOne * numberTwo;

				case "/":
					return (double)numberOne / numberTwo;

				default:
					return -1;
			}
		}
	}
}
