using System;

namespace CellularAutomata
{
	class Program
	{
		public static void Main (string[] args)
		{
			var n = new Network (20, 100, 0.42f, 2);
			n.PrintNetwork ();
		}
	}
}
