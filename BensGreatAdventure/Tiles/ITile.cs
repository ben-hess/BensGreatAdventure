using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure.Tiles
{
    public interface ITile
    {
        void OnUpdate(int x, int y, char ch, Scene scene, bool isInteraction);
        string GetDisplayName();
    }
}