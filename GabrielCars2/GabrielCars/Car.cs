using System;
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
		protected string _numb; //номер
        protected double _volMax; //объём бака (л)
        protected double _volCur; //объём топлива в баке (л)
        protected double _speedCur; //скорость движения (км/ч)
        protected double _speedMax; //максимальная скорость движения (км/ч)
        protected double _kmWaste; //расход топлива на 100 км (Л)
        protected double _x1; //начальная координата x
        protected double _y1; //начальная координата y
        protected double _x2; //конечная координата x
        protected double _y2; //конечная координата y
        protected double _km; //расстояние (км)
        protected List<string> _traj = new List<string>(); //траектория движения
        protected double _run; //пробег (км)

		/// <summary>
		/// Создание автомобиля
		/// </summary>
		public Car()
		{
			this._numb = "";
			this._speedMax = 180;
			this._kmWaste = 12;
		}

		/// <summary>
		/// Выбор приватного метода
		/// </summary>
		/// <param name="cars">список автомобилей</param>
		public void Act(List<Car> cars)
		{
			this.CarAppear(cars);
		}

        /// <summary>
        /// Создание автомобиля
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        protected void CarAppear(List<Car> cars)
		{
			if (cars.Count < 1)
			{
				Console.WriteLine("Для начала работы создайте хотя бы один автомобиль.\n");
			}
            Console.WriteLine("Что Вы хотите создать?\n1. Легковой автомобиль\n2. Грузовой автомобиль\n3. Автобус");
			int obj = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (obj)
			{
				case 1:
                    cars.Add(new Car());
                    Car last1 = cars.Last();
                    last1.InfoIn(cars);
                    break;

                case 2:
                    cars.Add(new Truck());
                    Car last2 = cars.Last();
                    last2.InfoIn(cars);
                    break;

                case 3:
                    cars.Add(new Bus());
                    Car last3 = cars.Last();
                    last3.InfoIn(cars);
                    break;
            }
			Console.WriteLine();
			Console.WriteLine("Чтобы начать работу, нажмите Enter. Если хотите создать ещё автомобиль, введите '+' и нажмите Enter.");
			string answ = Console.ReadLine();
			Console.WriteLine();
			if (answ == "+")
			{
				this.CarAppear(cars);
			}
			else
			{
				this.ChooseCar(cars);
			}
		}

        /// <summary>
        /// Заполнение информации об автомобиле
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        protected void InfoIn(List<Car> cars)
        {
            this.InfoInNumb(cars);
            this.InfoInVol();
            this.InfoInRun();
        }

        /// <summary>
        /// Заполнение номера автомобиля
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        protected void InfoInNumb(List<Car> cars)
        {
            Console.Write("Введите номер автомобиля (не может остаться пустым): ");
            string numb = Console.ReadLine();
            if (numb == "")
            {
                this.InfoInNumb(cars);
            }
            int a = 0;
            foreach (Car car in cars)
            {
                if (numb == car._numb)
                {
                    Console.WriteLine("Автомобиль с таким номером уже есть в базе.");
                    a++;
                    this.InfoInNumb(cars);
                }
            }
            if (a == 0)
            {
                this._numb = numb;
            }
        }

        /// <summary>
        /// Заполнение литража бака автомобиля
        /// </summary>
        protected void InfoInVol()
        {
            Console.Write("Введите литраж бака автомобиля (должен быть больше нуля): ");
            double vol = Convert.ToDouble(Console.ReadLine());
            if (vol <= 0)
            {
                this.InfoInVol();
            }
            else
            {
                this._volMax = vol;
            }
        }

        /// <summary>
        /// Заполнение пробега автомобиля
        /// </summary>
        protected void InfoInRun()
        {
            Console.Write("Введите пробег автомобиля (должен быть не меньше нуля): ");
            double run = Convert.ToDouble(Console.ReadLine());
            if (run < 0)
            {
                this.InfoInRun();
            }
            else
            {
                this._run = run;
            }
        }

        /// <summary>
        /// Выбор автомобиля
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        protected void ChooseCar(List<Car> cars)
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
					car.ChooseAction(cars);
					break;
				}
			}
			Console.WriteLine();
		}

        /// <summary>
        /// Вывод информации об автомобиле
        /// </summary>
        protected virtual void InfoOut()
        {
            Console.WriteLine("Тип: легковой автомобиль");
            Console.WriteLine("Номер: " + this._numb);
            Console.WriteLine("Бак: " + this._volCur + "/" + this._volMax + " л.");
            Console.WriteLine("Расход топлива на 100 км: " + this._kmWaste);
            Console.WriteLine("Пробег: " + this._run + " км.");
        }

        /// <summary>
        /// Выбор действия
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        protected void ChooseAction(List<Car> cars)
		{
			Console.WriteLine("\nВыберите действие:\n1 - информация об автомобиле\n2 - спланировать маршрут\n3 - заправка\n4 - расчёт количества возможных аварий\n5 - поездка\n6 - выбрать другой автомобиль\n7 - добавить автомобиль\nEnter - выход\n");
			string actChoice = Console.ReadLine();
			Console.WriteLine();
			switch (actChoice)
			{
				case "1":
					this.InfoOut();
					this.ChooseAction(cars);
					break;
					

                case "2":
                    this.Way();
                    this.ChooseAction(cars);
                    break;
                    

                case "3":
					this.Refill();
					this.ChooseAction(cars);
					break;
					

                case "4":
                    this.Search(cars);
                    break;
                    

                case "5":
					this.Move();
					this.ChooseAction(cars);
					break;
					

				case "6":
					this.ChooseCar(cars);
					break;
					

				case "7":
					this.CarAppear(cars);
					break;
					

				default:
					break;
					
			}
		}

        /// <summary>
        /// Планирование маршрута
        /// </summary>
        protected virtual void Way()
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
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            while (this._x1 <= this._x2)
            {
                x.Add(this._x1);
                this._x1++;
            }
            this._x1 = Math.Round(x1, 2);
            for (int i = 0; i < x.Count; i++)
            {
                y.Add(this._y1 + ((double)(this._y2 - this._y1) * (x[i] - this._x1) / (this._x2 - this._x1)));
            }
            for (int i = 0; i < x.Count; i++)
            {
                this._traj.Add(Convert.ToString(x[i] + " ; " + y[i]));
            }
            x.Clear();
            y.Clear();
            Console.WriteLine("Маршрут спланирован.");
        }

        /// <summary>
        /// Заправка бака
        /// </summary>
        protected void Refill()
		{
            Console.Write("Объём Вашего бака: " + this._volMax + " л.");
            Console.Write("Введите объём топлива: ");
			double vol = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
			if (vol <= Math.Round(this._volMax - this._volCur, 2))
			{
				this._volCur = vol;
				Console.WriteLine("Бак заправлен.");
			}
			else
			{
				Console.WriteLine("Вы не можете заправить объём топлива, превышающий литраж бака.");
				this.Refill();
			}
		}

        /// <summary>
        /// Поездка
        /// </summary>
        protected virtual void Move()
		{
			if (this._traj.Count == 0)
			{
				Console.WriteLine("Спланируйте поездку.");
				Way();
				Move();
			}
			else
			{
				Console.WriteLine("Общее расстояние: " + this._km + " км.\n");
				while (this._km > 0)
				{
                    if (this._speedCur == 0)
                    {
                        this.SpeedUp();
                    }
                    if (Math.Round((double)this._kmWaste / 100 * this._km, 2) <= this._volCur)
					{
						this._run += this._km;
						this._volCur -= Math.Round((double)this._kmWaste / 100 * this._km, 2);
                        this._km = 0;
                        this.Stop();
						Console.WriteLine("Вы доехали. Ваш текущий пробег " + this._run + " км.");
					}
					else
					{
						this._run += Math.Round((double)this._volCur / this._kmWaste * 100, 2);
						Console.WriteLine("Вы проехали " + Math.Round((double)this._volCur / this._kmWaste * 100, 2) + " км. Ваш текущий пробег: " + this._run + " км. Для дальнейшей поездки необходимо дозаправить " + Left() + " л топлива. Поедете дальше?");
						string answ = Console.ReadLine();
						if (answ == "нет")
						{
							this._volCur = 0;
                            this.Stop();
                            break;
						}
						else if (answ == "да")
						{
							this._km -= Math.Round((double)this._volCur / this._kmWaste * 100, 2);
							this._volCur = 0;
							this.Refill();
						}
                    }
				}
			}
		}

        /// <summary>
        /// Разгон
        /// </summary>
        protected virtual void SpeedUp()
		{
            Console.WriteLine("Необходимо разогнаться.");
            Console.Write("Разоганться на (введите число больше нуля): ");
			double v = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
			if (v <= 0)
			{
				this.SpeedUp();
			}
			else if (v > this._speedMax)
			{
				Console.WriteLine("Невозможно ехать быстрее " + this._speedMax + " км/ч.");
				this.SpeedUp();
			}
			else
			{
				this._speedCur = v;
                Console.WriteLine("Ваша скорость: " + this._speedCur + " км/ч.");
                if (this._speedCur >= 0 && this._speedCur <= 45)
				{
					this._kmWaste = 12;
				}
				else if (this._speedCur >= 46 && this._speedCur <= 100)
				{
					this._kmWaste = 9;
				}
				else if (this._speedCur >= 101 && this._speedCur <= 180)
				{
					this._kmWaste = 12.5;
				}
			}
		}

        /// <summary>
        /// Остановка
        /// </summary>
        protected void Stop()
		{
			this._speedCur = 0;
			this._kmWaste = 12;
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
        /// Выбор автомобиля для сверки траектории
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        protected void Search(List<Car> cars)
		{
            if (this._traj.Count == 0)
            {
                Console.WriteLine("Спланируйте поездку.");
                Way();
                Search(cars);
            }
            else
            {
                int comp = cars.Count(c => c._numb != this._numb && c._traj.Count != 0);
                if (comp > 0)
                {
                    Console.WriteLine("Выберите автомобиль для сверки траекторий: ");
                    int i = 1;
                    foreach (Car car in cars)
                    {
                        if (car._numb != this._numb && car._traj.Count != 0)
                        {
                            Console.WriteLine(i + ". " + car._numb);
                            i++;
                        }
                    }
                    int index = Convert.ToInt32(Console.ReadLine()) - 1;
					this.Accident(cars[index], cars);
                }
                else
                {
                    Console.WriteLine("Нет автомобилей, доступных для сверки траектории.");
                    this.ChooseAction(cars);
                }
            }
        }

        /// <summary>
        /// Расчёт возможного количества аварий
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        protected void Accident(Car car, List<Car> cars)
		{
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
			Console.WriteLine("Количество возможных аварий с участием " + this._numb + " и " + car._numb + ": " + acc);
			this.ChooseAction(cars);
        }
	}
}