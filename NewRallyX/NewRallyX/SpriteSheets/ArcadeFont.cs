using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewRallyX.Framework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRallyX.SpriteSheets
{
    internal class ArcadeFont
    {
        public static Color White = new Color(216, 216, 216, 255);
        public static Color Red = new Color(216, 0, 0, 255);
        public static Color Yellow = new Color(248, 248, 0, 255);
        public static Color Cyan = new Color(32, 216, 216, 255);
        public static Color Black = new Color(0, 0, 0, 255);
        public static Color RadarBlue = new Color(41, 85, 164, 255);

        private readonly SpriteSheet _spriteSheet;

        public ArcadeFont(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, string message, GameTime gameTime, Color colour)
        {
            int[] indices = GetIndexes(message);

            for (int i = 0; i < indices.Length; i++)
            {
                Vector2 pos = position + new Vector2(i * 8, 0);
                _spriteSheet.Draw(spriteBatch, pos, indices[i], gameTime, colour);
            }
        }

        private int[] GetIndexes(string message)
        {
            string msg = message.ToUpper();
            int[] result = new int[msg.Length];

            for (int i = 0; i < msg.Length; i++)
            {
                char ch = msg[i];
                if (ch >= 'A' && ch <= 'Z')
                {
                    result[i] = ch - 'A';
                }
                else if (ch >= '0' && ch <= '9')
                {
                    result[i] = 26 + ch - '0';
                }
                else if (ch == '!')
                {
                    result[i] = 36;
                }
                else if (ch == '"')
                {
                    result[i] = 37;
                }
                else if (ch == '.')
                {
                    result[i] = 38;
                }
                else if (ch == '-')
                {
                    result[i] = 39;
                }
                else
                {
                    result[i] = 40;
                }
            }

            return result;
        }
    }
}
