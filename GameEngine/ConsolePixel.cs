using System;

namespace GameEngine
{
    /// <summary>
    /// Class that is responsible for game object's pixel 
    /// that compose the sprite
    /// </summary>
    public struct ConsolePixel
    {
        /// <summary>
        /// Char to be used has a pixel for the object sprite
        /// </summary>
        public readonly char shape;

        /// <summary>
        /// Foreground color for the pixel in question
        /// </summary>
        public readonly ConsoleColor foregroundColor;

        /// <summary>
        /// Background color for the pixel in question
        /// </summary>
        public readonly ConsoleColor backgroundColor;

        /// <summary>
        /// Checks if  this pixel renderable
        /// </summary>
        public bool IsRenderable
        {
            get
            {
                // The pixel is renderable if any of its fields is not the
                // default to the specific type
                return !shape.Equals(default(char))
                    && !foregroundColor.Equals(default(ConsoleColor))
                    && !backgroundColor.Equals(default(ConsoleColor));
            }
        }

        // Below there are several constructors for building a console pixel

        /// <summary>
        /// Constructor to be used if pixel has foreground and background color
        /// </summary>
        /// <param name="shape"> Shape of the pixel </param>
        /// <param name="foregroundColor"> Desired foreground color </param>
        /// <param name="backgroundColor"> Desired background color </param>
        public ConsolePixel(char shape,
            ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this.shape = shape;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
        }

        /// <summary>
        /// Constructor to be used if pixel uses default Console colors
        /// </summary>
        /// <param name="shape"> The shape of the pixel </param>
        public ConsolePixel(char shape)
        {
            this.shape = shape;
            foregroundColor = Console.ForegroundColor;
            backgroundColor = Console.BackgroundColor;
        }

    }
}
