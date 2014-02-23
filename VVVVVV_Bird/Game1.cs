#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace VVVVVV_Bird
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager GraphicsDeviceManager;
        SpriteBatch spriteBatch;
        private CollisionResolver collisionResolver;
        private Vector2 cameraOffset;
        private Rectangle worldBounds;
        private Background background;
        private Player player;
        private Level level;
        private Texture2D fontTexture, spBoostSprite,wallSprite;
        private SpriteFont font;
        private int columnSeperation, firstColumnStartPosition, worldLowerBound, worldUpperBound;
        private UI playerScore;
        private UI playerHiScore;

        public Game1()
            : base()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            worldBounds = new Rectangle();
            worldBounds.Width = 1280;
            worldBounds.Height = 720;
            worldBounds.X = worldBounds.Width / 2;
            worldBounds.Y = worldBounds.Height / 2;

            GraphicsDeviceManager.PreferredBackBufferWidth = worldBounds.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = worldBounds.Height;

            cameraOffset = new Vector2(worldBounds.Width / 3, 0);

            GraphicsDeviceManager.IsFullScreen = false;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Input.Initialize();
            collisionResolver = new CollisionResolver();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = new Background(Content.Load<Texture2D>("BGTile.png"), new Vector2(worldBounds.Width, worldBounds.Height));

            Texture2D playerSprite = Content.Load<Texture2D>("PLAYER.png");
            spBoostSprite = Content.Load<Texture2D>("SPEEDBOOSTS.png");
            wallSprite = Content.Load<Texture2D>("WALL.png");
            fontTexture = Content.Load<Texture2D>("Font.png");
            font = Content.Load<SpriteFont>("HellaFont");

            worldLowerBound = 650;
            worldUpperBound = 50;

            columnSeperation = 500;
            firstColumnStartPosition = 1000;

            player = new Player(playerSprite, new Vector2(0, 360), new Vector2(1, 0.4f), new Vector2(10, 10), columnSeperation, 3);

            level = new Level(wallSprite);
            level.GenerateWorldBounds(worldLowerBound, worldUpperBound, 50);//generate the worlds lower and upper walls which the pipes are attached to
            level.GenerateColumns(columnSeperation, player.Dimensions, 3, firstColumnStartPosition);

            playerScore = new UI(font, new Vector2(30, 60), 0, "Score: ", true);
            playerHiScore = new UI(font, new Vector2(600, 60), 0, "Hi Score: ", true);

            collisionResolver = new CollisionResolver();

            collisionResolver.Add(player);

            foreach (Wall wall in level.Walls) { collisionResolver.Add(wall); }
            foreach (Column column in level.Columns) 
            {
                List<Wall> columnComponents = column.ColumnComponents;
                foreach (Wall wall in columnComponents) { collisionResolver.Add(wall); }
            } 
        }

        public void RestartGame(int pScore)
        {
            foreach (Wall wall in level.Walls) { collisionResolver.Remove(wall); }//remove walls and columns from resolver 
            foreach (Column column in level.Columns)
            {
                List<Wall> columnComponents = column.ColumnComponents;
                foreach (Wall wall in columnComponents) { collisionResolver.Remove(wall); }
            } 

            level = null;
            level = new Level(wallSprite);
            level.GenerateWorldBounds(worldLowerBound, worldUpperBound, 50);//generate the worlds lower and upper walls which the pipes are attached to
            level.GenerateColumns(columnSeperation, player.Dimensions, 3, firstColumnStartPosition);

            foreach (Wall wall in level.Walls) { collisionResolver.Add(wall); }
            foreach (Column column in level.Columns)
            {
                List<Wall> columnComponents = column.ColumnComponents;
                foreach (Wall wall in columnComponents) { collisionResolver.Add(wall); }
            } 

            player.IsDead = false;
            player.SpeedBoosts = 3;
            player.MaxXSpeed = 10;

            if (playerHiScore.GetNumber() < pScore)
                playerHiScore.SetNumber(pScore);


            player.Score = 0;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.Update(gameTime);
            Camera2D.UpdatePosition(new Vector2(player.Position.X, 0),cameraOffset);//move camera w/pos of player
            level.MoveWorldBounds(Camera2D.CameraPosition,cameraOffset);

            playerScore.SetPosition(new Vector2(30,0)+Camera2D.CameraPosition-cameraOffset);//move scores w/camera
            playerHiScore.SetPosition(new Vector2(1000, 0) + Camera2D.CameraPosition - cameraOffset);

            player.Update(gameTime, columnSeperation, firstColumnStartPosition);
            playerScore.SetNumber(player.Score);//update player score UI

            if (player.IsDead)
                RestartGame(player.Score);

            for (int i = 0; i < level.Columns.Count; i++)//check columns if they have moved off camera, regenerate in front of the other two
            {
                if (level.Columns[i].ColumnXPosition < Camera2D.CameraPosition.X-cameraOffset.X)
                {
                    foreach (Wall wall in level.Columns[i].ColumnComponents) { collisionResolver.Remove(wall); }//remove from resolver
                    level.Columns[i].ReGenerateColumn(level.Columns[i].ColumnXPosition + columnSeperation * 3,worldLowerBound,worldUpperBound);
                    foreach (Wall wall in level.Columns[i].ColumnComponents) { collisionResolver.Add(wall); }//readd after position change
                }
            }

            if ((player.Score % 5 == 0)&&(player.Score != 0))
            {
                player.MaxXSpeed += 0.08f;
                Console.WriteLine(player.MaxXSpeed);
            }

            collisionResolver.Resolve();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Texture,
            null, null, null, null, null,
            Camera2D.CameraTranslationMatrix);

            background.Draw(spriteBatch);
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);

            playerScore.Draw(spriteBatch);
            playerHiScore.Draw(spriteBatch);

            for (int i = 0; i < player.SpeedBoosts; i++)//draw speed boosts
            {
                spriteBatch.Draw(spBoostSprite, new Vector2(50 * (i + 1), 675) + Camera2D.CameraPosition - cameraOffset, null, Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
