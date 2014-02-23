using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VVVVVV_Bird
{
    /// <summary>
    /// This contains the different type of alignment.
    /// </summary>
    enum align
    {
        left = 1,
        center = 2,
        right = 3,
    }

    /// <summary>
    /// This class draws texts using a sprite font.
    /// The sprite sheet from which the font is created must be in the same order as the alphabet string. IT can contain blank spaces instead of characters.
    /// </summary>
    class SpriteText
    {
        /// <summary>
        /// This method needs to be called every time text wants to be drawn using this method.
        /// </summary>
        /// <param name="text">The string of text that you want to display.</param>
        /// <param name="font">The spritesheet to be used as a font.</param>
        /// <param name="position">A Vector2 variable indicating the position. THe y Value will always be the on top.</param>
        /// <param name="size">The size of each character on the spritesheet. This needs to be consisdent throughout</param>
        /// <param name="scale">The sacle of the text to be displayed</param>
        /// <param name="alignment">Use left, center or right to indicate how the text is aligned relative to the X Position</param>
        /// <param name="spritebatch">The spritebatch used to draw the sprite on</param>
        /// <param name="color">THe color of the text.</param>
        public static void DrawSpriteText(string text, Texture2D font, Vector2 position, Vector2 size, int scale, align alignment, SpriteBatch spritebatch, Color color)
        {
            //Finds out the length of the string
            int textLength = text.Length;

            //This is used to find out where the letter is on thespritesheet.
            //The spritesheet needs to be layed out in the same order as this
            string alphabet = " ! #$%&'()*+,-. 0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[ ]^_'abcdefghijklmnopqrstuvwxyz{|}~ ";

            //Used to remeber the position of the letter
            int alphabetPosition = 0;

            //Finds out the number of letters in each row of the spritesheet
            int numberOfLetterPerRow = (int)(font.Width / size.X);

            //Loops through every letter and displays it on screen
            for (int i = 0; i < textLength; i++)
            {
                //Finds the position of the char on the string.
                alphabetPosition = alphabet.IndexOf(text[i]);

                //Finds out the xPos depending on how the text is aligned
                int xPos = 0;
                if (alignment == align.left)
                    xPos = (int)(position.X + (i * size.X * scale));
                else if (alignment == align.center)
                    xPos = (int)(position.X + ((i * size.X) - (size.X * textLength) / 2) * scale);
                else
                    xPos = (int)(position.X + ((i * size.X) - (size.X * textLength)) * scale);

                //Used to find out what part of the spritesheet is being drawn from
                Rectangle sourceRect = new Rectangle(
                    (int)((alphabetPosition % numberOfLetterPerRow) * size.X),
                    (int)(Math.Floor((double)(alphabetPosition / numberOfLetterPerRow)) * size.Y),
                    (int)size.X, (int)size.Y);

                //What part of the screen is being drawn on.
                Rectangle destinationRect = new Rectangle(xPos, (int)position.Y, (int)(size.X * scale), (int)(size.Y * scale));

                //Draws the sprite
                spritebatch.Draw(font, destinationRect, sourceRect, color, 0f, Vector2.Zero, SpriteEffects.None, 0.25f);
            }
        }
    }
}
