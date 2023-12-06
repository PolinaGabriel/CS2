using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielTwentyFour
{
	internal class TwentyFour
	{

		public void StartText()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Игра в 24");
			Console.ResetColor();
			Console.WriteLine("Имеется карточка с 4 целыми числами в диапазоне от 1 до 30.\nЗадача игрока - при помощи действий сложения, вычитания, умножения и деления и любого количества скобок\nсоставить с этими числами выражение, равное 24 (числа и знаки не могут повторяться).\nДанное приложение проверяет, пригоден ли набор чисел с карточки для игры.\n");
			Game();
		}

		private void Game()
		{
			Console.WriteLine("Введите числа с карточки:");
			int a = 0;
			int b = 0;
			int c = 0;
			int d = 0;
			NumberIn('a', a);
			NumberIn('b', b);
			NumberIn('c', c);
			NumberIn('d', d);
			Cycle(Numbers(a, b, c, d), Signs());
			Console.WriteLine("Хотите проверить ещё карточку?\n1 - да\nEnter - нет");
			string answ = Console.ReadLine();
			Console.WriteLine();
			switch (answ)
			{
				case "1":
					Game();
					break;

				default:
					break;
			}
		}

		private void NumberIn(char x, int n)
		{
			Console.Write(x + " = ");
			try
			{
				n = Convert.ToInt32(Console.ReadLine());
				if (n < 1 || n > 30)
				{
					Console.WriteLine("Число должно принадлежать отрезку [1;30]");
					NumberIn(x, n);
				}
			}
			catch (FormatException e)
			{
				Console.WriteLine("Неверный тип данных");
				NumberIn(x, n);
			}
		}
		
		private List<string> Numbers(int a, int b, int c, int d)
		{
			List<string> numbers = new List<string>();
			int[] numb = { a, b, c, d };
			int i = 0;
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
								numbers.Add(Convert.ToString(numb[i1]) + "a" + Convert.ToString(numb[i2]) + "b" + Convert.ToString(numb[i3]) + "c" + Convert.ToString(numb[i4]));
								i++;
							}
						}
					}
				}
			}
			for (int j = 0; j < numbers.Count() - 1; j++)
			{
				for (int k = 0; k < numbers.Count() - 1; k++)
				{
					if (j != k && numbers[j] == numbers[k])
					{
						numbers.Remove(numbers[j]);
					}
				}
			}
			return numbers;
		}

		private string[] Signs()
		{
			string[] signs = new string[24];
			string[] sig = { "+", "-", "*", "/" };
			int i = 0;
			for (int i1 = 0; i1 < 4; i1++)
			{
				for (int i2 = 0; i2 < 4; i2++)
				{
					for (int i3 = 0; i3 < 4; i3++)
					{
						if (i1 != i2 && i1 != i3 && i2 != i3)
						{
							signs[i] = sig[i1] + sig[i2] + sig[i3];
							i++;
						}	
					}
				}
			}
			return signs;
		}

		private void Cycle(List<string> numbers, string[] signs)
		{
			int i = 0;
			foreach (string number in numbers)
			{
				foreach (string sign in signs)
				{
					if (Result(number, sign) == 24)
					{
						i++;
						break;
					}
				}
				if (i > 0)
				{
					break;
				}
			}
			if (i > 0)
			{
				Console.WriteLine("Карточка составлена корректно.\n");
			}
			else
			{
				Console.WriteLine("Карточка составлена некорректно.\n");
			}
		}

		private double Result(string number, string sign)
		{
			double[] numbersOneString = { Convert.ToDouble(number.Substring(0, number.IndexOf("a"))), Convert.ToDouble(number.Substring(number.IndexOf("a") + 1, number.IndexOf("b") - number.IndexOf("a") - 1)), Convert.ToDouble(number.Substring(number.IndexOf("b") + 1, number.IndexOf("c") - number.IndexOf("b") - 1)), Convert.ToDouble(number.Substring(number.IndexOf("c") + 1)) };
			string[] signsOneString = { sign.Substring(0, 1), sign.Substring(1, 1), sign.Substring(2) };
			return Steps(numbersOneString, signsOneString);
		}

		private double Steps(double[] numbersOneString, string[] signsOneString)
		{
			int k = 1;
			double answer = 0;
			for (int j = 0; j < signsOneString.Count(); j++)
			{
				if (k == 1 && j == 0)
				{
					answer = Step(numbersOneString[0], numbersOneString[1], signsOneString[j]);
				}
				else
				{
					answer = Step(answer, numbersOneString[k], signsOneString[j]);
				}
				k++;
			}
			return answer;
		}

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