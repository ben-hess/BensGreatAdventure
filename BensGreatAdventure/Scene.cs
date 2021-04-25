using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    public class Scene
    {
        public PlayerDirection direction { get; private set; }

        public Renderer renderer { get; private set; }

        public int playerX { get; set; }
        public int playerY { get; set; }
        public int hp { get; set; }

        public Dictionary<char, ITileController> controllers { get; private set; }

        public string caption { get; set; }

        public Map map { get; private set; }

        int cameraX;
        int cameraY;

        public Scene(Renderer renderer)
        {
            this.renderer = renderer;

            playerX = 10;
            playerY = 10;
            hp = 10;

            controllers = new Dictionary<char, ITileController>();
            caption = "Use the arrow keys to move...";

            cameraX = 0;
            cameraY = 0;

            map = new Map(100, 50);
            map.Square(5, 5, 20, 10, '#');
            map.SetTile(20, 10, '*');
            map.SetTile(15, 11, 'O');
            map.SetTile(15, 6, '*');
            map.SetTile(30, 16, 'O');
            map.SetTile(35, 16, '+');
            map.SetTile(15, 25, '+');
        }

        public string GetTileDisplayName(int x, int y)
        {
            char tile = map.GetTile(x, y);
            if (controllers.ContainsKey(tile))
            {
                return controllers[tile].GetDisplayName();
            }
            return "";
        }

        public void UpdateTile(int x, int y, bool isInteraction)
        {
            if (x < map.width && y < map.height && x >= 0 && y >= 0)
            {
                char tile = map.GetOldTile(x, y);
                if (controllers.ContainsKey(tile))
                {
                    controllers[tile].OnUpdate(x, y, tile, this, isInteraction);
                }
            }
        }

        void AdjustCamera()
        {
            if(map.width > renderer.width)
            {
                cameraX = Utils.MinMax(playerX - renderer.width / 2, 0, map.width - renderer.width);
            }
            else
            {
                cameraX = 0;
            }

            if(map.height > renderer.height)
            {
                cameraY = Utils.MinMax(playerY - (renderer.height - 3) / 2, 0, map.height - (renderer.height - 3));
            }
            else
            {
                cameraY = 0;
            }
        }

        public void Update(PlayerDirection direction)
        {
            int nextX = playerX;
            int nextY = playerY;

            switch (direction)
            {
                case PlayerDirection.Up: nextY--; break;
                case PlayerDirection.Down: nextY++; break;
                case PlayerDirection.Right: nextX++; break;
                case PlayerDirection.Left: nextX--; break;
            }

            if(nextX < 0 || nextY < 0 || nextX >= map.width || nextY >= map.height)
            {
                nextX = playerX;
                nextY = playerY;
            }

            int oldX = playerX;
            int oldY = playerY;
            int oldHp = hp;

            for (int y = 0; y < map.height; y++)
            {
                for (int x = 0; x < map.width; x++)
                {
                    UpdateTile(x, y, x == nextX && y == nextY);
                }
            }

            if (playerX != oldX || playerY != oldY || map.GetTile(nextX, nextY) != ' ')
            {
                nextX = playerX;
                nextY = playerY;
            }

            if(map.GetTile(oldX, oldY) == '@')
            {
                map.SetTile(oldX, oldY, ' ');
            }
            map.SetTile(nextX, nextY, '@');

            playerX = nextX;
            playerY = nextY;

            AdjustCamera();

            renderer.Clear();
            renderer.PutString(0, 0, caption);
            
            renderer.PutString(0, 1, "HP[");
            for(int i = 0; i < hp; i++)
            {
                renderer.PutCh(3 + i, 1, '♥');
            }
            renderer.PutCh(13, 1, ']');

            if(hp != oldHp)
            {
                renderer.PutString(15, 1, (hp > oldHp ? "+" : "") + (hp - oldHp).ToString());
            }

            renderer.PutString(0, renderer.height - 1, "X: " + playerX + " Y: " + playerY);
            renderer.PutString(15, renderer.height - 1, "N: " + GetTileDisplayName(playerX, playerY - 1) +
                "  E: " + GetTileDisplayName(playerX + 1, playerY) +
                "  S: " + GetTileDisplayName(playerX, playerY + 1) +
                "  W: " + GetTileDisplayName(playerX - 1, playerY));

            for (int y = 0; y < renderer.height - 3; y++)
            {
                for (int x = 0; x < renderer.width; x++)
                {
                    renderer.PutCh(x, y + 2, map.GetTile(cameraX + x, cameraY + y));
                }
            }

            map.Update();

            renderer.Render();
            caption = "-";
        }

        public void RenderMenu()
        {
            renderer.Clear();
            renderer.PutCenterString(0, " ----- Ben's Great Adventure ----- ");
            renderer.PutCenterString(3, "How to play:");
            renderer.PutCenterString(4, "Use arrow keys to move.");
            renderer.PutCenterString(6, "Symbols:");

            List<string> symbolsList = new List<string>();
            symbolsList.Add("@ = Player");
            foreach(KeyValuePair<char, ITileController> controller in controllers)
            {
                string displayName = controller.Value.GetDisplayName();
                if (displayName.Length > 0)
                {
                    symbolsList.Add(controller.Key.ToString() + " = " + displayName);
                }
            }
            renderer.PutCenterString(7, string.Join("\n", symbolsList));

            renderer.PutCenterString(symbolsList.Count + 9, "[ Press any key to play... ]");
            renderer.Render();
        }
    }
}
