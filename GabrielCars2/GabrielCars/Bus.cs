using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielCars
{
    internal class Bus:Car
    {
        private int _passCur; //число пассажиров (шт)
        private int _passMax; //число мест (шт)
        private double _percent; //коэффициент для скорости
        private double _track; //расстояние между двумя точками траектории поездки

        /// <summary>
        /// Создание грузовика
        /// </summary>
        public Bus()
        {
            this._numb = "";
            this._speedMax = 180;
            this._kmWaste = 12;
            this._passMax = 30;
            this._percent = 1;
        }

        /// <summary>
        /// Вывод информации об автомобиле
        /// </summary>
        protected override void InfoOut()
        {
            Console.WriteLine("Тип: автобус");
            Console.WriteLine("Номер: " + this._numb);
            Console.WriteLine("Бак: " + this._volCur + "/" + this._volMax + " л.");
            Console.WriteLine("Пробег: " + this._run + " км.");
        }

        /// <summary>
        /// Планирование маршрута
        /// </summary>
        protected override void Way() //отличается от базового только вызовом метода с остановками
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
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            if (x1 < x2)
            {
                while (x1 <= x2)
                {
                    x.Add(x1);
                    x1++;
                }
            }
            else if (x1 > x2)
            {
                while (x1 >= x2)
                {
                    x.Add(x1);
                    x1--;
                }
            }
            x1 = x0;
            for (int i = 0; i < x.Count; i++)
            {
                y.Add(y1 + ((double)(y2 - y1) * (x[i] - x1) / (x2 - x1)));
            }
            for (int i = 0; i < x.Count; i++)
            {
                this._traj.Add(Convert.ToString(x[i] + " ; " + y[i]));
            }
            x.Clear();
            y.Clear();
            this.BusStop(); //остановки
            Console.WriteLine("Маршрут спланирован.");
        }

        /// <summary>
        /// Выбор остановок на маршруте и расчёт расстояния между ними
        /// </summary>
        private void BusStop()
        {
            this._traj[this._traj.Count - 1] += ".";
            Console.WriteLine("Выберите координаты остановки:");
			int a = 1;
			foreach (string i in this._traj)
			{
				if (this._traj.IndexOf(i) != 0 && this._traj.IndexOf(i) != this._traj.Count - 1)
				{
					Console.WriteLine(a + ") " + i);
					a++;
				}
			}
			int index = Convert.ToInt32(Console.ReadLine());
			this._traj[index] += ".";
			Console.WriteLine("Чтобы закончить планирование, нажмите Enter. Если хотите добавить ещё остановку, введите '+' и нажмите Enter.");
			string answ = Console.ReadLine();
            if (answ == "+")
            {
                this.BusStop();
            }
            else
            {
                for (int i = this._traj.Count - 2; i >= 0; i--)
                {
                    this._traj.Add(this._traj[i]);
                }
            }
		}

        /// <summary>
        /// Поездка
        /// </summary>
        protected override void Move()
        {
            if (this._traj.Count == 0)
            {
                Console.WriteLine("Спланируйте маршрут.");
                Way();
                Move();
            }
            else
            {
                Console.WriteLine("Общее расстояние: " + this._km + " км.\n");
                foreach (string point in this._traj)
                {
                    if (point == this._traj[0])
                    {
                        this.PassIn();
                    }
                    else if (point == this._traj[this._traj.Count - 1])
                    {
                        this.Track(point);
                        this.PassOutDepo();
                        Console.WriteLine("Вы вернулись в депо. Ваш текущий пробег " + this._run + " км.");
                    }
                    else
                    {
                        this.Track(point);
                    }
                }
            }
        }

        /// <summary>
        /// Часть поездки между двумя точками маршрута
        /// </summary>
        /// <param name="point">точка маршрута, в которую приехал автобус</param>
        private void Track(string point)
        {
            if (this._speedCur == 0)
            {
                this.SpeedUp();
            }
            Fuel:
            if (Math.Round((double)this._kmWaste / 100 * this._track, 2) <= this._volCur)
            {
                this._volCur -= Math.Round((double)this._kmWaste / 100 * this._track, 2);
            }
            else
            {
                Console.WriteLine("Текущий объём топлива в баке: " + this._volCur + " л. Необходима дозаправка.");
                this._volCur = 0;
                this.Refill();
                goto Fuel;
            }
            if (point.Contains('.'))
            {
                Console.WriteLine("Остановка.");
                this.Stop();
                this.PassIn();
                this.PassOut();
            }
            this._km -= this._track;
            this._run += this._track;
        }

        /// <summary>
        /// Разгон
        /// </summary>
        protected override void SpeedUp()
        {
            Console.WriteLine("Необходимо разогнаться.");
            Console.Write("Разоганться на (введите число больше нуля): ");
            double v = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            if (v <= 0)
            {
                this.SpeedUp();
            }
            else if (v * this._percent > this._speedMax)
            {
                Console.WriteLine("Невозможно ехать быстрее " + this._speedMax + " км/ч.");
                this.SpeedUp();
            }
            else
            {
                this._speedCur = v * this._percent;
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
        /// Вход пассажиров
        /// </summary>
        private void PassIn()
        {
            Console.Write("Число вошедших пассажиров: ");
            int pass = Convert.ToInt32(Console.ReadLine());
            if (pass < 0)
            {
                Console.WriteLine("Число пассажиров должно быть больше нуля.");
                this.PassIn();
            }
            else if (this._passCur + pass > this._passMax)
            {
                Console.Write("В автобусе " + (this._passMax - this._passCur) + " свободных мест.");
                this.PassIn();
            }
            else
            {
                this._passCur += pass;
                if (this._passCur < 2)
                {
                    this._percent = 1;
                }
                else if (this._passCur >= 2 && this._passCur <= 14)
                {
                    this._percent = 0.6;
                }
                else if (this._passCur >= 15 && this._passCur <= 30)
                {
                    this._percent = 0.2;
                }
            }
        }

        /// <summary>
        /// Выход пассажиров
        /// </summary>
        private void PassOut()
        {
            Console.Write("Число вышедших пассажиров: ");
            int pass = Convert.ToInt32(Console.ReadLine());
            if (pass < 0)
            {
                Console.WriteLine("Число пассажиров должно быть больше нуля.");
                this.PassOut();
            }
            else if (pass > this._passCur)
            {
                Console.WriteLine("В автобусе меньше пассажиров.");
                this.PassOut();
            }
            else
            {
                this._passCur = pass;
                if (this._passCur < 2)
                {
                    this._percent = 1;
                }
                else if (this._passCur >= 2 && this._passCur <= 14)
                {
                    this._percent = 0.6;
                }
                else if (this._passCur >= 15 && this._passCur <= 30)
                {
                    this._percent = 0.2;
                }
            }
        }

        /// <summary>
        /// Выход пассажиров в депо
        /// </summary>
        private void PassOutDepo()
        {
            Console.WriteLine("Число вышедших пассажиров: " + this._passCur);
            this._passCur = 0;
        }
    }
}
