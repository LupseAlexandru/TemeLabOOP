using System;
using System.Drawing;
using System.Windows.Forms;

namespace Big_Ball_Game
{
    class RepelentBall : Ball
    {
        public RepelentBall(Panel canvasPanel, Random random, int initialRadius)
            : base(canvasPanel, random, initialRadius)
        {
        }

        public override void Move()
        {
            x += dx;
            y += dy;
            Reflect();
        }

        public override void HandleCollision(Ball otherBall)
        {
            if (otherBall is RegularBall regularBall)
            {
                red = regularBall.Red;
                green = regularBall.Green;
                blue = regularBall.Blue;
                dx = -dx;
                dy = -dy;
            }
            else if (otherBall is RepelentBall repelentBall)
            {
                int tempRed = red;
                int tempGreen = green;
                int tempBlue = blue;
                red = repelentBall.Red;
                green = repelentBall.Green;
                blue = repelentBall.Blue;
                repelentBall.red = tempRed;
                repelentBall.green = tempGreen;
                repelentBall.blue = tempBlue;

                dx = -dx;
                dy = -dy;
                repelentBall.dx = -repelentBall.dx;
                repelentBall.dy = -repelentBall.dy;
            }
            else if (otherBall is MonsterBall)
            {
                radius = Math.Max(radius / 2, 1);
                dx = -dx;
                dy = -dy;
            }
        }

        public override void Draw(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(red, green, blue)))
            {
                g.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
            }
        }
    }
}
