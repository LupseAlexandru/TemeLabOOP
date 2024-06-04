using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Big_Ball_Game
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private List<Ball> balls = new List<Ball>();
        private bool finished = false;


        public Form1()
        {
            InitializeComponent();
            SetDoubleBuffered(panel1);
        }
        private void SetDoubleBuffered(Control control) // pentru a fi mai smooth miscarea bilelor pe panel
        {
            control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(control, true, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int regularCount = 4;
            int monsterCount = 1;
            int repelentCount = 3;
            InitializeSystem(regularCount, monsterCount, repelentCount);

            Thread simulationThread = new Thread(Simulate);
            simulationThread.IsBackground = true;
            simulationThread.Start();
        }

        private void InitializeSystem(int regularCount, int monsterCount, int repelentCount)
        {
            for (int i = 0; i < regularCount; i++)
            {
                balls.Add(new RegularBall(panel1, random, random.Next(5, 16)));
            }

            for (int i = 0; i < monsterCount; i++)
            {
                balls.Add(new MonsterBall(panel1, random, random.Next(10, 21)));
            }

            for (int i = 0; i < repelentCount; i++)
            {
                balls.Add(new RepelentBall(panel1, random, random.Next(5, 16)));
            }
        }

        private void Simulate()
        {
            while (!finished)
            {
                Turn();
                Delay();
                panel1.Invalidate();
                Application.DoEvents();
            }
        }

        private void Turn()
        {
            List<Ball> ballsToRemove = new List<Ball>();

            foreach (Ball ball in balls)
            {
                ball.Move();
                CheckCollision(ball, ballsToRemove);
            }

            balls.RemoveAll(ball => ball.Removed);
        }

        private void CheckCollision(Ball currentBall, List<Ball> ballsToRemove)
        {
            foreach (Ball otherBall in balls)
            {
                if (currentBall != otherBall && currentBall.Intersects(otherBall))
                {
                    currentBall.HandleCollision(otherBall);
                }
            }

            currentBall.Reflect();
        }

        private void Delay()
        {
            Thread.Sleep(50);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Azure);
             
            foreach (Ball ball in balls)
            {
                ball.Draw(e.Graphics);
            }
        }
    }
}
