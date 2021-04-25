using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure.Tiles
{
    public class Effect : ITile
    {
        public string GetDisplayName()
        {
            return "";
        }

        public void OnUpdate(int x, int y, char ch, Scene scene, bool isInteraction)
        {
            if(scene.map.GetTile(x, y) == ch)
            {
                scene.map.SetTile(x, y, ' ');
            }
        }
    }
}
