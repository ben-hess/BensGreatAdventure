using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BensGreatAdventure.Tiles;

namespace BensGreatAdventure
{
    public class Scene
    {
        public Renderer renderer { get; private set; }
        public Map map { get; set; }

        public int playerHP { get; set; }

        public Dictionary<char, ITile> controllers { get; private set; }

        public string caption { get; set; }

        int cameraX;
        int cameraY;

        public Scene(Renderer renderer, Map map)
        {
            this.renderer = renderer;
            this.map = map;

            playerHP = 10;

            controllers = new Dictionary<char, ITile>();
            caption = "Use the arrow keys to move...";

            cameraX = 0;
            cameraY = 0;
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
                cameraX = Utils.MinMax(map.playerX - renderer.width / 2, 0, map.width - renderer.width);
            }
            else
            {
                cameraX = 0;
            }

            if(map.height > renderer.height)
            {
                cameraY = Utils.MinMax(map.playerY - (renderer.height - 3) / 2, 0, map.height - (renderer.height - 3));
            }
            else
            {
                cameraY = 0;
            }
        }

        public void Update(MovementDirection direction)
        {
            int nextX = map.playerX;
            int nextY = map.playerY;

            switch (direction)
            {
                case MovementDirection.Up: nextY--; break;
                case MovementDirection.Down: nextY++; break;
                case MovementDirection.Right: nextX++; break;
                case MovementDirection.Left: nextX--; break;
            }

            if(nextX < 0 || nextY < 0 || nextX >= map.width || nextY >= map.height)
            {
                nextX = map.playerX;
                nextY = map.playerY;
            }

            int oldX = map.playerX;
            int oldY = map.playerY;
            int oldHp = playerHP;

            for (int y = 0; y < map.height; y++)
            {
                for (int x = 0; x < map.width; x++)
                {
                    UpdateTile(x, y, x == nextX && y == nextY);
                }
            }

            if (map.playerX != oldX || map.playerY != oldY || map.GetTile(nextX, nextY) != ' ')
            {
                nextX = map.playerX;
                nextY = map.playerY;
            }

            if(map.GetTile(oldX, oldY) == '@')
            {
                map.SetTile(oldX, oldY, ' ');
            }
            map.SetTile(nextX, nextY, '@');

            map.playerX = nextX;
            map.playerY = nextY;

            AdjustCamera();

            renderer.Clear();
            renderer.PutString(0, 0, caption);
            
            renderer.PutString(0, 1, "HP[");
            for(int i = 0; i < playerHP; i++)
            {
                renderer.PutCh(3 + i, 1, '♥');
            }
            renderer.PutCh(13, 1, ']');

            if(playerHP != oldHp)
            {
                renderer.PutString(15, 1, (playerHP > oldHp ? "+" : "") + (playerHP - oldHp).ToString());
            }

            renderer.PutString(0, renderer.height - 1, "X: " + map.playerX + " Y: " + map.playerY);
            renderer.PutString(15, renderer.height - 1, "N: " + GetTileDisplayName(map.playerX, map.playerY - 1) +
                "  E: " + GetTileDisplayName(map.playerX + 1, map.playerY) +
                "  S: " + GetTileDisplayName(map.playerX, map.playerY + 1) +
                "  W: " + GetTileDisplayName(map.playerX - 1, map.playerY));

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
            foreach(KeyValuePair<char, ITile> controller in controllers)
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
