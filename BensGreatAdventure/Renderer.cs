using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    public class Renderer
    {
        public int width { get; private set; }
        public int height { get; private set; }

        char[,] buffer;

        StringBuilder sb;

        public Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;
            buffer = new char[width, height];
            sb = new StringBuilder();
            Clear();
        }

        public void PutCh(int x, int y, char ch)
        {
            if(x < width && y < height && x >= 0 && y >= 0)
            {
                buffer[x, y] = ch;
            }
        }

        public void PutString(int x, int y, string str)
        {
            int _x = x;
            int _y = y;
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i] == '\n')
                {
                    _x = x;
                    _y++;
                }
                else
                {
                    PutCh(_x, _y, str[i]);
                    _x++;
                }
            }
        }

        public void PutCenterString(int y, string str)
        {
            int strWidth = str.Split('\n').Max(s => s.Length);
            PutString((width - strWidth) / 2, y, str);
        }

        public void Clear()
        {
            for(int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    buffer[x, y] = ' ';
                }
            }
        }

        public string GenerateString()
        {
            sb.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    sb.Append(buffer[x, y]);
                }
                if(y < height - 1)
                {
                    sb.Append('\n');
                }
            }
            return sb.ToString();
        }

        public void Render()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(GenerateString());
        }
    }
}