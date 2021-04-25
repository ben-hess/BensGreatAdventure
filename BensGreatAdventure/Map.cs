using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    public class Map
    {
        public int width { get; private set; }
        public int height { get; private set; }

        public int playerX { get; set; }
        public int playerY { get; set; }

        char[,] oldTiles;
        char[,] tiles;

        public Map(int width, int height, int playerX, int playerY)
        {
            this.width = width;
            this.height = height;
            this.playerX = playerX;
            this.playerY = playerY;
            tiles = new char[width, height];
            oldTiles = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[x, y] = ' ';
                    oldTiles[x, y] = ' ';
                }
            }
        }

        public char GetTile(int x, int y)
        {
            if (x < width && y < height && x >= 0 && y >= 0)
            {
                return tiles[x, y];
            }
            else
            {
                return (char)0;
            }
        }

        public char GetOldTile(int x, int y)
        {
            if (x < width && y < height && x >= 0 && y >= 0)
            {
                return oldTiles[x, y];
            }
            else
            {
                return (char)0;
            }
        }

        public void SetTile(int x, int y, char ch)
        {
            if (x < width && y < height && x >= 0 && y >= 0)
            {
                tiles[x, y] = ch;
            }
        }

        public bool TrySetTile(int x, int y, char ch)
        {
            if (GetTile(x, y) == ' ')
            {
                SetTile(x, y, ch);
                return true;
            }
            return false;
        }

        public void Update()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    oldTiles[x, y] = tiles[x, y];
                }
            }
        }
    }
}
