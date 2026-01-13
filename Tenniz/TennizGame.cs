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

        Window.AllowUserResizing = true;
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

        SwitchScreen(new MenuScreen());


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
        LoadedScreen.Draw(_spriteBatch);

        base.Draw(gameTime);
    }

    public void SwitchScreen(Screen screen)
    {
        LoadedScreen = screen;
        LoadedScreen.Game = this;
        LoadedScreen.LoadContent(GraphicsDevice);
    }
}
