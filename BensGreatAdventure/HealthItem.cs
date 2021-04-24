using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    public class HealthItem : ITileController
    {
        public string GetDisplayName()
        {
            return "Health";
        }

        public void OnUpdate(int x, int y, char ch, Scene scene, bool isInteraction)
        {
            if (isInteraction)
            {
                scene.hp++;
                scene.map.SetTile(x, y, ' ');
                scene.caption = "You used a health item.";
            }
        }
    }
}
