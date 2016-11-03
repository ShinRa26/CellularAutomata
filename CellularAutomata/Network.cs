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
		private int smoothFactor { get; set; }
		private bool useSeed { get; set; }

		//Default constructor. Creates a 10x10 empty grid.
		public Network ()
		{
			this.width = 10;
			this.height = 10;
			this.cave = new int[width, height];
		}

		//Constructor which uses a seed for the random generation of the network
		public Network(int width, int height, bool useSeed, int smooth)
		{
			this.width = width;
			this.height = height;
			this.useSeed = useSeed;
			this.wallChance = 0.0f;
			this.smoothFactor = smooth;
			this.cave = new int[width, height];

			SmoothNetwork ();
		}

		//Constructor for user-defined random chance in generating network
		public Network (int width, int height, float chance, int smooth)
		{
			this.width = width;
			this.height = height;
			this.wallChance = chance;
			this.cave = new int[width, height];
			this.smoothFactor = smooth;
			this.useSeed = false;

			SmoothNetwork ();
		}

		//Generates a random cave network, "1" for walls, "0" for space
		private void GenerateNetwork()
		{
			//If we're using seed for our random value
			if (useSeed) 
			{
				var r = new Random(Guid.NewGuid().GetHashCode());

				for(int x = 0; x < width; x++)
				{
					for(int y = 0; y < height; y++)
					{
						var noWall = (float)r.NextDouble();

						cave[x,y] = ((float)r.NextDouble() < noWall) ? 0:1;
					}
				}
			} 

			//User-defined chance
			else 
			{
				var r = new Random ();

				for (int x = 0; x < width; x++) 
				{
					for (int y = 0; y < height; y++) 
					{
						var noWall = (float)r.NextDouble ();

						if (wallChance < noWall) 
						{
							cave [x, y] = 0;
						} 
						else if (wallChance > noWall) 
						{
							cave [x, y] = 1;
						}
					}
				}
			}
		}

		//Smooths the network out by iterating over it several times
		private void SmoothNetwork()
		{
			GenerateNetwork ();

			for(int i = 0; i < smoothFactor; i++)
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
			else if(xCoord == 0 && yCoord != height -1)
			{
				for(int x = xCoord; x < xCoord + 2; x++)
				{
					for(int y = yCoord-1; y < yCoord + 2; y++)
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

			//Left Column
			else if(yCoord == 0 && xCoord != width-1)
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
			else if(xCoord == width - 1 && yCoord != height-1)
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
			else if(xCoord == width - 1 && yCoord == height)
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
			else if(yCoord == height -1 && (xCoord != 0 && xCoord != width -1))
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

		//Converts a cell to either a wall(0) or space(1)
		private void ConvertCell(int wCount, int nWCount, int x, int y)
		{
			if (wCount >= 4)
				cave [x, y] = 1;
			else if (nWCount >= 4)
				cave [x, y] = 0;
			else
			{
				var r = new Random ();
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
					if(cave[x,y] == 0) //WALLS - IM LAZY AND CAN'T BE ARSED CHANGING THE VALUES AROUND
					{
						Console.Write("#");
					}
					else if(cave[x,y] == 1) //SPACES - IM LAZY AND CAN'T BE ARSED CHANGING THE VALUES AROUND
					{
						Console.Write(".");
					}
				}
				Console.WriteLine ();
			}
		}
	}
}

