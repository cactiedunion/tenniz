using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tenniz.Screens;

public class MenuScreen : Screen
{
    public override void LoadContent(GraphicsDevice graphicsDevice)
    {
        
    }
    
    public override void Update(GameTime gameTime)
    {
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.Space))
        {
            Game.SwitchScreen(new GameplayScreen());
        }
    }
    
    public override void Draw(SpriteBatch spritebatch)
    {
        
    }
}
