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

        char[,] oldTiles;
        char[,] tiles;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
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

        public void FillTiles(int x, int y, int w, int h, char ch)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    SetTile(x + j, y + i, ch);
                }
            }
        }

        public void Square(int x, int y, int w, int h, char ch)
        {
            FillTiles(x, y, w, 1, ch);
            FillTiles(x, y + h - 1, w, 1, ch);
            FillTiles(x, y + 1, 1, h - 2, ch);
            FillTiles(x + w - 1, y + 1, 1, h - 2, ch);
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
