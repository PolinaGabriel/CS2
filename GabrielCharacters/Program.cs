﻿using System;
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
		//Проблемы/недоработки:
		//сделать поиск врагов в точке после каждого выбора персонажа (чтобы можно было продолжить драку, если в ней ты потратил всё здоровье, а потом тебя полечили)
		//если в ходе сражения все умирают, имя того, за кого ты играл, не выводится в статистике
	}
}