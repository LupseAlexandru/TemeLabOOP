using System;
using System.Drawing;
using System.Windows.Forms;

namespace Big_Ball_Game
{
    class RegularBall : Ball
    {
        public RegularBall(Panel canvasPanel, Random random, int initialRadius)
            : base(canvasPanel, random, initialRadius)
        {
        }

        public override void Draw(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(red, green, blue)))
            {
                g.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
            }
        }

        public override void HandleCollision(Ball otherBall)
        {
            if (otherBall is RegularBall otherRegularBall)
            {
                if (Radius > otherRegularBall.Radius)
                {
                    int newRadius = Math.Min(radius + otherRegularBall.Radius, MaxRadius);
                    red = (red * radius + otherRegularBall.red * otherRegularBall.radius) / newRadius;
                    green = (green * radius + otherRegularBall.green * otherRegularBall.radius) / newRadius;
                    blue = (blue * radius + otherRegularBall.blue * otherRegularBall.radius) / newRadius;
                    radius = newRadius;
                    otherRegularBall.MarkForRemoval();
                }
                else
                {
                    otherRegularBall.HandleCollision(this); 
                }
            }
            else if (otherBall is MonsterBall)
            {
                MarkForRemoval();
                otherBall.HandleCollision(this);
            }
            else if (otherBall is RepelentBall)
            {
                otherBall.HandleCollision(this);
            }
        }

        public override void Move()
        {
            x += dx;
            y += dy;
            Reflect();
        }
    }
}

