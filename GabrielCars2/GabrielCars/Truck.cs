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
        private double _track; //расстояние между двумя точками траектории поездки

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
            Console.WriteLine("Расход топлива на 100 км: " + this._kmWaste);
            Console.WriteLine("Пробег: " + this._run + " км.");
        }

        /// <summary>
        /// Планирование маршрута
        /// </summary>
        protected override void Way()
        {
            Console.WriteLine("Введите координаты базы:");
            Console.Write("x1: ");
            double x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("y1: ");
            double y1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите координаты точки разгрузки:");
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
            Console.WriteLine("Выберите координаты точки погрузки:");
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
            this._traj[this._traj.Count - 1] += "-";
            for (int i = this._traj.Count - 2; i >= 0; i--)
            {
                this._traj.Add(this._traj[i]);
            }
			this._traj[index] += "+";
            this._track = Math.Round((double)this._km / (this._traj.Count - 1), 2);
            Console.WriteLine("Маршрут спланирован.");
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
                    if (point != this._traj[0])
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
                        if (point.Contains('+'))
                        {
                            this.Stop();
                            this.CargoIn();
                        }
                        else if (point.Contains('-'))
                        {
                            this.Stop();
                            this.CargoOut();
                        }
                        this._km -= this._track;
                        this._run += this._track;
                    } 
                }
                Console.WriteLine("Вы вернулись на базу. Ваш текущий пробег " + this._run + " км.");
            }
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
        /// Погрузка
        /// </summary>
        private void CargoIn()
        {
            Console.Write("Вы прибыли в точку погрузки.");
            Console.Write("Введите массу груза (больше нуля): ");
            double cargo = Convert.ToDouble(Console.ReadLine());
            if (cargo < 0)
            {
                this.CargoIn();
            }
            else if (cargo > this._cargoMax)
            {
                Console.WriteLine("Масса груза не может превышать " + this._cargoMax + " кг");
                this.CargoIn();
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
        private void CargoOut()
        {
            Console.Write("Вы прибыли в точку разгрузки.");
            this._cargoCur = 0;
            this._percent = 1;
            Console.WriteLine("Груз сдан.");
        }
    }
}
