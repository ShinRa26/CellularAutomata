using System;

namespace CellularAutomata
{
	class Program
	{
		public static void Main (string[] args)
		{
			var n = new Network (20, 50, 0.50f);
			//n.GenerateNetwork ();
			n.PrintNetwork ();
		}
	}
}
