using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public class Ball : GameObject
    {
        public int XStep = 5;
        public int YStep = 5;

        public Ball(string fileName)
            : base(fileName)
        {

        }

        public Ball()
            : base("ball.gif")
        {

        }


        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }

        public void Move()
        {
            Position.X += XStep;
            Position.Y += YStep;

        }

        public void MoveFast()
        {
            Position.X += 2 * XStep;
            Position.Y += 2 * YStep;

        }

        public void MoveFaster()
        {
            Position.X += 3* XStep;
            Position.Y += 3* YStep;

        }

        public void MoveFastest()
        {
            Position.X += 4 * XStep;
            Position.Y += 4 * YStep;

        }

    }
}
