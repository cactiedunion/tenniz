using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tenniz;

public abstract class Screen
{
    public TennizGame Game;

    /// <param name="graphicsDevice">Used to load textures from file</param>
    public virtual void LoadContent(GraphicsDevice graphicsDevice) { }

    public virtual void Update(GameTime gameTime) { }

    public virtual void Draw(SpriteBatch spritebatch) { }
}
