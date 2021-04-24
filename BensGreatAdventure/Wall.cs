using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    class Wall : ITileController
    {
        public string GetDisplayName()
        {
            return "Wall";
        }

        public void OnUpdate(int x, int y, char ch, Scene scene, bool isInteraction)
        {
            if (isInteraction)
            {
                scene.caption = "You can't walk through walls.";
            }
        }
    }
}
