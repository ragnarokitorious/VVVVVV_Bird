using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    public class Wall : ICollidableObject
    {
        private Vector2 position;
        private Vector2 origin;
        private Rectangle collRect;
        private Texture2D wallSprite;
        private bool inView;

        public Wall(Texture2D wallSprite, Vector2 position)
        {
            this.position = position;
            this.origin = new Vector2(wallSprite.Width / 2, wallSprite.Height / 2);
            this.collRect = AddBoundingBox(position, origin, wallSprite.Width, wallSprite.Height);

            this.wallSprite = wallSprite;
        }

        public Rectangle AddBoundingBox(Vector2 position, Vector2 origin,int width, int height)
        {
            return new Rectangle((int)(position.X - origin.X),
                              (int)(position.Y - origin.Y),
                              width, height);
        }

        public void UpdateWall()
        {
            collRect.X = (int)(position.X - origin.X);
            collRect.Y = (int)(position.Y - origin.Y);
        }

        #region getset methods

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return origin;
            }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return collRect;
            }
            set
            {
                collRect = value;
            }
        }

        public bool InView
        {
            get
            {
                return inView;
            }
            set
            {
                inView = value;
            }
        }

        #endregion

    }
}
