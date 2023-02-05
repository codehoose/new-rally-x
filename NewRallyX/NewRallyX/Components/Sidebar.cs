using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewRallyX.Framework;
using NewRallyX.Framework.Sprites;
using NewRallyX.SpriteSheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRallyX.Components
{
    internal class Sidebar : GameComponent<NewRallyXGame>
    {
        private const int LEFT = 224;
        private const int TOP = 0;
        private const int SIDEBAR_WIDTH = 64;
        private const int SCREEN_HEIGHT = 224;
        private const int RADAR_HEIGHT = 112;

        private readonly Texture2D _texture;
        private readonly Texture2D _fuelGauge;
        private readonly ArcadeFont _font;
        private readonly SpriteSheet _sprites;
        private readonly SpriteBatch _batch;

        public Sidebar(Game game, SpriteBatch spriteBatch, SpriteSheet sprites, ArcadeFont font)
            : base(game)
        {
            _texture = new Texture2D(game.GraphicsDevice, 1, 1);
            _fuelGauge = game.Content.Load<Texture2D>("fuel-indicator");
            _sprites = sprites;
            _font = font;
            _texture.SetData(new Color[] { Color.White });
            _batch = spriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            // Background
            Rectangle sidebarBackground = new Rectangle(LEFT, TOP, SIDEBAR_WIDTH, SCREEN_HEIGHT);
            _batch.Draw(_texture, sidebarBackground, Color.Black);

            // Static text
            Print("HI-SCORE", 0, gameTime, ArcadeFont.White);
            Print($"{Game.GameState.Player}UP", 16, gameTime, ArcadeFont.White);
            Print($"ROUND{Game.GameState.Round, 3}", 208, gameTime, ArcadeFont.White);

            // Fuel
            int width = Game.GameState.Fuel[Game.GameState.Player - 1];
            _batch.Draw(_fuelGauge, new Vector2(LEFT, 56), Color.White);
            Rectangle fuelRemaining = new Rectangle(LEFT + (64 - width), 65, width, 6);
            _batch.Draw(_texture, fuelRemaining, ArcadeFont.Yellow);

            // Dynamic text
            Print($"{Game.GameState.HiScore, 8}", 8, gameTime, ArcadeFont.Red);
            Print($"{Game.GameState.Scores[Game.GameState.Player - 1],8}", 24, gameTime, ArcadeFont.Cyan);

            // Lives
            for (int i = 0; i < Game.GameState.Lives[Game.GameState.Player - 1] - 1; i++)
            {
                _sprites.Draw(_batch, new Vector2(LEFT + i * 16, 192), 8, gameTime, Color.White);
            }

            // Radar
            Rectangle radarBackground = new Rectangle(LEFT, 80, SIDEBAR_WIDTH, RADAR_HEIGHT);
            _batch.Draw(_texture, radarBackground, ArcadeFont.RadarBlue);

            base.Draw(gameTime);
        }

        private void Print(string message, int offset, GameTime gameTime, Color colour)
        {
            Vector2 pos = new Vector2(LEFT, offset);
            _font.Draw(_batch, pos, message, gameTime, colour);
        }
    }
}
