using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Autos
{
	public class Avto
	{
		private string nom; //номер
		private double bak; //литраж бака
		private double ras; //расход топлива на 100 км
		private double top; //объём бензина в баке
		private double x1; //начальная координата x
		private double y1; //начальная координата y
		private double x2; //конечная координата x
		private double y2; //конечная координата y
		private double km; //расстояние (км)
		private List<double> x = new(); //координаты x
		private List<double> y = new(); //координаты y
		private List<string> traj = new(); //траектория движения
		private double prob; //пробег
		private int avtoCheck; //проверка совпадения номера
        	private int acc; //количество возможных аварий

	        /// <summary>
	        /// Выбор приватного метода
	        /// </summary>
	        /// <param name="a"></param>
	        public void Methods(int operChoice, string nom, double bak, double ras, double prob, double x1, double y1, double x2, double y2, Avto[] avtos)
		{
			switch (operChoice)
			{
				case 1:
				{
					InfoIn(nom, bak, ras, prob);
				        break;
				}
			
				case 2:
				{
				        InfoOut();
				        break;
			        }
			
			        case 3:
			        {
				        Zapravka();
				        break;
			        }
			
				case 4:
			        {
					Move(x1, y1, x2, y2);
				        break;
			        }
			                    
			        case 5:
			        {
				        Accident(avtos, nom);
				        break;
			        }
		        }
		}
	
	        /// <summary>
	        /// Ввод информации об авто
	        /// </summary>
	        /// <param name="nom"></param>
	        /// <param name="bak"></param>
	        /// <param name="ras"></param>
	        /// <param name="prob"></param>
		private void InfoIn(string nom, double bak, double ras, double prob)
		{
			this.nom = nom;
			this.bak = Math.Round(bak, 2);
			this.ras = Math.Round(ras, 2);
			this.prob = Math.Round(prob, 2);
		        Console.WriteLine("Информация внесена.");
	        }
	
	        /// <summary>
	        /// Вывод информации об авто
	        /// </summary>
	        private void InfoOut()
	        {
		        Console.WriteLine("Номер авто: " + this.nom);
		        Console.WriteLine("Литраж бензобака: " + this.bak);
		        Console.WriteLine("Расход бензина на 100 км: " + this.ras);
		        Console.WriteLine("Пробег: " + this.prob);
		        Console.WriteLine("Объём заправленного бензина: " + this.top);
	        }
	
	        /// <summary>
	        /// Заправка бака
	        /// </summary>
	        private void Zapravka()
	        {
		        Console.Write("Введите объём бензина: ");
		        double top = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
		        if (top <= Math.Round(this.bak - this.top, 2))
		        {
			        this.top = top;
			        Console.WriteLine("Бензин заправлен.");
		        }
		        else
		        {
		        	Console.WriteLine("Вы не можете заправить объём бензина, превышающий литраж бензобака. Повторите ввод.");
		        }
	        }
	
	        /// <summary>
	        /// Поездка
	        /// </summary>
	        /// <param name="x1"></param>
	        /// <param name="y1"></param>
	        /// <param name="x2"></param>
	        /// <param name="y2"></param>
		private void Move(double x1, double y1, double x2, double y2)
		{
			Distance(x1, y1, x2, y2);
		        Console.WriteLine("Общее расстояние: " + this.km + " км.\n");
			while (this.km > 0)
			{
				if (Math.Round((double)this.ras / 100 * this.km, 2) <= this.top)
				{
					this.prob += this.km;
					this.top -= Math.Round((double)this.ras / 100 * this.km, 2);
					Console.WriteLine("Вы доехали. Ваш текущий пробег " + this.prob + " км.");
					break;
				}
				else
				{
					this.prob += Math.Round((double)this.top / this.ras * 100, 2);
					Console.WriteLine("Вы проехали " + Math.Round((double)this.top / this.ras * 100, 2) + " км. Ваш текущий пробег: " + this.prob + " км. Для дальнейшей поездки необходимо дозаправить " + Ostatok() + " л бензина. Поедете дальше?");
					string answ = Console.ReadLine();
					if (answ == "нет")
					{
						this.top = 0;
						break;
					}
					else if (answ == "да")
					{
						this.km -= Math.Round((double)this.top / this.ras * 100, 2);
						this.top = 0;
						Zapravka();
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
	        private void Distance(double x1, double y1, double x2, double y2)
	        {
		        this.x1 = Math.Round(x1, 2);
		        this.y1 = Math.Round(y1, 2);
		        this.x2 = Math.Round(x2, 2);
		        this.y2 = Math.Round(y2, 2);
			this.km = Math.Round(Math.Sqrt(Math.Pow((this.x2 - this.x1), 2) + Math.Pow((this.y2 - this.y1), 2)), 2);
	        }
	
	        /// <summary>
	        /// Расчёт недостающего объёма топлива
	        /// </summary>
	        /// <returns></returns>
	        private double Ostatok()
	        {
	        	return Math.Round((((double)this.ras / 100) * this.km) - this.top, 2);
	        }
	
	        /// <summary>
	        /// Расчёт возможного количества аварий
	        /// </summary>
	        /// <param name="avtos"></param>
	        /// <param name="nom"></param>
	        private void Accident(Avto[] avtos, string nom)
	        {
		        if (this.nom == nom)
		        {
				Console.WriteLine("Вы не можете свериться с собственной траекторией.");
		        }
		        else
		        {
			        foreach (Avto avto in avtos)
				{
				        if (avto.nom == nom)
				        {
						this.avtoCheck = 1;
					        this.Trajectory(this.x1, this.y1, this.x2, this.y2);
					        avto.Trajectory(avto.x1, avto.y1, avto.x2, avto.y2);
					        for (int i = 0; i < this.traj.Count; i++)
						{
						        for (int j = 0; j < avto.traj.Count; j++)
						        {
							        if (this.traj[i] == avto.traj[j])
							        {
							        	this.acc++;
							        }
						        }
					        }
					        Console.WriteLine("Количество возможных аварий с участием выбранных машин: " + this.acc);
					        this.x.Clear();
					        avto.x.Clear();
					        this.y.Clear();
					        avto.y.Clear();
					        this.traj.Clear();
					        avto.traj.Clear();
					        this.acc = 0;
					}
			        }
			        if (this.avtoCheck == 0)
				{
			        	Console.WriteLine("Номер машины для сверки не существует или введён неверно.");
			        }
			        this.avtoCheck = 0;
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
		        this.x1 = Math.Round(x1, 2);
		        this.y1 = Math.Round(y1, 2);
		        this.x2 = Math.Round(x2, 2);
		        this.y2 = Math.Round(y2, 2);
		        while (this.x1 <= this.x2)
		        {
		        	x.Add(this.x1);
		        	this.x1++;
		        }
		        this.x1 = Math.Round(x1, 2);
		        for (int i = 0; i < x.Count; i++)
		        {
		        	y.Add(this.y1 + ((double)(this.y2 - this.y1) * (x[i] - this.x1) / (this.x2 - this.x1)));
		        }
		        for (int i = 0; i < x.Count; i++)
		        {
		        	this.traj.Add(Convert.ToString(x[i] + " ; " + y[i]));
		        }
	        }
    	}
}
