using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    enum GameState
    {
        MENU, PLAYING
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameState gameState;

        Texture2D title;
        Vector2 titlePos;

        Texture2D playButton;
        Texture2D playButton1;
        Texture2D playButton2;
        Vector2 playButtonPos;
        Rectangle playButtonRect;

        Texture2D quitButton;
        Texture2D quitButton1;
        Texture2D quitButton2;
        Vector2 quitButtonPos;
        Rectangle quitButtonRect;

        Texture2D leftPaddle;
        Vector2 leftPaddlePos;
        float paddleSpeed = 5f;

        SoundEffect rollover;
        SoundEffect click;
        SoundEffectInstance rI;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 540;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            gameState = GameState.MENU;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.IsMouseVisible = true;
            

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            title = Content.Load<Texture2D>("title");
            titlePos = new Vector2(380, 50);

            playButton1 = Content.Load<Texture2D>("playButton");
            playButton2 = Content.Load<Texture2D>("playButton2");
            playButtonPos = new Vector2(355, 150);
            playButtonRect = new Rectangle(355, 150, 250, 50);

            quitButton1 = Content.Load<Texture2D>("quitButton1");
            quitButton2 = Content.Load<Texture2D>("quitButton2");
            quitButtonPos = new Vector2(355, 250);
            quitButtonRect = new Rectangle(355, 250, 250, 50);

            leftPaddle = Content.Load<Texture2D>("paddle");
            leftPaddlePos = new Vector2(0, 220);

            click = Content.Load<SoundEffect>("buttonclick");
            rollover = Content.Load<SoundEffect>("buttonrollover");
            rI = rollover.CreateInstance();
            rI.IsLooped = false;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            MouseState mouseState = Mouse.GetState();
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            KeyboardState ks = Keyboard.GetState();

            //Main Menu
            if (gameState == GameState.MENU)
            {
                //hovering over the play button
                if (playButtonRect.Contains(mousePosition))
                {
                    //rI.Play();
                    //rI.Stop();
                    playButton = playButton2;
                    //pressing the play button
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        //click.Play();
                        gameState = GameState.PLAYING;
                    }
                }
                else
                {
                    playButton = playButton1;
                }

                //hovering over the quit button
                if (quitButtonRect.Contains(mousePosition))
                {
                    //rollover.Play();
                    quitButton = quitButton2;
                    //pressing the quit button
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        //click.Play();
                        Exit();
                    }
                }
                else
                {
                    quitButton = quitButton1;
                }
            }

            //Playing
            if (gameState == GameState.PLAYING)
            {
                if (ks.IsKeyDown(Keys.W))
                {
                    if (leftPaddlePos.Y <= 0)
                    {
                        leftPaddlePos.Y -= 0;
                    }
                    else
                    {
                        leftPaddlePos.Y -= paddleSpeed;
                    }
                }

                if (ks.IsKeyDown(Keys.S))
                {
                    if (leftPaddlePos.Y + leftPaddle.Height >= 540)
                    {
                        leftPaddlePos.Y += 0;
                    }
                    else
                    {
                        leftPaddlePos.Y += paddleSpeed;
                    }
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 150, 245));

            if (gameState == GameState.MENU)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(title, titlePos, Color.White);
                spriteBatch.Draw(playButton, playButtonPos, Color.White);
                spriteBatch.Draw(quitButton, quitButtonPos, Color.White);
                spriteBatch.End();
            }

            if (gameState == GameState.PLAYING)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(leftPaddle, leftPaddlePos, Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
