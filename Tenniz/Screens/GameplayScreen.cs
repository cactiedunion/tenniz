using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tenniz.Screens;

public class GameplayScreen : Screen
{
    public Player Player1;
    public Player Player2;
    public Rectangle PlayArea = new Rectangle(0, 0, 256, 128);

    public Texture2D GroundTexture;
    public Texture2D NetTexture;

    public Rectangle NetTopSourceRectangle;
    public Rectangle NetMiddleSourceRectangle;
    public Rectangle NetBottomSourceRectangle;

    private RenderTarget2D _renderTarget;
    public const int RenderTargetScale = 8;

    public override void LoadContent(GraphicsDevice graphicsDevice)
    {
        GroundTexture = Texture2D.FromFile(graphicsDevice, "Assets/ground.png");

        NetTexture = Texture2D.FromFile(graphicsDevice, "Assets/net.png");
        NetTopSourceRectangle = new Rectangle(0, 0, 16, 16);
        NetMiddleSourceRectangle = new Rectangle(0, 16, 16, 16);
        NetBottomSourceRectangle = new Rectangle(0, 32, 16, 16);

        _renderTarget = new RenderTarget2D(
            graphicsDevice,
            PlayArea.Width * RenderTargetScale,
            PlayArea.Height  * RenderTargetScale,
            false,
            SurfaceFormat.Color,
            DepthFormat.None
        );

        Player1 = new Player(PlayerIndex.One);
        Player1.Position = new Vector2(PlayArea.Width / 2 - PlayArea.Width / 4, PlayArea.Y / 2);
        Player1.LoadContent(graphicsDevice);

        Player2 = new Player(PlayerIndex.Two);
        Player2.LoadContent(graphicsDevice);
        Player2.Position = new Vector2(PlayArea.Width / 2 + PlayArea.Width / 4, PlayArea.Y / 2);
    }

    public override void Update(GameTime gameTime)
    {
        Player1.Update(gameTime);
        Player2.Update(gameTime);

        int left = PlayArea.X;
        int right = PlayArea.X + PlayArea.Width - 1;

        int top = PlayArea.Y - 8;
        int bottom = top + PlayArea.Height - 27 - 1;

        int netLeft = PlayArea.Width / 2 - NetTopSourceRectangle.Width - 8 + 4;
        int netRight = PlayArea.Width / 2 + NetTopSourceRectangle.Width + 2;

        ConstrainPlayer(Player1, left, netLeft, top, bottom);
        ConstrainPlayer(Player2, netRight - 16, right - 16, top, bottom);
    }

    private void ConstrainPlayer(Player player, int minX, int maxX, int minY, int maxY)
    {
        player.Position.X = Math.Clamp(player.Position.X, minX, maxX);
        player.Position.Y = Math.Clamp(player.Position.Y, minY, maxY);
    }

    public override void Draw(SpriteBatch spritebatch)
    {
        var gd = spritebatch.GraphicsDevice;

        gd.SetRenderTarget(_renderTarget);
        gd.Clear(Color.CornflowerBlue);

        spritebatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(RenderTargetScale, RenderTargetScale, 1));

        spritebatch.Draw(
            GroundTexture,
            new Rectangle(0, 0, PlayArea.Width, PlayArea.Height),
            Color.White
        );

        Player1.Draw(spritebatch);
        Player2.Draw(spritebatch);
        DrawNet(spritebatch);

        spritebatch.End();



        gd.SetRenderTarget(null);

        int scale = Math.Min(
            gd.Viewport.Width / PlayArea.Width,
            gd.Viewport.Height / PlayArea.Height
        );

        int finalWidth = PlayArea.Width * scale;
        int finalHeight = PlayArea.Height * scale;

        int x = (gd.Viewport.Width - finalWidth) / 2;
        int y = (gd.Viewport.Height - finalHeight) / 2;

        spritebatch.Begin(samplerState: SamplerState.PointClamp);

        spritebatch.Draw(
            _renderTarget,
            new Rectangle(x, y, finalWidth, finalHeight),
            Color.White
        );

        spritebatch.End();
    }

    private void DrawNet(SpriteBatch spritebatch)
    {
        float xPos = PlayArea.Width / 2f - NetTopSourceRectangle.Width / 2f;

        for(int i = 0; i < PlayArea.Height; i += NetTopSourceRectangle.Height)
        {
            Rectangle destinationRectangle = new Rectangle(
                (int)xPos,
                i,
                NetTopSourceRectangle.Width,
                NetTopSourceRectangle.Height
            );
            if (i == 0)
            {
                spritebatch.Draw(NetTexture, destinationRectangle, NetTopSourceRectangle, Color.White);
            }
            else if (i + NetMiddleSourceRectangle.Height >= PlayArea.Height)
            {
                spritebatch.Draw(NetTexture, destinationRectangle, NetBottomSourceRectangle, Color.White);
            }
            else
            {
                spritebatch.Draw(NetTexture, destinationRectangle, NetMiddleSourceRectangle, Color.White);
            }
        }
    }
}