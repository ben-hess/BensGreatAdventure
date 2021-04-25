using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BensGreatAdventure.Tiles;

namespace BensGreatAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ben's Great Adventure";
            Console.Clear();

            Renderer renderer = new Renderer(Console.WindowWidth - 2, Console.WindowHeight);
            Map map = new Map(100, 50, 5, 5);
            Scene scene = new Scene(renderer, map);
            scene.controllers.Add('*', new Bomb());
            scene.controllers.Add('#', new Wall());
            scene.controllers.Add('X', new Effect());
            scene.controllers.Add('O', new Bolder());
            scene.controllers.Add('+', new HealthItem());

            scene.RenderMenu();
            Console.ReadKey(true);

            MovementDirection direction = MovementDirection.None;
            while (true)
            {
                scene.Update(direction);
                ConsoleKeyInfo info = Console.ReadKey(true);
                direction = MovementDirection.None;
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow: direction = MovementDirection.Up; break;
                    case ConsoleKey.DownArrow: direction = MovementDirection.Down; break;
                    case ConsoleKey.LeftArrow: direction = MovementDirection.Left; break;
                    case ConsoleKey.RightArrow: direction = MovementDirection.Right; break;
                }
            }
        }
    }
}