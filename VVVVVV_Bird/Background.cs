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
        private Vector2 worldBounds;

        public Background(Texture2D backgroundToDraw, Vector2 worldBounds)
        {
            this.backgroundToDraw = backgroundToDraw;
            this.worldBounds = worldBounds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Draw(backgroundToDraw, new Vector2(i * backgroundToDraw.Width, 0), null, Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);

        }
    }
}