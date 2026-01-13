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

    public float MaxSpeed = 50;
    public float Acceleration = 800f;
    public float DeAcceleration = 20;

    public PlayerIndex PlayerIndex;

    ICondition moveRight;
    ICondition moveLeft;
    ICondition moveUp;
    ICondition moveDown;

    public Player(PlayerIndex playerIndex)
    {
        PlayerIndex = playerIndex;

        if(PlayerIndex == PlayerIndex.One)
        {
            moveRight = new AnyCondition(
                new KeyboardCondition(Keys.D)
            );
            moveLeft = new AnyCondition(
                new KeyboardCondition(Keys.A)
            );
            moveUp = new AnyCondition(
                new KeyboardCondition(Keys.W)
            );
            moveDown = new AnyCondition(
                new KeyboardCondition(Keys.S)
            );
        }
        else if(PlayerIndex == PlayerIndex.Two)
        {
            moveRight = new AnyCondition(
                new KeyboardCondition(Keys.Right)
            );
            moveLeft = new AnyCondition(
                new KeyboardCondition(Keys.Left)
            );
            moveUp = new AnyCondition(
                new KeyboardCondition(Keys.Up)
            );
            moveDown = new AnyCondition(
                new KeyboardCondition(Keys.Down)
            );
        }
    }

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        if(PlayerIndex == PlayerIndex.One)
        {
            Texture = Texture2D.FromFile(graphicsDevice, "Assets/Player1.png");
        }
        else if(PlayerIndex == PlayerIndex.Two)
        {
            Texture = Texture2D.FromFile(graphicsDevice, "Assets/Player2.png");
        }
    }

    public void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 direction = Vector2.Zero;

        if (moveUp.Held())
            direction.Y -= 1;
        if (moveDown.Held())
            direction.Y += 1;
        if (moveLeft.Held())
            direction.X -= 1;
        if (moveRight.Held())
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