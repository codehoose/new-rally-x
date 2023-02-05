using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewRallyX.Framework.Sprites
{
    internal class SpriteSheet
    {
        private readonly Texture2D _texture;
        private readonly int _columns;
        private readonly int _rows;

        private readonly int _cellWidth;
        private readonly int _cellHeight;

        public SpriteSheet(Game game, string spriteSheetName, int columns, int rows)
        {
            _texture = game.Content.Load<Texture2D>(spriteSheetName);
            _columns = columns;
            _rows = rows;

            _cellWidth = _texture.Width / _columns;
            _cellHeight = _texture.Height / _rows;
        }

        public SpriteSheet(Texture2D texture, int columns, int rows)
        {
            _texture = texture;
            _columns = columns;
            _rows = rows;

            _cellWidth = _texture.Width / _columns;
            _cellHeight = _texture.Height / _rows;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int spriteID, GameTime gameTime, Color colour, float rotation = 0, SpriteEffects effects = SpriteEffects.None)
        {
            int row = spriteID / _columns;
            int column = spriteID % _columns;

            Rectangle rect = new Rectangle(column * _cellWidth, row * _cellHeight, _cellWidth, _cellHeight);
            spriteBatch.Draw(_texture, position, rect, colour, rotation, Vector2.Zero, 1, effects, 0);
        }
    }
}
