using System;
using Apos.Gui;
using Apos.Input;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tenniz.Screens;

public class MenuScreen : Screen
{
    const string FontPath = "Assets/rimouski sb.ttf";

    IMGUI ui;

    public override void LoadContent(GraphicsDevice graphicsDevice)
    {
        FontSystem fontSystem = new FontSystem();
        fontSystem.AddFont(TitleContainer.OpenStream(FontPath));
        
        GuiHelper.Setup(Game, fontSystem);

        ui = new IMGUI();
        GuiHelper.CurrentIMGUI = ui;
    }
    
    public override void Update(GameTime gameTime)
    {
        GuiHelper.Scale = 1 * Math.Min(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height) / 400;

        ui.UpdateStart(gameTime);
        
        MenuPanel.Push();
        if (Button.Put("Play").Clicked) {
            Game.SwitchScreen(new GameplayScreen());
        }

        if (Button.Put("Quit").Clicked) {
            Game.Exit();
        }
        MenuPanel.Pop();

        ui.UpdateEnd(gameTime);
    }
    
    public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
    {
        spritebatch.GraphicsDevice.Clear(Color.Black);
        ui.Draw(gameTime);   
    }
}
