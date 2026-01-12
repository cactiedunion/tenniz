using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tenniz.Screens;

namespace Tenniz;

public class TennizGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Screen LoadedScreen;

    public TennizGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        InputHelper.Setup(this);

        LoadedScreen = new GameplayScreen();
        LoadedScreen.LoadContent(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        InputHelper.UpdateSetup();

        LoadedScreen.Update(gameTime);

        InputHelper.UpdateCleanup();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(4, 4, 1));

        LoadedScreen.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
