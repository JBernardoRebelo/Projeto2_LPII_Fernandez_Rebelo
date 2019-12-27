using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEngine
{
    public class RenderableStringComponent : RenderableComponent
    {

        // Since this is a renderable component, it must implement the Pixels
        // property
        public override
        IEnumerable<KeyValuePair<Vector2, ConsolePixel>> Pixels
        {
            get
            {
                // Get the string to render
                string strToRender = getStr();

                // Cycle through the string
                for (int i = 0; i < strToRender.Length; i++)
                {
                    // Get position for the current character
                    Vector2 pos = getPos(i);

                    // Create a console pixel for the current character
                    ConsolePixel pix =
                        new ConsolePixel(strToRender[i], fgColor, bgColor);

                    // Return the position and the pixel to be rendered
                    yield return new
                        KeyValuePair<Vector2, ConsolePixel>(pos, pix);
                }
            }
        }

        // Delegate which returns a string to be rendered
        private Func<string> getStr;

        // Delegate which returns a position for every character in the string
        private Func<int, Vector2> getPos;

        // The foreground and background colors of the string to be rendered
        private ConsoleColor fgColor, bgColor;

        // Create a new renderable string component
        public RenderableStringComponent(
            Func<string> getStr, Func<int, Vector2> getPos,
            ConsoleColor fgColor, ConsoleColor bgColor)
        {
            this.getStr = getStr;
            this.getPos = getPos;
            this.fgColor = fgColor;
            this.bgColor = bgColor;
        }
    }
}
