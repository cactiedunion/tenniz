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

    public Collider Collider;

    public float MaxSpeed = 50;
    public float Acceleration = 800f;
    public float DeAcceleration = 20;

    public PlayerIndex PlayerIndex;

    ICondition moveRight;
    ICondition moveLeft;
    ICondition moveUp;
    ICondition moveDown;

    ICondition swing;

    Ball ball;

    public Player(PlayerIndex playerIndex, Ball ball)
    {
        this.ball = ball;

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

            swing = new AnyCondition(
                new KeyboardCondition(Keys.Space)
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

            swing = new AnyCondition(
                new KeyboardCondition(Keys.Enter)
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

        Collider = new Collider(Position, new Vector2(Texture.Width, Texture.Height));
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

        Collider.Position = Position;

        BallDetection();
    }

    public void BallDetection()
    {
        if (Collider.Intersects(ball.BallCollider) && Collider.Intersects(ball.BallCollider))
        {
            if (swing.Pressed())
            {
                ball.HeightVelocity = -100;

                float powerX = 2;

                if(PlayerIndex == PlayerIndex.Two)
                {
                    powerX *= -1;
                }
                powerX += Velocity.X / 30;

                ball.Velocity.X = powerX;

                float powerY = 0;
                powerY += Velocity.Y / 30;
                ball.Velocity.Y = powerY;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
        Collider.DebugDraw(spriteBatch);
    }
}