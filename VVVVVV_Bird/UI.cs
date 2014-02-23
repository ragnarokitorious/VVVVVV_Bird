using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    public class UI
    {
        private String label;
        private int number;
        private SpriteFont font;
        private Vector2 position;
        private bool visible;

        public UI(SpriteFont font, Vector2 position, int number, string label, bool visible)
        {
            this.font = font;
            this.position = position;
            this.label = label;
            this.number = number;
            this.visible = visible;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
                spriteBatch.DrawString(font, label + number, position, Color.White,0f,new Vector2(),2f,SpriteEffects.None,0f);
        }

        public int GetNumber()
        {
            return this.number;
        }

        public void SetNumber(int number)
        {
            this.number = number;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setVisible(bool visible)
        {
            this.visible = visible;
        }
    }
}
