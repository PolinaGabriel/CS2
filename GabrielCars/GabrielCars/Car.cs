﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GabrielCars
{
	internal class Car
	{
		private string _numb; //номер
		private double _volMax; //литраж бака
		private double _volCur; //объём бензина в баке
		private double _kmWaste; //расход топлива на 100 км
		private double _x1; //начальная координата x
		private double _y1; //начальная координата y
		private double _x2; //конечная координата x
		private double _y2; //конечная координата y
		private double _km; //расстояние (км)
		private List<double> _x = new List<double>(); //координаты x
		private List<double> _y = new List<double>(); //координаты y
		private List<string> _traj = new List<string>(); //траектория движения
		private double _run; //пробег

		/// <summary>
		/// 
		/// </summary>
		public Car()
		{
			this._numb = "";
			this._volMax = 0;
			this._volCur = 0;
			this._kmWaste = 0;
			this._x1 = 0;
			this._y1 = 0;
			this._x2 = 0;
			this._y2 = 0;
			this._km = 0;
			this._run = 0;
		}

		/// <summary>
		/// Выбор приватного метода
		/// </summary>
		/// <param name="actCoice">case метода в switch</param>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		public void Act(int actCoice, List<Car> cars)
		{
			switch (actCoice)
			{
				case 1:
					{
						this.OneCar(cars);
						break;
					}

				case 2:
					{
						this.NextCars(cars);
						break;
					}

				case 3:
					{
						this.ChooseCar(cars);
						break;
					}

				case 4:
					{
						this.ChooseAction(cars);
						break;
					}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cars"></param>
		private void OneCar(List<Car> cars)
		{
			if (cars.Count < 1)
			{
				Console.WriteLine("Для начала работы создайте хотя бы один автомобиль.\n");
			}
			cars.Add(new Car());
			Car last = cars.Last();
			last.InfoIn(cars);
			Console.WriteLine();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cars"></param>
		private void NextCars(List<Car> cars)
		{
			Console.WriteLine("Чтобы начать работу, нажмите Enter. Если хотите создать ещё автомобиль, введите '+' и нажмите Enter.");
			string answ = Console.ReadLine();
			Console.WriteLine();
			if (answ == "+")
			{
				cars.Add(new Car());
				Car last = cars.Last();
				last.InfoIn(cars);
				Console.WriteLine();
				NextCars(cars);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cars"></param>
		private void ChooseCar(List<Car> cars)
		{
			Console.WriteLine("Выберите автомобиль:\n");
			foreach (Car car in cars)
			{
				Console.WriteLine((cars.IndexOf(car) + 1) + ".");
				car.InfoOut();
				Console.WriteLine();
			}
			int carChoice = Convert.ToInt32(Console.ReadLine()) - 1;
			foreach (Car car in cars)
			{
				if (carChoice == cars.IndexOf(car))
				{
					this._numb = car._numb;
					this._volMax = car._volMax;
					this._volCur = car._volCur;
					this._kmWaste = car._kmWaste;
					this._x1 = car._x1;
					this._y1 = car._y1;
					this._x2 = car._x2;
					this._y2 = car._y2;
					this._km = car._km;
					this._x = car._x;
					this._y = car._y;
					this._traj = car._traj;
					this._run = car._run;
				}
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Выбор действия
		/// </summary>
		/// <param name="aliveChar">список живых персонажей</param>
		/// <param name="deadChar">список мёртвых персонажей</param>
		private void ChooseAction(List<Car> cars)
		{
			Console.WriteLine("\nВыберите действие:\n1 - информация об автомобиле\n2 - заправка\n3 - поездка\n4 - расчёт количества возможных аварий\n5 - сменить автомобиль\n6 - добавить автомобиль\nEnter - выход\n");
			string actChoice = Console.ReadLine();
			Console.WriteLine();
			switch (actChoice)
			{
				case "1":
					{
						this.InfoOut();
						this.ChooseAction(cars);
						break;
					}

				case "2":
					{
						this.Refill();
						this.ChooseAction(cars);
						break;
					}

				case "3":
					{
						this.Move();
						this.ChooseAction(cars);
						break;
					}

				case "4":
					{
						this.Accident(cars);
						this.ChooseAction(cars);
						break;
					}

				case "5":
					{
						this.ChooseCar(cars);
						this.ChooseAction(cars);
						break;
					}

				case "6":
					{
						NextCars(cars);
						this.ChooseAction(cars);
						break;
					}

				default:
					{
						break;
					}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cars"></param>
		private void InfoIn(List<Car> cars)
		{
			InfoInNumb(cars);
			InfoInVol();
			InfoInKmWaste();
			InfoInRun();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cars"></param>
		private void InfoInNumb(List<Car> cars)
		{
			Console.Write("Введите номер автомобиля (не может остаться пустым): ");
			string numb = Console.ReadLine();
			if (numb == "")
			{
				InfoInNumb(cars);
			}
			int a = 0;
			foreach (Car car in cars)
			{
				if (numb == car._numb)
				{
					Console.WriteLine("Автомобиль с таким номером уже есть в базе.");
					a++;
					InfoInNumb(cars);
				}
			}
			if (a == 0)
			{
				this._numb = numb;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void InfoInVol()
		{
			Console.Write("Введите литраж бака автомобиля (должен быть больше нуля): ");
			int vol = Convert.ToInt32(Console.ReadLine());
			if (vol <= 0)
			{
				InfoInVol();
			}
			else
			{
				this._volMax = vol;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void InfoInKmWaste()
		{
			Console.Write("Введите расход топлива на 100 км (должен быть больше нуля): ");
			int kmWaste = Convert.ToInt32(Console.ReadLine());
			if (kmWaste <= 0)
			{
				InfoInKmWaste();
			}
			else
			{
				this._kmWaste = kmWaste;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void InfoInRun()
		{
			Console.Write("Введите пробег автомобиля (должен быть не меньше нуля): ");
			int run = Convert.ToInt32(Console.ReadLine());
			if (run < 0)
			{
				InfoInRun();
			}
			else
			{
				this._run = run;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void InfoOut()
		{
			Console.WriteLine("Номер: " + this._numb);
			Console.WriteLine("Бак: " + this._volCur + "/" + this._volMax + " л.");
			Console.WriteLine("Расход топлива на 100 км: " + this._kmWaste);
			Console.WriteLine("Пробег: " + this._run + " км.");
		}

		/// <summary>
		/// Заправка бака
		/// </summary>
		private void Refill()
		{
			Console.Write("Введите объём бензина: ");
			double vol = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
			if (vol <= Math.Round(this._volMax - this._volCur, 2))
			{
				this._volCur = vol;
				Console.WriteLine("Бензин заправлен.");
			}
			else
			{
				Console.WriteLine("Вы не можете заправить объём бензина, превышающий литраж бензобака.");
				Refill();
			}
		}

		/// <summary>
		/// Поездка
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		private void Move()
		{
			Distance();
			Console.WriteLine("Общее расстояние: " + this._km + " км.\n");
			while (this._km > 0)
			{
				if (Math.Round((double)this._kmWaste / 100 * this._km, 2) <= this._volCur)
				{
					this._run += this._km;
					this._volCur -= Math.Round((double)this._kmWaste / 100 * this._km, 2);
					Console.WriteLine("Вы доехали. Ваш текущий пробег " + this._run + " км.");
					break;
				}
				else
				{
					this._run += Math.Round((double)this._volCur / this._kmWaste * 100, 2);
					Console.WriteLine("Вы проехали " + Math.Round((double)this._volCur / this._kmWaste * 100, 2) + " км. Ваш текущий пробег: " + this._run + " км. Для дальнейшей поездки необходимо дозаправить " + Left() + " л бензина. Поедете дальше?");
					string answ = Console.ReadLine();
					if (answ == "нет")
					{
						this._volCur = 0;
						break;
					}
					else if (answ == "да")
					{
						this._km -= Math.Round((double)this._volCur / this._kmWaste * 100, 2);
						this._volCur = 0;
						Refill();
					}
				}
			}
		}

		/// <summary>
		/// Расчёт длины пути
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		private void Distance()
		{
			Console.WriteLine("Введите координаты начала пути:");
			Console.Write("x1: ");
			double x1 = Convert.ToDouble(Console.ReadLine());
			Console.Write("y1: ");
			double y1 = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine("Введите координаты конца пути:");
			Console.Write("x2: ");
			double x2 = Convert.ToDouble(Console.ReadLine());
			Console.Write("y2: ");
			double y2 = Convert.ToDouble(Console.ReadLine());
			this._x1 = Math.Round(x1, 2);
			this._y1 = Math.Round(y1, 2);
			this._x2 = Math.Round(x2, 2);
			this._y2 = Math.Round(y2, 2);
			this._km = Math.Round(Math.Sqrt(Math.Pow((this._x2 - this._x1), 2) + Math.Pow((this._y2 - this._y1), 2)), 2);
		}

		/// <summary>
		/// Расчёт недостающего объёма топлива
		/// </summary>
		/// <returns></returns>
		private double Left()
		{
			return Math.Round((((double)this._kmWaste / 100) * this._km) - this._volCur, 2);
		}

		/// <summary>
		/// Расчёт возможного количества аварий
		/// </summary>
		/// <param name="avtos"></param>
		/// <param name="nom"></param>
		private void Accident(List<Car> cars)
		{
			if (this._traj.Count == 0)
			{
				Console.Write("Для сверки траекторий Вам необходимо совершить поездку.");
				this.ChooseAction(cars);
			}
			else
			{
				Console.Write("Введите номер автомобиля для сверки: ");
				string numb = Console.ReadLine();
				if (this._numb == numb)
				{
					Console.WriteLine("Вы не можете свериться с собственной траекторией.");
					this.Accident(cars);
				}
				else
				{
					int avtoCheck = 0;
					foreach (Car car in cars)
					{
						if (car._numb == numb)
						{
							if (car._traj.Count == 0)
							{
								Console.Write("Для сверки траекторий второму автомобилю необходимо совершить поездку.");
								this.Accident(cars);
							}
							else
							{
								avtoCheck++;
								this.Trajectory(this._x1, this._y1, this._x2, this._y2);
								car.Trajectory(car._x1, car._y1, car._x2, car._y2);
								int acc = 0;
								for (int i = 0; i < this._traj.Count; i++)
								{
									for (int j = 0; j < car._traj.Count; j++)
									{
										if (this._traj[i] == car._traj[j])
										{
											acc++;
										}
									}
								}
								Console.WriteLine("Количество возможных аварий с участием выбранных машин: " + acc);
								this._x.Clear();
								car._x.Clear();
								this._y.Clear();
								car._y.Clear();
								this._traj.Clear();
								car._traj.Clear();
							}
						}
					}
					if (avtoCheck == 0)
					{
						Console.WriteLine("Номер машины для сверки не существует или введён неверно.");
					}
				}
			}
		}

		/// <summary>
		/// Построение траектории движения
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		private void Trajectory(double x1, double y1, double x2, double y2)
		{
			this._x1 = Math.Round(x1, 2);
			this._y1 = Math.Round(y1, 2);
			this._x2 = Math.Round(x2, 2);
			this._y2 = Math.Round(y2, 2);
			while (this._x1 <= this._x2)
			{
				this._x.Add(this._x1);
				this._x1++;
			}
			this._x1 = Math.Round(x1, 2);
			for (int i = 0; i < _x.Count; i++)
			{
				_y.Add(this._y1 + ((double)(this._y2 - this._y1) * (_x[i] - this._x1) / (this._x2 - this._x1)));
			}
			for (int i = 0; i < _x.Count; i++)
			{
				this._traj.Add(Convert.ToString(_x[i] + " ; " + _y[i]));
			}
		}
	}
}