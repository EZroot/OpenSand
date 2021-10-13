using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace OpenSandGame.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new OpenSand())
                game.Run();
        }
    }
}
