using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tenniz;

public class Ball
{
    public Vector2 Position;

    public Vector2 Velocity;

    public Collider ShadowCollider;
    public Collider BallCollider;

    public float DeAcceleration = 1.5f;

    public float HeightVelocity;

    public float Gravity = 200f;

    private float height;
    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            if(value > 0)
            {
                value = 0;
                OnGround();
            }
            height = value;
        }
    }

    public Texture2D Texture;

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        Texture = Texture2D.FromFile(graphicsDevice, "Assets/ball.png");

        ShadowCollider = new Collider(Position, new Vector2(Texture.Width, Texture.Height));
        BallCollider = new Collider(Position + new Vector2(0, Height), new Vector2(Texture.Width, Texture.Height));
    }

    public void Update(GameTime gameTime)
    {
        /*
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.Up))
        {
            Velocity.Y -= 0.1f;
        }
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.Down))
        {
            Velocity.Y += 0.1f;
        }
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.Right))
        {
            Velocity.X += 0.1f;
        }
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.Left))
        {
            Velocity.X -= 0.1f;
        }

        if (InputHelper.NewKeyboard.IsKeyDown(Keys.Q))
        {
            HeightVelocity += 5;
        }
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.E))
        {
            HeightVelocity -= 5;
        }
        */

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(Height > 0)
        {
            Velocity *= 0.5f;
        }
        Position += Velocity;

        Velocity.X = MathHelper.Lerp(Velocity.X, 0f, DeAcceleration * dt);
        Velocity.Y = MathHelper.Lerp(Velocity.Y, 0f, DeAcceleration * dt);

        HeightVelocity += Gravity * dt;

        Height += HeightVelocity * dt;

        ShadowCollider.Position = Position;
        BallCollider.Position = Position + new Vector2(0, Height);
    }

    public void OnGround()
    {
        HeightVelocity = -HeightVelocity * 0.8f;
        if(Height < -0.1f)
        {
            Velocity = Velocity * 1.1f;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.DarkGray * 0.5f);
        spriteBatch.Draw(Texture, Position + new Vector2(0, Height), Color.White);
        ShadowCollider.DebugDraw(spriteBatch);
        BallCollider.DebugDraw(spriteBatch);
    }
}
