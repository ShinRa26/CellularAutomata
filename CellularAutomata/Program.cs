using System;

namespace CellularAutomata
{
	class Program
	{
		public static void Main (string[] args)
		{
			while (true)
			{
				//var n = new Network (22, 50, 0.40f, 2);
				var n = new Network(22,50,true,3);
				n.PrintNetwork ();
				Console.ReadLine ();
			}
		}
	}
}
