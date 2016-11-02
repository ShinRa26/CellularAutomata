using System;
using System.Collections.Generic;


namespace CellularAutomata
{
	public class Network
	{
		private int[,] cave {get; set;}
		private int width { get; set;}
		private int height { get; set; }
		private float wallChance { get; set;}

		//Default constructor. Creates a 10x10 empty grid.
		public Network ()
		{
			this.width = 10;
			this.height = 10;
			this.cave = new int[width, height];
		}

		//Main Constructor
		public Network (int width, int height, float chance)
		{
			this.width = width;
			this.height = height;
			this.wallChance = chance;
			this.cave = new int[width, height];

			SmoothNetwork ();
		}

		//Generates a random cave network, "#" for walls, " " for space
		private void GenerateNetwork()
		{
			Random r = new Random ();

			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					float noWall = (float)r.NextDouble ();

					if(wallChance < noWall)
					{
						cave [x, y] = 0;
					}
					else if(wallChance > noWall)
					{
						cave [x, y] = 1;
					}
				}
			}
		}

		private void SmoothNetwork()
		{
			GenerateNetwork ();

			for(int i = 0; i < 5; i++)
				ProcessNetwork();
		}

		//Processes the network
		private void ProcessNetwork()
		{
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					ProcessCell (x, y);
				}
			}
		}

		//Gets a cell's neighbours and converts appropriately
		private void ProcessCell(int xCoord, int yCoord)
		{
			if (!IsBoundaryCell (xCoord, yCoord))
				ProcessMiddleCell (xCoord, yCoord);
			else
				ProcessBoundaryCell (xCoord, yCoord);
		}

		//Determines if the given cell is @ the boundary of the network
		private bool IsBoundaryCell(int x, int y)
		{
			return ((x == 0) || (y == 0) || (x == 0 && y == 0) || (x == width - 1 && y == 0) || (x == width - 1) || (y == height-1 || x == 0) || (y == height - 1) || (x == width - 1 && y == height - 1));
		}

		//Processes cells in the middle of the network
		private void ProcessMiddleCell(int xCoord, int yCoord)
		{
			int wallCount = 0;
			int noWallCount = 0;

			for(int x = xCoord - 1; x < xCoord + 2; x++)
			{
				for(int y = yCoord - 1; y < yCoord + 2; y++)
				{
					if(x == xCoord && y == yCoord)
						continue;
					else
					{
						if (cave [x, y] == 0)
							noWallCount++;
						else if (cave [x, y] == 1)
							wallCount++;
					}

				}
			}

			ConvertCell (wallCount, noWallCount, xCoord, yCoord);
		}

		//Processes cells that are @ the boundary of the network
		private void ProcessBoundaryCell(int xCoord, int yCoord)
		{
			int wallCount = 0;
			int noWallCount = 0;

			//Top Left Corner
			if(xCoord == 0 && yCoord == 0)
			{
				for(int x = xCoord; x < xCoord + 2; x++)
				{
					for(int y = yCoord; y < yCoord + 2; y++)
					{
						if (x == xCoord && y == yCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			//Top Row
			else if(xCoord == 0)
			{
				for(int x = xCoord; x < xCoord + 2; x++)
				{
					for(int y = yCoord-1; y < yCoord + 2; y++)
					{
						if (xCoord == 0 && y == yCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			//Left Column
			else if(yCoord == 0)
			{
				for(int x = xCoord-1; x < xCoord + 2; x++)
				{
					for(int y = yCoord; y < yCoord + 2; y++)
					{
						if (y == yCoord && x == xCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			//Bottom Left
			else if (xCoord == width - 1 && yCoord == 0)
			{
				for(int x = xCoord - 1; x < xCoord + 1; x++)
				{
					for(int y = yCoord; y < yCoord + 2; y++)
					{
						if (x == xCoord && y == yCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			//Bottom Row
			else if(xCoord == width - 1)
			{
				for(int x = xCoord - 1; x < xCoord + 1; x++)
				{
					for(int y = yCoord - 1; y < yCoord + 2; y++)
					{
						if (x == xCoord && y == yCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			//Bottom Right
			else if(xCoord == width - 1 && yCoord == height-1)
			{
				for(int x = xCoord - 1; x < xCoord + 1; x++)
				{
					for(int y = yCoord - 1; y < yCoord + 1; y++)
					{
						if (x == xCoord && y == yCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			//Right column
			else if(yCoord == height -1)
			{
				for(int x = xCoord - 1; x < xCoord + 2; x++)
				{
					for(int y = yCoord - 1; y < yCoord + 1; y++)
					{
						if (x == xCoord && y == yCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			//Top right
			else if(xCoord == 0 && yCoord == height-1)
			{
				for(int x = xCoord; x < xCoord + 2; x++)
				{
					for(int y = yCoord - 1; y < yCoord + 1; y++)
					{
						if(x == xCoord && y == yCoord)
							continue;
						else
						{
							if (cave [x, y] == 0)
								noWallCount++;
							else if (cave [x, y] == 1)
								wallCount++;
						}
					}
				}
			}

			ConvertCell (wallCount, noWallCount, xCoord, yCoord);
		}

		//Converts a cell to either a wall(1) or space(0)
		private void ConvertCell(int wCount, int nWCount, int x, int y)
		{
			if (wCount >= 4)
				cave [x, y] = 1;
			else if (nWCount >= 4)
				cave [x, y] = 0;
			else
			{
				Random r = new Random ();
				int chance = r.Next (0, 1);

				if (chance == 0)
					cave [x, y] = 0;
				else
					cave [x, y] = 1;
			}
		}

		//Prints out the network
		public void PrintNetwork()
		{
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					if(cave[x,y] == 0)
					{
						Console.Write(" ");
					}
					else if(cave[x,y] == 1)
					{
						Console.Write("#");
					}
				}
				Console.WriteLine ();
			}
		}
	}
}

