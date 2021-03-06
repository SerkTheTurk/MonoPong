﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

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

        Texture2D rightPaddle;
        Vector2 rightPaddlePos;

        SoundEffect rollover;
        SoundEffect click;
        SoundEffectInstance rI;

        SoundEffect music;
        SoundEffectInstance track;

        Texture2D ball;
        Vector2 ballPos;
        float ballSpeed = 5f;
        int ballDir;

        int leftScore = 0;
        int rightScore = 0;

        Random random = new Random();

        SpriteFont font;
        SpriteFont muse;

        bool soundplay = false;

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
            leftPaddlePos = new Vector2(15, 220);

            rightPaddle = Content.Load<Texture2D>("paddle");
            rightPaddlePos = new Vector2(920, 220);

            click = Content.Load<SoundEffect>("buttonclick");
            rollover = Content.Load<SoundEffect>("buttonrollover");
            rI = rollover.CreateInstance();
            rI.IsLooped = false;

            ball = Content.Load<Texture2D>("ball");
            ballPos = new Vector2(470, 260);

            font = Content.Load<SpriteFont>("Score");
            muse = Content.Load<SpriteFont>("musicguy");

            music = Content.Load<SoundEffect>("day65");
            track = music.CreateInstance();
            track.IsLooped = true;

            SoundEffect.MasterVolume = 0.3f;

            
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
                    playButton = playButton2;
                    //pressing the play button
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        click.Play();
                        gameState = GameState.PLAYING;
                        startGame();
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
                track.Play();
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

                if (ks.IsKeyDown(Keys.Up))
                {
                    if (rightPaddlePos.Y <= 0)
                    {
                        rightPaddlePos.Y -= 0;
                    }
                    else
                    {
                        rightPaddlePos.Y -= paddleSpeed;
                    }
                }

                if (ks.IsKeyDown(Keys.Down))
                {
                    if (rightPaddlePos.Y + rightPaddle.Height >= 540)
                    {
                        rightPaddlePos.Y += 0;
                    }
                    else
                    {
                        rightPaddlePos.Y += paddleSpeed;
                    }
                }

                //ball
                if (ballDir == 1)
                {
                    ballPos.X -= ballSpeed;
                    ballPos.Y -= ballSpeed;
                    if (ballPos.Y <= 0)
                    {
                        ballDir = 3;
                    }
                    if (ballPos.X <= leftPaddlePos.X + leftPaddle.Width)
                    {
                        if (ballPos.Y >= leftPaddlePos.Y && ballPos.Y + ball.Height <= leftPaddlePos.Y + leftPaddle.Height)
                        {
                            ballDir = 2;
                        }
                    }
                }
                if (ballDir == 2)
                {
                    ballPos.X += ballSpeed;
                    ballPos.Y -= ballSpeed;
                    if (ballPos.Y <= 0)
                    {
                        ballDir = 4;
                    }
                    if (ballPos.X + ball.Width >= rightPaddlePos.X)
                    {
                        if (ballPos.Y >= rightPaddlePos.Y && ballPos.Y + ball.Height <= rightPaddlePos.Y + rightPaddle.Height)
                        {
                            ballDir = 1;
                        }
                    }
                }
                if (ballDir == 3)
                {
                    ballPos.X -= ballSpeed;
                    ballPos.Y += ballSpeed;
                    if (ballPos.Y + ball.Height >= 540)
                    {
                        ballDir = 1;
                    }
                    if (ballPos.X <= leftPaddlePos.X + leftPaddle.Width)
                    {
                        if (ballPos.Y >= leftPaddlePos.Y && ballPos.Y + ball.Height <= leftPaddlePos.Y + leftPaddle.Height)
                        {
                            ballDir = 4;
                        }
                    }
                }
                if (ballDir == 4)
                {
                    ballPos.X += ballSpeed;
                    ballPos.Y += ballSpeed;
                    if (ballPos.Y + ball.Height >= 540)
                    {
                        ballDir = 2;
                    }
                    if (ballPos.X + ball.Width >= rightPaddlePos.X)
                    {
                        if (ballPos.Y >= rightPaddlePos.Y && ballPos.Y + ball.Height <= rightPaddlePos.Y + rightPaddle.Height)
                        {
                            ballDir = 3;
                        }
                    }
                }

                if (ballPos.X >= 960)
                {
                    leftScore += 1;
                    ballPos.X = 470;
                    ballPos.Y = 260;
                    ballDir = 1;
                }

                if (ballPos.X <= 0)
                {
                    rightScore += 1;
                    ballPos.X = 470;
                    ballPos.Y = 260;
                    ballDir = 2;
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
                spriteBatch.DrawString(muse, "Music by @Markymark655", new Vector2(10, 500), Color.Black);
                spriteBatch.End();
            }

            if (gameState == GameState.PLAYING)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(leftPaddle, leftPaddlePos, Color.White);
                spriteBatch.Draw(rightPaddle, rightPaddlePos, Color.White);
                spriteBatch.Draw(ball, ballPos, Color.White);
                spriteBatch.DrawString(font, leftScore.ToString(), new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(font, rightScore.ToString(), new Vector2(880, 10), Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void startGame()
        {
            int startDir = random.Next(2) + 1;
            if (startDir == 1)
            {
                ballDir = 1;
            }
            else if (startDir == 2)
            {
                ballDir = 2;
            }
        }

    }
}
