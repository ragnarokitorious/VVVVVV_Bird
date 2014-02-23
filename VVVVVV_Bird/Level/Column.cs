using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    public class Column
    {
        private List<Wall> columnComponents;
        Texture2D wallSprite;
        Vector2 position;
        Random rnd;
        private Vector2 playerDimensions;
        
        public Column(Texture2D wallSprite,Vector2 playerDimensions,int XPosition,int lowerBound,int upperBound,Random rnd)
        {
            this.wallSprite = wallSprite;
            this.columnComponents = new List<Wall>();
            this.playerDimensions = playerDimensions;
            this.position.X = XPosition;
            this.rnd = rnd;
            GenerateColumn(lowerBound, upperBound);//these 2 vars are to define bounds to create columns in
        }

        private void GenerateColumn(int lowerBound, int upperBound)
        {
            int playerWallSizeRatio = (int)Math.Round(playerDimensions.Y) / wallSprite.Height;//size of player relative to wall blocks
            int randomMax = ((lowerBound - upperBound) / wallSprite.Height) - playerWallSizeRatio * 2;//specify max height of 1 column section


                int randomLowerColumn = rnd.Next(1, randomMax);//specify number of lower column blocks to generate
                int randomUpperColumn = randomMax - randomLowerColumn;//generates uppercolumn from knowing size of lower column 

                for (int j = 0; j < randomLowerColumn; j++)
                {
                    columnComponents.Add(new Wall(wallSprite, new Vector2(position.X, lowerBound - (wallSprite.Height * j))));//column section from bottom up
                }

                for (int k = 0; k < randomUpperColumn; k++)
                {
                    columnComponents.Add(new Wall(wallSprite, new Vector2(position.X, upperBound + (wallSprite.Height * k))));//column section from top down
                }

        }

        public void ReGenerateColumn(float xPosition,int lowerBound, int upperBound)
        {
            ColumnXPosition = xPosition;

            int playerWallSizeRatio = (int)Math.Round(playerDimensions.Y) / wallSprite.Height;//size of player relative to wall blocks
            int randomMax = ((lowerBound - upperBound) / wallSprite.Height) - playerWallSizeRatio * 2;//specify max height of 1 column section


            int randomLowerColumn = rnd.Next(1, randomMax);//specify number of lower column blocks to generate
            int randomUpperColumn = randomMax - randomLowerColumn;//generates uppercolumn from knowing size of lower column 
            columnComponents=new List<Wall>();

            for (int i = 0; i < randomLowerColumn; i++)
            {
                columnComponents.Add(new Wall(wallSprite, new Vector2(ColumnXPosition, lowerBound - (wallSprite.Height * i))));//column section from bottom up
            }
            for (int j = 0; j < randomUpperColumn; j++)
            {
                columnComponents.Add(new Wall(wallSprite, new Vector2(ColumnXPosition, upperBound + (wallSprite.Height * j))));//column section from top down
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Wall columnComponent in columnComponents)
            {
                spriteBatch.Draw(wallSprite, columnComponent.Position, null, Color.White, 0.0f, columnComponent.Origin, 1.0f, SpriteEffects.None, 0.0f);
            }
        }

        public List<Wall> ColumnComponents
        {
            get
            {
                return columnComponents;
            }
        }

        public float ColumnXPosition
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }
        
    }
}
