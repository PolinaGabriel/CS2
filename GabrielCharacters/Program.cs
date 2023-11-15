using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GabrielCharacters
{
	internal class Program
	{
		static void Main()
		{
			List<Character> aliveChar = new List<Character>(); //список живых персонажей
			List<Character> deadChar = new List<Character>(); //список мёртвых персонажей
			Character playChar = new Character(); //объект для игры
			playChar.Play(aliveChar, deadChar);
		}
	}
}