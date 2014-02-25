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
        private List<Wall> upperwall, lowerwall;
        private List<Column> columns;
        private Texture2D wallSprite, columnSprite;
        int lowerBound, upperBound;
        Random rnd;

        public Level(Texture2D wallSprite,Texture2D columnSprite)
        {
            this.wallSprite = wallSprite;
            this.columnSprite = columnSprite;
            lowerwall = new List<Wall>();
            upperwall = new List<Wall>();
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
                lowerwall.Add(new Wall(wallSprite, new Vector2(i * wallSprite.Width,upperBound)));//creates lower bound walls
            }
            for (int i = 0; i < generateNumber; i++)
            {
                upperwall.Add(new Wall(wallSprite, new Vector2(i * wallSprite.Width, lowerBound)));//creates upper bound walls
            }
        }

        public void MoveWorldBounds(Vector2 position,Vector2 offset)
        {
            for (int i = 0; i < lowerwall.Count; i++)//walls
            {
                lowerwall[i].Position = new Vector2((32 * i), lowerwall[i].Position.Y) + Camera2D.CameraPosition - offset;
                lowerwall[i].UpdateWall();//update collision box for moving upper and lower bounds            
            }
            for (int i = 0; i < upperwall.Count; i++)//walls
            {
                upperwall[i].Position = new Vector2((32 * i), upperwall[i].Position.Y) + Camera2D.CameraPosition - offset;
                upperwall[i].UpdateWall();//update collision box for moving upper and lower bounds            
            }
        }

        //generates the columns to weave through, needs to know size of player to leave big enough gap
        public void GenerateColumns(int seperation, Vector2 playerDimensions, int generateNumber, int firstColumnStartPosition)
        {
            for (int columnXPosition = firstColumnStartPosition; columnXPosition < generateNumber * seperation + firstColumnStartPosition; columnXPosition += seperation)
            {
                columns.Add(new Column(columnSprite,playerDimensions,columnXPosition,lowerBound,upperBound,rnd));
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Column column in columns)//columns
            {
                column.Draw(spriteBatch);
            }
            for (int i = 0; i < upperwall.Count; i++)//walls
            {
                spriteBatch.Draw(wallSprite, upperwall[i].Position, null, Color.White, 0.0f, upperwall[i].Origin, 1.0f, SpriteEffects.None, 0.0f);//lower wall
            }
            for (int i = 0; i < lowerwall.Count; i++)//walls
            {
                spriteBatch.Draw(wallSprite, lowerwall[i].Position, null, Color.White, 0.0f, lowerwall[i].Origin, 1.0f, SpriteEffects.FlipVertically, 0.0f);//upper wall
            }
        }

        public List<Wall> LowerWalls { get { return this.lowerwall; } }
        public List<Wall> UpperWalls { get { return this.upperwall; } }
        public List<Column> Columns { get { return this.columns; } }
    }
}
