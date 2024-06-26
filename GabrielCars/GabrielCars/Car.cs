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
		private string _numb; //номер
	        private double _volMax; //объём бака (л)
	        private double _volCur; //объём топлива в баке (л)
	        private double _speedCur; //скорость движения (км/ч)
	        private double _speedMax; //максимальная скорость движения (км/ч)
	        private double _kmWaste; //расход топлива на 100 км (л)
	        private double _km; //расстояние (км)
	        private List<string> _traj = new List<string>(); //траектория движения
	        private double _run; //пробег (км)

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
	        private void CarAppear(List<Car> cars)
		{
			if (cars.Count < 1)
			{
				Console.WriteLine("Для начала работы создайте хотя бы один автомобиль.\n");
			}
	                cars.Add(new Car());
	                Car last1 = cars.Last();
	                last1.InfoIn(cars);
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
	        private void InfoIn(List<Car> cars)
	        {
		        this.InfoInNumb(cars);
		        this.InfoInVol();
		        this.InfoInRun();
	        }
	
	        /// <summary>
	        /// Заполнение номера автомобиля
	        /// </summary>
	        /// <param name="cars">список автомобилей</param>
	        private void InfoInNumb(List<Car> cars)
	        {
		        Console.Write("Введите номер (не может остаться пустым): ");
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
	        private void InfoInVol()
	        {
		        Console.Write("Введите литраж бака (должен быть больше нуля): ");
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
	        private void InfoInRun()
	        {
		        Console.Write("Введите пробег (должен быть не меньше нуля): ");
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
					car.ChooseAction(cars);
					break;
				}
			}
			Console.WriteLine();
		}
	
	        /// <summary>
	        /// Вывод информации об автомобиле
	        /// </summary>
	        private void InfoOut()
	        {
		        Console.WriteLine("Тип: легковой автомобиль");
		        Console.WriteLine("Номер: " + this._numb);
		        Console.WriteLine("Бак: " + this._volCur + "/" + this._volMax + " л.");
		        Console.WriteLine("Пробег: " + this._run + " км.");
	        }
	
	        /// <summary>
	        /// Выбор действия
	        /// </summary>
	        /// <param name="cars">список автомобилей</param>
	        private void ChooseAction(List<Car> cars)
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
	        private void Way()
	        {
		        Console.WriteLine("Введите координаты начала пути:");
		        Console.Write("x1: ");
		        double x1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
		        double x0 = x1;
		        Console.Write("y1: ");
		        double y1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
		        Console.WriteLine("Введите координаты конца пути:");
		        Console.Write("x2: ");
		        double x2 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
		        Console.Write("y2: ");
		        double y2 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
		        this._km = Math.Round(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)), 2);
		        if (x1 == x2)
		        {
				this.Way();
		        }
		        else
		        {
		        	if (x1 < x2)
				{
				        while (x1 <= x2)
				        {
					        this._traj.Add(Convert.ToString(x1));
					        x1++;
				        }
				}
			        else if (x1 > x2)
			        {
				        while (x1 >= x2)
				        {
					        this._traj.Add(Convert.ToString(x1));
					        x1--;
				        }
			        }
			        x1 = x0;
			        for (int i = 0; i < this._traj.Count; i++)
			        {
			        	this._traj[i] += ";" + Convert.ToString(y1 + ((double)(y2 - y1) * (Convert.ToDouble(this._traj[i]) - x1) / (x2 - x1)));
			        }
			        Console.WriteLine("Маршрут спланирован.");
		        }
	        }
	
	        /// <summary>
	        /// Заправка бака
	        /// </summary>
	        private void Refill()
		{
		        Console.WriteLine("Объём Вашего бака: " + this._volMax + " л.");
		        Console.Write("Введите объём топлива: ");
			double vol = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
		        if (vol == 0)
		        {
				Console.WriteLine("Вы не можете заправить нулевой объём топлива.");
			        this.Refill();
		        }
		        else
		        {
			        if (vol <= Math.Round(this._volMax - this._volCur, 2))
			        {
				        this._volCur += vol;
				        Console.WriteLine("Бак заправлен.");
				}
			        else
			        {
				        Console.WriteLine("Вы не можете заправить объём топлива, превышающий литраж бака.");
				        this.Refill();
			        }
		        } 
	        }
	
	        /// <summary>
	        /// Поездка
	        /// </summary>
	        private void Move()
	        {
		        if (this._volCur == 0)
		        {
				Console.WriteLine("Заправьтесь.");
			        this.Refill();
			        this.Move();
		        }
		        else if (this._traj.Count == 0)
			{
				Console.WriteLine("Спланируйте маршрут.");
				this.Way();
				this.Move();
			}
			else
			{
				Console.WriteLine("Общее расстояние: " + this._km + " км.");
				while (this._km > 0)
				{
				        if (this._speedCur == 0)
				        {
				        	this.SpeedUpIn();
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
						Console.WriteLine("Вы проехали " + Math.Round((double)this._volCur / this._kmWaste * 100, 2) + " км. Ваш текущий пробег: " + this._run + " км. Для дальнейшей поездки всего необходимо " + Left() + " л топлива. Поедете дальше?");
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
			        this._traj.Clear();
			}
		}
	
	        /// <summary>
	        /// Ввод данных для разгона и изменение расхода топлива
	        /// </summary>
	        private void SpeedUpIn()
	        {
		        Console.WriteLine("Необходимо разогнаться.");
		        Console.Write("Разоганться на (введите число больше нуля): ");
		        double v = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
		        if (v <= 0)
		        {
		        	this.SpeedUpIn();
		        }
		        else if (v > this._speedMax)
		        {
			        Console.WriteLine("Невозможно ехать быстрее " + this._speedMax + " км/ч.");
			        this.SpeedUpIn();
		        }
		        else
		        {
			        this.SpeedUp(v);
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
	        /// Разгон
	        /// </summary>
	        /// <param name="v">величина разгона (км/ч)</param>
	        private void SpeedUp(double v)
	        {
	        	this._speedCur = v; 
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
	        private void Search(List<Car> cars)
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
	        private void Accident(Car car, List<Car> cars)
		{
			int acc = 0;
		        string place = "";
			for (int i = 1; i <= this._traj.Count() - 1; i++)
			{
				for (int j = 1; j <= this._traj.Count() - 1; j++)
				{
				        if (this._traj[i] == car._traj[j])
				        {
						acc++;
					        place = this._traj[i];
				        }
				}
			}
		        Console.WriteLine("Автомобили: " + this._numb + ", " + car._numb);
		        if (acc == 0)
		        {
		        	Console.WriteLine("Траектории движения автомобилей не пересекаются.");
		        }
		        else if (acc == 1)
		        {
		        	Console.WriteLine("Возможный аварийный участок: " + place);
		        }
		        else
		        {
				string start = "";
				string fin = "";
				if (this._traj[1] == car._traj[1])
				{
					start = this._traj[1];
				}
				else if (Convert.ToDouble(this._traj[1].Remove(this._traj[1].IndexOf(";"), 1)) > Convert.ToDouble(car._traj[1].Remove(car._traj[1].IndexOf(";"), 1)))
				{
					start = this._traj[1];
				}
				else
				{
					start = car._traj[1];
				}
				if (Convert.ToDouble(this._traj.Last().Remove(this._traj.Last().IndexOf(";"), 1)) == Convert.ToDouble(this._traj.Last().Remove(this._traj.Last().IndexOf(";"), 1)))
				{
					fin = this._traj.Last();
				}
				else if (Convert.ToDouble(this._traj.Last().Remove(this._traj.Last().IndexOf(";"), 1)) > Convert.ToDouble(this._traj.Last().Remove(this._traj.Last().IndexOf(";"), 1)))
				{
					fin = this._traj.Last();
				}
				else
				{
					fin = this._traj.Last();
				}
				Console.WriteLine("Возможный аварийный участок: [" + start + " - " + fin + "]");
		        }
		        Console.WriteLine("Количество возможных аварий: " + acc);
			this.ChooseAction(cars);
	        }
	}
}
