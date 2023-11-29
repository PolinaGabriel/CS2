using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielCars
{
    internal class Truck:Car
    {
        private double _cargoCur; //масса груза (кг)
        private double _cargoMax; //максимальная разрешённая масса груза (кг)
        private double _percent; //коэффициент для скорости
        private double _track; //расстояние между двумя точками траектории маршрута (км)

        /// <summary>
        /// Создание грузовика
        /// </summary>
        public Truck()
        {
            this._numb = "";
            this._speedMax = 180;
            this._kmWaste = 12;
            this._cargoMax = 2000;
            this._percent = 1;
        }

        /// <summary>
        /// Вывод информации об автомобиле
        /// </summary>
        protected override void InfoOut()
        {
            Console.WriteLine("Тип: грузовой автомобиль");
            Console.WriteLine("Номер: " + this._numb);
            Console.WriteLine("Бак: " + this._volCur + "/" + this._volMax + " л.");
            Console.WriteLine("Пробег: " + this._run + " км.");
        }

        /// <summary>
        /// Планирование маршрута
        /// </summary>
        protected override void Way() //отличается от базового только вызовом метода с остановками, от Truck названиями
        {
            Console.WriteLine("Введите координаты базы:");
            Console.Write("x1: ");
            double x1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            double x0 = x1;
            Console.Write("y1: ");
            double y1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            Console.WriteLine("Введите координаты точки разгрузки:");
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
                this.TruckStop(x1, x2);
                Console.WriteLine("Маршрут спланирован.");
            }
        }

        /// <summary>
        /// Выбор остановок на маршруте и расчёт расстояния между ними
        /// </summary>
        /// <param name="x1">координата x начала маршрута</param>
        /// <param name="x2">координата x конца маршрута</param>
        private void TruckStop(double x1, double x2)
        {
            Console.Write("Введите координату x точки погрузки: ");
            double x = Convert.ToDouble(Console.ReadLine());
            if (x == x1)
            {
                Console.WriteLine("База не может быть точкой погрузки.");
                this.TruckStop(x1, x2);
            }
            else if (x == x2)
            {
                Console.WriteLine("Точка разгрузки не может быть точкой погрузки.");
                this.TruckStop(x1, x2);
            }
            else if (x1 < x2 && (x < x1 || x > x2))
            {
                Console.WriteLine("Координата не принадлежит траектории маршрута.");
                this.TruckStop(x1, x2);
            }
            else if (x1 > x2 && (x > x1 || x < x2))
            {
                Console.WriteLine("Координата не принадлежит траектории маршрута.");
                this.TruckStop(x1, x2);
            }
            else
            {
                string stopX = Convert.ToString(Math.Round(x, 2));
                int index = -1;
                foreach (string i in this._traj)
                {
                    if (i.Substring(0, i.IndexOf(";")) == stopX)
                    {
                        index = this._traj.IndexOf(i);
                    }
                }
                this._traj[this._traj.Count - 1] += "-";
                for (int i = this._traj.Count - 2; i >= 0; i--)
                {
                    this._traj.Add(this._traj[i]);
                }
                if (index > 0)
                {
                    this._traj[index] += "+";
                }
                this._traj[0] += "s";
                this._traj[this._traj.Count - 1] += "f";
                this._track = Math.Round((double)this._km / (this._traj.Count - 1), 2);
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
                Console.WriteLine("Вы находитесь на базе.");
                foreach (string point in this._traj)
                {
                    if (point.Contains("s") == false)
                    {
                        if (point.Contains("f") == true)
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
                }
                this._traj.Clear();
            }
        }

        /// <summary>
        /// Часть поездки между двумя точками маршрута
        /// </summary>
        /// <param name="point">точка маршрута, в которую приехал грузовик</param>
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
                if (point.Contains("+") == true)
                {
                    this.Stop();
                    this.CargoIn(point);
                }
                else if (point.Contains("-") == true)
                {
                    this.Stop();
                    this.CargoOut(point);
                }
                else if (point.Contains("f") == true)
                {
                    this.Stop();
                    Console.WriteLine("Вы вернулись на базу.");
                }
            }
            else
            {
                Console.WriteLine("Текущий объём топлива в баке: " + this._volCur + " л. Необходима дозаправка.");
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
        /// Погрузка
        /// </summary>
        /// <param name="point">точка маршрута, в которую приехал грузовик</param>
        private void CargoIn(string point)
        {
            Console.WriteLine("Вы прибыли в точку погрузки (" + point.Substring(0, point.Length - 1) + ").");
            Console.Write("Введите массу груза (больше нуля): ");
            double cargo = Convert.ToDouble(Console.ReadLine());
            if (cargo < 0)
            {
                this.CargoIn(point);
            }
            else if (cargo > this._cargoMax)
            {
                Console.WriteLine("Масса груза не может превышать " + this._cargoMax + " кг");
                this.CargoIn(point);
            }
            else
            {
                this._cargoCur = cargo;
                if (this._cargoCur < 100)
                {
                    this._percent = 1;
                }
                else if (this._cargoCur >= 100 && this._cargoCur <= 1000)
                {
                    this._percent = 0.6;
                }
                else if (this._cargoCur >= 1001 && this._cargoCur <= 2000)
                {
                    this._percent = 0.2;
                }
                Console.WriteLine("Груз принят.");
            }
        }

        /// <summary>
        /// Разгрузка
        /// </summary>
        /// <param name="point">точка маршрута, в которую приехал грузовик</param>
        private void CargoOut(string point)
        {
            Console.Write("Вы прибыли в точку разгрузки (" + point.Substring(0, point.Length - 1) + ").\n");
            this._cargoCur = 0;
            this._percent = 1;
            Console.WriteLine("Груз сдан.");
        }
    }
}