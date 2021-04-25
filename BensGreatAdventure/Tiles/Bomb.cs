using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure.Tiles
{
    public class Bomb : ITile
    {
        public void OnUpdate(int x, int y, char ch, Scene scene, bool isInteraction)
        {
            if (isInteraction || (Math.Abs(x - scene.map.playerX) + Math.Abs(y - scene.map.playerY) == 1))
            {
                if(Math.Abs(x - scene.map.playerX) + Math.Abs(y - scene.map.playerY) <= 2)
                {
                    scene.map.playerX += Utils.Sign(scene.map.playerX - x);
                    scene.map.playerY += Utils.Sign(scene.map.playerY - y);
                    scene.playerHP -= 2;
                }
                scene.map.SetTile(x, y, 'X');
                scene.map.SetTile(x + 1, y, 'X');
                scene.map.SetTile(x - 1, y, 'X');
                scene.map.SetTile(x, y + 1, 'X');
                scene.map.SetTile(x, y - 1, 'X');
                scene.caption = "Boom!";
            }
        }

        public string GetDisplayName()
        {
            return "Bomb";
        }
    }
}
