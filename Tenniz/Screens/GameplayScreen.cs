using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tenniz.Screens;

public class GameplayScreen : Screen
{
    public Player Player;

    public override void LoadContent(GraphicsDevice graphicsDevice)
    {
        Player = new Player();
        Player.LoadContent(graphicsDevice);
    }

    public override void Update(GameTime gameTime)
    {
        Player.Update(gameTime);
    }

    public override void Draw(SpriteBatch spritebatch)
    {
        Player.Draw(spritebatch);
    }
}
