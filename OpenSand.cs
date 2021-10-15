using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace OpenSandGame.Core
{
    public class OpenSand : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private bool _dirty = false;

        CoreEngine coreEngine;
        Camera camera;
        public OpenSand()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set the window title.
            this.Window.Title = "Pixel Plotter";

            // Create out Simulation
            coreEngine = new CoreEngine(200,200,1,graphics: _graphics);
            // Set the window size.
            _graphics.PreferredBackBufferWidth = coreEngine.Width * coreEngine.Scale;
            _graphics.PreferredBackBufferHeight = coreEngine.Height * coreEngine.Scale;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            camera = new Camera(_graphics.GraphicsDevice.Viewport);
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            coreEngine.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            coreEngine.UpdateSimulation(gameTime, camera, _graphics);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.GetTransform(_graphics.GraphicsDevice));
            this.Render();
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void Render()
        {
            coreEngine.Render(_spriteBatch);
        }
    }
}