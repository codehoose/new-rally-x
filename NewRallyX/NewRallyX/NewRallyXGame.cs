using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NewRallyX.Components;
using NewRallyX.Framework.Sprites;
using NewRallyX.Models;
using NewRallyX.SpriteSheets;

namespace NewRallyX
{
    public class NewRallyXGame : Game
    {
        private const int GAME_WIDTH = 288 * 4;
        private const int GAME_HEIGHT = 224 * 4;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _blocks;
        private Texture2D _font;
        private Texture2D _spriteTex;
        private Texture2D _fuelBarTex;

        private SpriteSheet _smallBlocks;
        private SpriteSheet _largeBlocks;
        private SpriteSheet _fontSheet;
        private SpriteSheet _sprites;
        private ArcadeFont _arcadeFont;
        private SpriteSheet _fuelBar;

        private Sidebar _sidebar;

        internal GameState GameState { get; private set; } = new GameState();

        public NewRallyXGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            _graphics.PreferredBackBufferWidth = GAME_WIDTH;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _blocks = Content.Load<Texture2D>("blocks");
            _largeBlocks = new SpriteSheet(_blocks, 4, 4);
            _smallBlocks = new SpriteSheet(_blocks, 12, 12);

            _font = Content.Load<Texture2D>("arcade-font");
            _fontSheet = new SpriteSheet(_font, 22, 2);
            _arcadeFont = new ArcadeFont(_fontSheet);

            _spriteTex = Content.Load<Texture2D>("sprites");
            _sprites = new SpriteSheet(_spriteTex, 11, 1);

            _fuelBarTex = Content.Load<Texture2D>("fuel-bar");
            _fuelBar = new SpriteSheet(_fuelBarTex, 8, 1);

            _sidebar = new Sidebar(this, _spriteBatch, _sprites, _arcadeFont);
            Components.Add(_sidebar);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Render to target
            RenderTarget2D target = new RenderTarget2D(GraphicsDevice, 288, 224);
            GraphicsDevice.SetRenderTarget(target);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
            base.Draw(gameTime);
            _spriteBatch.End();

            // Expand target to full window
            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(target, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.End();
        }
    }
}