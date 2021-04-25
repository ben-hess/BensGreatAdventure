using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    public class Bomb : ITileController
    {
        public void OnUpdate(int x, int y, char ch, Scene scene, bool isInteraction)
        {
            if (isInteraction || (Math.Abs(x - scene.playerX) + Math.Abs(y - scene.playerY) == 1))
            {
                if(Math.Abs(x - scene.playerX) + Math.Abs(y - scene.playerY) <= 2)
                {
                    scene.playerX += Utils.Sign(scene.playerX - x);
                    scene.playerY += Utils.Sign(scene.playerY - y);
                    scene.hp -= 2;
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
