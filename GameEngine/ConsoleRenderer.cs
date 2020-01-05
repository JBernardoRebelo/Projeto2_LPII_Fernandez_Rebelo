using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// Class that is responsible to render each frame of the game
    /// </summary>
    public class ConsoleRenderer
    {
        /// <summary>
        /// Bool to check if cursor was visible before the game started
        /// </summary>
        private bool cursorVisibleBefore = true;

        /// <summary>
        /// Array of Console Pixel which composes the frame that was rendered
        /// </summary>
        private ConsolePixel[,] _framePrev;

        /// <summary>
        /// Array of Console Pixel which composes the frame that is to be
        /// rendered
        /// </summary>
        private ConsolePixel[,] _frameNext;

        /// <summary>
        /// Struct to be used internally for managing renderable components
        /// </summary>
        private struct Renderable
        {
            /// <summary>
            /// Name of the game object
            /// </summary>
            public string Name { get; }
            /// <summary>
            /// Position of the game object sprite
            /// </summary>
            public Vector3 Pos { get; }
            /// <summary>
            /// Game object RenderableComponent
            /// </summary>
            public RenderableComponent Sprite { get; }

            /// <summary>
            /// Constructor to the struct
            /// </summary>
            /// <param name="name"> Game Object name </param>
            /// <param name="pos"> Game Object position </param>
            /// <param name="sprite"> Game Object sprite </param>
            public Renderable(
                string name, Vector3 pos, RenderableComponent sprite)
            {
                Name = name;
                Pos = pos;
                Sprite = sprite;
            }
        }

        /// <summary>
        /// Scene collum dimensions
        /// </summary>
        private int xdim;

        /// <summary>
        /// Scene row dimensions
        /// </summary>
        private int ydim;

        /// <summary>
        /// Default background pixel
        /// </summary>
        private ConsolePixel bgPix;

        /// <summary>
        /// Console Renderer constructor
        /// </summary>
        /// <param name="xdim"> Scene x dimension </param>
        /// <param name="ydim"> Scene y dimension </param>
        /// <param name="bgPix"> Background pixel </param>
        public ConsoleRenderer(int xdim, int ydim, ConsolePixel bgPix)
        {
            this.xdim = xdim;
            this.ydim = ydim;
            this.bgPix = bgPix;
            _framePrev = new ConsolePixel[xdim, ydim];
            _frameNext = new ConsolePixel[xdim, ydim];

            for (int y = 0; y < ydim; y++)
            {
                for (int x = 0; x < xdim; x++)
                {
                    _frameNext[x, y] = bgPix;
                }
            }
        }

        /// <summary>
        /// Pre-rendering setup method
        /// </summary>
        public void Start()
        {
            // Clean console
            Console.Clear();

            // Hide cursor
            Console.CursorVisible = false;

            // Resize window if we're in Windows (not supported on Linux/Mac)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //cursorVisibleBefore = Console.CursorVisible;
                Console.SetWindowSize(
                    Console.LargestWindowWidth, Console.LargestWindowHeight);
            }

            // Render the first frame
            RenderFrame();
        }

        /// <summary>
        /// Post-rendering teardown
        /// </summary>
        public void Finish()
        {
            Console.CursorVisible = cursorVisibleBefore;
        }

        /// <summary>
        /// Renders the actual frame
        /// </summary>
        private void RenderFrame()
        {
            // Background and foreground colors of each pixel
            ConsoleColor fgColor, bgColor;

            // Auxiliary frame variable for swaping buffers in the end
            ConsolePixel[,] frameAux;

            // Show frame in screen
            Console.SetCursorPosition(0, 0);
            fgColor = Console.ForegroundColor;
            bgColor = Console.BackgroundColor;
            for (int y = 0; y < ydim; y++)
            {
                for (int x = 0; x < xdim; x++)
                {
                    // Get current current and previous frame for this position
                    ConsolePixel pix = _frameNext[x, y];
                    ConsolePixel prevPix = _framePrev[x, y];

                    // Clear pixel at previous frame
                    _framePrev[x, y] = bgPix;

                    // If current pixel is not renderable, use background pixel
                    if (!pix.IsRenderable)
                    {
                        pix = bgPix;
                    }

                    // If current pixel is the same as previous pixel, don't
                    // draw it
                    if (pix.Equals(prevPix)) continue;

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

                    // Position cursor
                    Console.SetCursorPosition(x, y);

                    // Render pixel
                    Console.Write(pix.shape);
                }

                // New line
                Console.WriteLine();
            }

            // Setup frame buffers
            frameAux = _frameNext;
            _frameNext = _framePrev;
            _framePrev = frameAux;
        }

        /// <summary>
        /// Creates the next frame for rendering and then renders it
        /// </summary>
        /// <param name="gameObjects"> Collection of game objects
        /// present in the scene
        /// </param>
        public void Render(IEnumerable<GameObject> gameObjects)
        {
            // Filter game objects with sprite and position, get renderable
            // information and order by ascending Z
            IEnumerable<Renderable> objectsToRender = gameObjects
                .Where(gObj => gObj.IsRenderable)
                .Select(gObj => new Renderable(
                    gObj.Name,
                    gObj.GetComponent<Transform>().Pos,
                    gObj.GetComponent<RenderableComponent>()))
                .OrderBy(rend => rend.Pos.Z);

            // Render from lower layers to upper layers
            foreach (Renderable rend in objectsToRender)
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
                    {
                        throw new IndexOutOfRangeException(
                            $"Out of bounds pixel at ({x},{y}) in game object"
                            + $" '{rend.Name}' ");
                    }

                    // Put pixel in frame
                    _frameNext[x, y] = pixel.Value;
                }
            }

            // Render the frame
            RenderFrame();
        }
    }

}
