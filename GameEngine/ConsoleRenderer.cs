using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Numerics;

namespace GameEngine
{
    public class ConsoleRenderer
    {
        // Was the cursor visible before game rendering started?
        // For now we assume it was
        private bool cursorVisibleBefore = true;

        // This struct is used internally for managing renderable components
        private struct Renderable
        {
            public string Name { get; }
            public Vector3 Pos { get; }
            public RenderableComponent Sprite { get; }

            public Renderable(
                string name, Vector3 pos, RenderableComponent sprite)
            {
                Name = name;
                Pos = pos;
                Sprite = sprite;
            }
        }

        // Scene dimensions
        private int xdim, ydim;

        // Default background pixel
        private ConsolePixel bgPix;

        // Constructor
        public ConsoleRenderer(int xdim, int ydim, ConsolePixel bgPix)
        {
            this.xdim = xdim;
            this.ydim = ydim;
            this.bgPix = bgPix;
        }

        // Pre-rendering setup
        public void Start()
        {
            // Clean console
            Console.Clear();

            // Hide cursor
            Console.CursorVisible = false;

            // Resize window if we're in Windows (not supported on Linux/Mac)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                cursorVisibleBefore = Console.CursorVisible;
                Console.SetWindowSize(xdim, ydim + 1);
            }

        }

        // Post-rendering teardown
        public void Finish()
        {
            Console.CursorVisible = cursorVisibleBefore;
        }

        // Render a frame
        public void Render(IEnumerable<GameObject> gameObjects)
        {
            // Background and foreground colors of each pixel
            ConsoleColor fgColor, bgColor;

            // The new frame to render
            ConsolePixel[,] frame = new ConsolePixel[xdim, ydim];

            // Filter game objects with sprite and position, get renderable
            // information and order ascending by Z
            IEnumerable<Renderable> stuffToRender = gameObjects
                .Where(gObj => gObj.IsRenderable)
                .Select(gObj => new Renderable(
                    gObj.Name,
                    gObj.GetComponent<Transform>().Pos,
                    gObj.GetComponent<RenderableComponent>()))
                .OrderBy(rend => rend.Pos.Z);

            // Render from lower layers to upper layers
            foreach (Renderable rend in stuffToRender)
            {
                // Cycle through all pixels in sprite
                foreach (KeyValuePair<Vector2, ConsolePixel> pixel
                    in rend.Sprite.Pixels)
                {
                    // Get absolute position of current pixel
                    int x = (int)(rend.Pos.X + pixel.Key.X);
                    int y = (int)(rend.Pos.Y + pixel.Key.Y);

                    // Throw exception if any of these is out of bounds
                    if (x < 0 || x >= xdim || y < 0 || y >= ydim)
                        throw new IndexOutOfRangeException(
                            $"Out of bounds pixel at ({x},{y}) in game object"
                            + $" '{rend.Name}'");

                    // Put pixel in frame
                    frame[x, y] = pixel.Value;
                }
            }

            // Show frame in screen
            Console.SetCursorPosition(0, 0);
            fgColor = Console.ForegroundColor;
            bgColor = Console.BackgroundColor;
            for (int y = 0; y < ydim; y++)
            {
                for (int x = 0; x < xdim; x++)
                {
                    // Get current pixel
                    ConsolePixel pix = frame[x, y];

                    // If current pixel is not renderable, use background pixel
                    if (!pix.IsRenderable)
                    {
                        pix = bgPix;
                    }

                    // Do we have to change the background and foreground
                    // colors for this pixel?
                    if (!pix.backgroundColor.Equals(bgColor))
                    {
                        bgColor = pix.backgroundColor;
                        Console.BackgroundColor = bgColor;
                    }
                    if (!pix.foregroundColor.Equals(fgColor))
                    {
                        fgColor = pix.foregroundColor;
                        Console.ForegroundColor = fgColor;
                    }

                    // Render pixel
                    Console.Write(pix.shape);
                }

                // New line
                Console.WriteLine();
            }
        }
    }

}
