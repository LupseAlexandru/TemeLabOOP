using System;
using System.Drawing;
using System.Windows.Forms;

namespace Big_Ball_Game
{
    class MonsterBall : Ball
    {
        public MonsterBall(Panel canvasPanel, Random random, int initialRadius)
            : base(canvasPanel, random, initialRadius)
        {
            dx = 0;
            dy = 0; 
        }

        public override void Move()
        {

        }

        public override void HandleCollision(Ball otherBall)
        {
            if (otherBall is RegularBall || otherBall is RepelentBall)
            {
                otherBall.MarkForRemoval();
                radius = Math.Min(radius + otherBall.Radius, MaxRadius);
            }
        }

        public override void Draw(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                g.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
            }
        }
    }
}
