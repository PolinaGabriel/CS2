using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GabrielCars
{
	internal class Program
	{
		static void Main()
		{
			List<Car> cars = new List<Car>(); //список автомобилей
			Car car = new Car(); //объект для взаимодействия
			car.Act(cars);
		}
	}
}