using System;
using System.Drawing;
using System.Windows.Forms;

namespace Big_Ball_Game
{
    abstract class Ball
    {
        protected int x, y;
        protected int dx, dy;
        protected int radius;
        protected int red, green, blue;
        public bool Removed { get; set; } = false;

        protected const int MaxRadius = 50;
        protected Panel canvasPanel;

        public int Radius => radius;
        public int Red => red;
        public int Green => green;
        public int Blue => blue;

        public Ball(Panel canvasPanel, Random random, int initialRadius)
        {
            this.canvasPanel = canvasPanel;
            x = random.Next(initialRadius, canvasPanel.Width - initialRadius);
            y = random.Next(initialRadius, canvasPanel.Height - initialRadius);
            dx = random.Next(-5, 6);
            dy = random.Next(-5, 6);
            radius = initialRadius;
            red = random.Next(0, 256);
            green = random.Next(0, 256);
            blue = random.Next(0, 256);
        }

        public abstract void Move();

        public abstract void HandleCollision(Ball otherBall);

        public void Reflect()
        {
            if (x - radius < 0)
            {
                x = radius;
                dx = -dx;
            }
            else if (x + radius > canvasPanel.Width)
            {
                x = canvasPanel.Width - radius;
                dx = -dx;
            }

            if (y - radius < 0)
            {
                y = radius;
                dy = -dy;
            }
            else if (y + radius > canvasPanel.Height)
            {
                y = canvasPanel.Height - radius;
                dy = -dy;
            }
        }

        public bool Intersects(Ball otherBall)
        {
            int distanceSquared = (x - otherBall.x) * (x - otherBall.x) + (y - otherBall.y) * (y - otherBall.y);
            int radiusSumSquared = (radius + otherBall.radius) * (radius + otherBall.radius);
            return distanceSquared <= radiusSumSquared;
        }

        public abstract void Draw(Graphics g);

        public virtual void MarkForRemoval()
        {
            Removed = true;
        }
    }
}
