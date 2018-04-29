using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public class Score
    {
        public int Count = 0;
        public Point Position = new Point(0, 0);
        public Font MyFont = new Font("Compact", 20.0f, GraphicsUnit.Pixel);

        public Score(int x, int y)
        {

            Position.X = x;
            Position.Y = y;
        }

        public void Draw(Graphics g)
        {
            g.DrawString(Count.ToString(), MyFont, Brushes.RoyalBlue, Position.X, Position.Y, new StringFormat());
        }

        public Rectangle GetFrame() 
        {
            Rectangle myRect = new Rectangle(Position.X, Position.Y, (int)MyFont.SizeInPoints * Count.ToString().Length, MyFont.Height);
            return myRect;
        }


        public void Reset()
        {
            Count = 0;
        }

       
        public void Increment()
        {
            Count++;

        }
    }
}
