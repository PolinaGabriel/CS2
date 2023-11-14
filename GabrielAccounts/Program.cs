using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GabrielAccounts
{
	internal class Program
	{
		static void Main()
		{
			List<Account> accounts = new List<Account>(); //список счетов
			Account account = new Account(); //объект для взаимодействия
			account.Act(accounts);
		}
	}
}