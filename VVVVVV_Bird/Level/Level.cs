using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    class Level
    {
        private List<Wall> walls;
        private List<Column> columns;
        private Texture2D wallSprite;
        int lowerBound, upperBound;
        Random rnd;

        public Level(Texture2D wallSprite)
        {
            this.wallSprite = wallSprite;
            walls = new List<Wall>();
            columns = new List<Column>();
            rnd = new Random();
        }

        //generates the bounds for the level i.e top and bottom where pipes will come from
        public void GenerateWorldBounds(int lowerBound, int upperBound, int generateNumber)
        {
            this.lowerBound = lowerBound;//used also for creation of columns
            this.upperBound = upperBound;

            for (int i = 0; i < generateNumber; i++)
            {
                walls.Add(new Wall(wallSprite, new Vector2(i * wallSprite.Width,upperBound)));//creates lower bound walls
                walls.Add(new Wall(wallSprite, new Vector2(i * wallSprite.Width, lowerBound)));//creates upper bound walls
            }
        }

        public void MoveWorldBounds(Vector2 position,Vector2 offset)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Position = new Vector2(position.X + (i * wallSprite.Width / 2) - offset.X, walls[i].Position.Y);
                walls[i].UpdateWall();//update collision box for moving upper and lower bounds
            }
        }

        //generates the columns to weave through, needs to know size of player to leave big enough gap
        public void GenerateColumns(int seperation, Vector2 playerDimensions, int generateNumber, int firstColumnStartPosition)
        {
            for (int columnXPosition = firstColumnStartPosition; columnXPosition < generateNumber * seperation + firstColumnStartPosition; columnXPosition += seperation)
            {
                columns.Add(new Column(wallSprite,playerDimensions,columnXPosition,lowerBound,upperBound,rnd));
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Column column in columns)//columns
            {
                column.Draw(spriteBatch);
            }
            foreach (Wall wall in walls)//walls
            {
                spriteBatch.Draw(wallSprite, wall.Position, null, Color.White, 0.0f, wall.Origin, 1.0f, SpriteEffects.None, 0.0f);
            }

        }

        public List<Wall> Walls { get { return this.walls; } }
        public List<Column> Columns { get { return this.columns; } }
    }
}
