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
            List<Character> aliveChar = new List<Character>(); //список живых (активных) персонажей
            List<Character> deadChar = new List<Character>(); //список мёртвых персонажей
			Character playChar = new Character(); //объект для игры
            playChar.Play(1, aliveChar, deadChar);
            playChar.Play(2, aliveChar, deadChar);
			playChar.Play(3, aliveChar, deadChar);
			playChar.Play(4, aliveChar, deadChar);
        }
    }
}