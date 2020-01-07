using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// Class responsible for the graphical 
    /// representation of a game object in a scene
    /// </summary>
    public class ConsoleSprite : RenderableComponent
    {
        // Since a console sprite is a renderable component, it must implement
        // this property, which returns an ienumerable of position-pixel pairs
        // to render

        /// <summary>
        /// Overriden IEnumerable of KeyValuePair of Vector2(position) and
        /// ConsolePixel
        /// </summary>
        public override
        IEnumerable<KeyValuePair<Vector2, ConsolePixel>> Pixels => pixels;

        /// <summary>
        /// The position-pixel pairs are actually kept here
        /// </summary>
        private IDictionary<Vector2, ConsolePixel> pixels;

        // Below there are several constructors for this class

        /// <summary>
        /// Constructor used when a collection of pixels exist
        /// </summary>
        /// <param name="pixels"> IDictionary of pixels and their position
        /// </param>
        public ConsoleSprite(IDictionary<Vector2, ConsolePixel> pixels)
        {
            this.pixels = new Dictionary<Vector2, ConsolePixel>(pixels);
        }

        /// <summary>
        /// Constructor to be used when a collection of chars exist
        /// </summary>
        /// <param name="pixels"> Bi-dimensional array of chars </param>
        /// <param name="fgColor"> Desired foreground color </param>
        /// <param name="bgColor"> Desired background color </param>
        public ConsoleSprite(
            char[,] pixels, ConsoleColor fgColor, ConsoleColor bgColor)
        {
            this.pixels = new Dictionary<Vector2, ConsolePixel>();
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    char shape = pixels[x, y];
                    if (!shape.Equals(default))
                    {
                        this.pixels[new Vector2(x, y)] =
                            new ConsolePixel(shape, fgColor, bgColor);
                    }
                }
            }
        }
    }
}
