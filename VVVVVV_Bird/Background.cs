using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace VVVVVV_Bird
{
    public class Background
    {

        private Texture2D backgroundToDraw;
        private Vector2 worldBounds, position;
        private Vector3 color;

        public Background(Texture2D backgroundToDraw, Vector2 worldBounds, Vector2 position)
        {
            this.color = new Vector3(255, 255, 255);
            this.backgroundToDraw = backgroundToDraw;
            this.worldBounds = worldBounds;
            this.position = position;
        }

        public Vector2 Position { get { return position; }set { position = value; } }
        public Vector3 Color { get { return color; } set { color = value; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundToDraw, position, null, new Color(color.X,color.Y,1f), 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}