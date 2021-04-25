﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    public class Bolder : ITileController
    {
        public string GetDisplayName()
        {
            return "Bolder";
        }

        public void OnUpdate(int x, int y, char ch, Scene scene, bool isInteraction)
        {
            if (isInteraction)
            {
                int nextX = x + Utils.Sign(x - scene.playerX);
                int nextY = y + Utils.Sign(y - scene.playerY);

                scene.caption = "You pushed the bolder.";
                scene.UpdateTile(nextX, nextY, true);

                if(scene.map.GetTile(nextX, nextY) == ' ')
                {
                    scene.map.SetTile(x, y, ' ');
                    scene.map.SetTile(nextX, nextY, ch);
                }
                else
                {
                    scene.caption = "You can't push the bolder that way.";
                }
            }
        }
    }
}
