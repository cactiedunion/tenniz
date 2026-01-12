using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tenniz;

public class Player
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Texture2D Texture;

    public float MaxSpeed = 100;
    public float Acceleration = 800f;
    public float DeAcceleration = 20;

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        Texture = Texture2D.FromFile(graphicsDevice, "Assets/Player1.png");
    }

    public void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 direction = Vector2.Zero;

        if (InputHelper.NewKeyboard.IsKeyDown(Keys.W))
            direction.Y -= 1;
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.S))
            direction.Y += 1;
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.A))
            direction.X -= 1;
        if (InputHelper.NewKeyboard.IsKeyDown(Keys.D))
            direction.X += 1;

        if (direction.LengthSquared() > 0)
            direction.Normalize();

        Vector2 acceleration = direction * Acceleration;

        Velocity += acceleration * delta;

        if(direction.Equals(Vector2.Zero))
            Velocity -= Velocity * DeAcceleration * delta;

        if (Velocity.LengthSquared() > MaxSpeed * MaxSpeed)
            Velocity = Vector2.Normalize(Velocity) * MaxSpeed;

        Position += Velocity * delta;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
}