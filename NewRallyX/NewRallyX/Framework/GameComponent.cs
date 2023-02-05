using Microsoft.Xna.Framework;

namespace NewRallyX.Framework
{
    internal class GameComponent<T> : DrawableGameComponent where T : Game
    {
        protected new T Game { get; private set; }

        public GameComponent(Game game) : base(game)
        {
            Game = (T)game;
        }
    }
}
