using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ben's Great Adventure";
            Console.Clear();

            Renderer renderer = new Renderer(Console.WindowWidth - 2, Console.WindowHeight);
            Scene scene = new Scene(renderer);
            scene.controllers.Add('*', new Knockback());
            scene.controllers.Add('#', new Wall());
            scene.controllers.Add('X', new Effect());
            scene.controllers.Add('O', new Bolder());
            scene.controllers.Add('+', new HealthItem());

            scene.RenderMenu();
            Console.ReadKey(true);

            PlayerDirection direction = PlayerDirection.None;
            while (true)
            {
                scene.Update(direction);
                ConsoleKeyInfo info = Console.ReadKey(true);
                direction = PlayerDirection.None;
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow: direction = PlayerDirection.Up; break;
                    case ConsoleKey.DownArrow: direction = PlayerDirection.Down; break;
                    case ConsoleKey.LeftArrow: direction = PlayerDirection.Left; break;
                    case ConsoleKey.RightArrow: direction = PlayerDirection.Right; break;
                }
            }
        }
    }
}