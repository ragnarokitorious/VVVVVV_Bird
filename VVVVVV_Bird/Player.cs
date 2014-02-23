using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    public class Player : ICollidableObject
    {
        private Vector2 startPosition;
        private Vector2 position, origin, velocity;
        Vector2 speed, maxSpeed;
        private Texture2D playerSprite;
        private Rectangle collRect;
        private Keys lastPressedKey;
        private int playerScore;
        private bool isDead;
        private int columnSeperation, speedBoosts;

        public Player(Texture2D playerSprite, Vector2 position, Vector2 speed, Vector2 maxSpeed, int columnSeperation, int speedBoosts)
        {
            this.startPosition = position;

            this.playerSprite = playerSprite;
            this.position=position;
            this.speed = speed;
            this.maxSpeed = maxSpeed;
            this.origin = new Vector2(playerSprite.Width / 2, playerSprite.Height / 2);
            this.collRect = AddBoundingBox(position, origin, playerSprite.Width, playerSprite.Height);
            this.speedBoosts = speedBoosts;

            this.columnSeperation = columnSeperation;
        }

        public Rectangle AddBoundingBox(Vector2 position, Vector2 origin,int width, int height)
        {
            return new Rectangle((int)(position.X - origin.X),
                                          (int)(position.Y - origin.Y),
                                          width, height);
        }

        public void Update(GameTime gameTime,int columnSeperation,int firstColumnStartPosition)
        {
            this.velocity *= 0.95f;

            //always move to right, until change of level direction
            velocity.X += speed.X/1f;

            if (Input.IsKeyPressed(Keys.Down))
                lastPressedKey = Keys.Down;
            if (Input.IsKeyPressed(Keys.Up))
                lastPressedKey = Keys.Up;

            if (lastPressedKey == Keys.Down)
                velocity.Y += speed.Y;
            if (lastPressedKey == Keys.Up)
                velocity.Y += -speed.Y;

            if (speedBoosts > 0)
            {
                if ((lastPressedKey == Keys.Down) && (Input.IsKeyPressed(Keys.Space)))
                {
                    position.Y += 75;
                    speedBoosts--;
                }
                if ((lastPressedKey == Keys.Up) && (Input.IsKeyPressed(Keys.Space)))
                {
                    position.Y += -75;
                    speedBoosts--;
                }
            }

            if (position.X >= firstColumnStartPosition - 20)
                playerScore = ((int)position.X / columnSeperation) - (firstColumnStartPosition / columnSeperation - 1);
            
            MovePlayer(this.velocity, gameTime);
        }

        public void MovePlayer(Vector2 velocity, GameTime gameTime)
        {
            velocity.X = Math.Min(Math.Abs(velocity.X), maxSpeed.X) * Math.Sign(velocity.X);
            velocity.Y = Math.Min(Math.Abs(velocity.Y), maxSpeed.Y) * Math.Sign(velocity.Y);
            position += (velocity * (float)(gameTime.ElapsedGameTime.Milliseconds / 10));
            collRect.X = (int)(position.X - origin.X);
            collRect.Y = (int)(position.Y - origin.Y);
        }

        public void KillPlayer()
        {
            IsDead = true;
            ResetPosition();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerSprite, position, null, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
        }


        #region getset methods

        public Rectangle CollisionRectangle
        {
            get
            {
                return collRect;
            }
        }

        public void ResetPosition() { this.position = this.startPosition; this.lastPressedKey = Keys.V; }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector2 Dimensions { get { return new Vector2(this.playerSprite.Width, this.playerSprite.Height); } }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        public int Score
        {
            get
            {
                return playerScore;
            }
            set
            {
                playerScore = value;
            }
        }

        public bool IsDead
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
            }
        }


        public int SpeedBoosts { get { return speedBoosts; ;} set { speedBoosts = value; ;} }

        #endregion

    }
}
