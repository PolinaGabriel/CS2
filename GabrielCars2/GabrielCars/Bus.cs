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
        private double _track; //расстояние между двумя точками траектории маршрута (км)

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
        protected override void Way() //отличается от базового только вызовом метода с остановками, от Bus названиями
        {
            Console.WriteLine("Введите координаты депо:");
            Console.Write("x1: ");
            double x1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            double x0 = x1;
            Console.Write("y1: ");
            double y1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            Console.WriteLine("Введите координаты последней остановки:");
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
                this.BusStop(x1, x2);
                Console.WriteLine("Маршрут спланирован.");
            }
        }

        /// <summary>
        /// Выбор остановок на маршруте и расчёт расстояния между ними
        /// </summary>
        /// <param name="x1">координата x начала маршрута</param>
        /// <param name="x2">координата x конца маршрута</param>
        private void BusStop(double x1, double x2)
        {
            Console.Write("Введите координату x остановки: ");
            double x = Convert.ToDouble(Console.ReadLine());
            if (x == x1)
            {
                Console.WriteLine("Депо не может быть остановкой.");
                this.BusStop(x1, x2);
            }
            else if (x == x2)
            {
                Console.WriteLine("Последняя остановка уже выбрана.");
                this.BusStop(x1, x2);
            }
            else if (x1 < x2 && (x < x1 || x > x2))
            {
                Console.WriteLine("Координата не принадлежит траектории маршрута.");
                this.BusStop(x1, x2);
            }
            else if (x1 > x2 && (x > x1 || x < x2))
            {
                Console.WriteLine("Координата не принадлежит траектории маршрута.");
                this.BusStop(x1, x2);
            }
            else
            {
                string stopX = Convert.ToString(Math.Round(x, 2));
                int index = -1;
                foreach (string i in this._traj)
                {
                    if (i.Substring(0, i.IndexOf(";")) == stopX && i.Contains(".") == false)
                    {
                        index = this._traj.IndexOf(i);
                    }
                }
                if (index > 0)
                {
                    this._traj[index] += ".";
                }
                Console.WriteLine("Чтобы закончить планирование, нажмите Enter. Если хотите добавить ещё остановку, введите '+' и нажмите Enter.");
                string answ = Console.ReadLine();
                if (answ == "+")
                {
                    this.BusStop(x1, x2);
                }
                else
                {
                    this._traj[this._traj.Count - 1] += ".";
                    for (int i = this._traj.Count - 2; i >= 0; i--)
                    {
                        this._traj.Add(this._traj[i]);
                    }
                    this._traj[0] += "s";
                    this._traj[this._traj.Count - 1] += "f";
                    this._track = Math.Round((double)this._km / (this._traj.Count - 1), 2);
                }
            }
		}

        /// <summary>
        /// Поездка
        /// </summary>
        protected override void Move()
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
                foreach (string point in this._traj)
                {
                    if (point.Contains("s") == true)
                    {
                        Console.WriteLine("Вы находитесь в депо.");
                        this.PassIn();
                    }
                    else if (point.Contains("f") == true)
                    {
                        this.Track(point);
                        this._run += this._km;
                        this._km = 0;
                        Console.WriteLine("Ваш текущий пробег " + this._run + " км.");
                    }
                    else
                    {
                        this.Track(point);
                    }
                }
                this._traj.Clear();
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
                this.SpeedUpIn();
            }
            Fuel:
            if (Math.Round((double)this._kmWaste / 100 * this._track, 2) <= this._volCur)
            {
                this._volCur -= Math.Round((double)this._kmWaste / 100 * this._track, 2);
                if (point.Contains(".") == true)
                {
                    Console.WriteLine("Остановка " + point.Substring(0, point.Length - 1));
                    this.Stop();
                    this.PassOut(point);
                    this.PassIn();
                }
                else if (point.Contains("f") == true)
                {
                    Console.WriteLine("Вы вернулись в депо.");
                    this.Stop();
                    this.PassOut(point);
                }
            }
            else
            {
                Console.WriteLine("Точка " + point + ". Текущий объём топлива в баке: " + this._volCur + " л. Необходима дозаправка."); //сильно длинный объём топлива
                this.Refill();
                goto Fuel;
            } 
        }

        /// <summary>
        /// Разгон
        /// </summary>
        /// <param name="v">величина разгона (км/ч)</param>
        protected override void SpeedUp(double v)
        {
            this._speedCur = v * this._percent;
        }

        /// <summary>
        /// Вход пассажиров
        /// </summary>
        /// <param name="point">точка маршрута, в которую приехал автобус</param>
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
                Console.WriteLine("В автобусе " + (this._passMax - this._passCur) + " свободных мест.");
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
        /// <param name="point">точка маршрута, в которую приехал автобус</param>
        private void PassOut(string point)
        {
            if (point.Contains("f") == true)
            {
                Console.WriteLine("Число вышедших пассажиров: " + this._passCur);
                this._passCur = 0;
            }
            else
            {
                Console.Write("Число вышедших пассажиров: ");
                int pass = Convert.ToInt32(Console.ReadLine());
                if (pass < 0)
                {
                    Console.WriteLine("Число пассажиров должно быть больше нуля.");
                    this.PassOut(point);
                }
                else if (pass > this._passCur)
                {
                    Console.WriteLine("В автобусе меньше пассажиров.");
                    this.PassOut(point);
                }
                else
                {
                    this._passCur -= pass;
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
        }
    }
}