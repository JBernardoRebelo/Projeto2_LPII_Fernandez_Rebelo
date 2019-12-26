using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    public class ConsoleSprite : RenderableComponent
    {
        // Since a console sprite is a renderable component, it must implement
        // this property, which returns an ienumerable of position-pixel pairs
        // to render
        public override
        IEnumerable<KeyValuePair<Vector2, ConsolePixel>> Pixels => pixels;

        // The position-pixel pairs are actually kept here
        private IDictionary<Vector2, ConsolePixel> pixels;

        // Below there are several constructors for this class

        public ConsoleSprite(IDictionary<Vector2, ConsolePixel> pixels)
        {
            this.pixels = new Dictionary<Vector2, ConsolePixel>(pixels);
        }

        public ConsoleSprite(ConsolePixel[,] pixels)
        {
            this.pixels = new Dictionary<Vector2, ConsolePixel>();
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    ConsolePixel cpixel = pixels[x, y];
                    if (cpixel.IsRenderable)
                    {
                        this.pixels[new Vector2(x, y)] = cpixel;
                    }
                }
            }
        }

        public ConsoleSprite(char[,] pixels)
        {
            this.pixels = new Dictionary<Vector2, ConsolePixel>();
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    char shape = pixels[x, y];
                    if (!shape.Equals(default(char)))
                    {
                        this.pixels[new Vector2(x, y)] =
                            new ConsolePixel(shape);
                    }
                }
            }
        }

        public ConsoleSprite(
            char[,] pixels, ConsoleColor fgColor, ConsoleColor bgColor)
        {
            this.pixels = new Dictionary<Vector2, ConsolePixel>();
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    char shape = pixels[x, y];
                    if (!shape.Equals(default(char)))
                    {
                        this.pixels[new Vector2(x, y)] =
                            new ConsolePixel(shape, fgColor, bgColor);
                    }
                }
            }
        }
    }
}
