using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace PingPong
{
    public class Paddle : GameObject
    {
        const int kInterval = 7;
        public bool Visible { get; set; }
        public Paddle(): base("paddle.gif")
        {
            Position.X = 200;
            Position.Y = 250;
        }
        public void MoveUp()
        {
            Position.Y -= kInterval;
            if (Position.Y < 0)
                Position.Y = 0;
        }

        public void MoveDown(int nLimit) //nlimit refers to the right border of the frame
        {
            Position.Y += kInterval;
            if (Position.Y > nLimit - Width) //left border of the paddle cannot be greater than 
                                             // the right border of the frame - width of the paddle
                Position.Y = nLimit - Width;
        }
        
    }
}
