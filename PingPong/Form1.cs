using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
using System.Threading;
namespace PingPong
{
    public partial class Form1 : Form
    {
        
        private Ball TheBall = new Ball();
        private Paddle ThePaddle = new Paddle();
        private Paddle ThePaddle2 = new Paddle();

        private Score TheScore = null;
        private Score TheScore2 = null;

        private Paddle Brick1 = new Paddle();
        private Paddle Brick2 = new Paddle();
        private Paddle Brick3 = new Paddle();
        private Paddle Brick4 = new Paddle();
        private Paddle Brick5 = new Paddle();
        private Paddle Brick6 = new Paddle();
        private Paddle Brick7 = new Paddle();
        private Paddle Brick8 = new Paddle();
        private Paddle Brick9 = new Paddle();
        private Paddle Brick10 = new Paddle();

        public int kNumberOfTries;
        
        private SoundPlayer BallOut;
        private SoundPlayer HitWall;
        private SoundPlayer HitPaddle;
        private SoundPlayer Win;

        public int speed;

        public Form1()
        {
           
            InitializeComponent();
            MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            ThePaddle.Position.X = this.ClientRectangle.Left;
            
            ThePaddle2.Position.X = this.ClientRectangle.Right - ThePaddle2.Width;
                     
            TheBall.Position.X = this.ClientRectangle.Right - 100;

            TheScore2 = new Score(this.ClientRectangle.Width / 2 +20, this.ClientRectangle.Height / 2 - 175);
            TheScore = new Score(this.ClientRectangle.Width /2 -35, this.ClientRectangle.Height/2 - 175);
           
            TheScore.MyFont = new Font("Arial", 30, FontStyle.Bold);
            TheScore2.MyFont = new Font("Arial", 30, FontStyle.Bold);

            //Bricks visibilty is true

            Brick1.Visible = true;
            Brick2.Visible = true;
            Brick3.Visible = true;
            Brick4.Visible = true;
            Brick5.Visible = true;
            Brick6.Visible = true;
            Brick7.Visible = true;
            Brick8.Visible = true;
            Brick9.Visible = true;
            Brick10.Visible = true;

            //Bricks locations

            Brick1.Position.X = this.ClientRectangle.Width / 2 + (3 * Brick1.Width);
            Brick1.Position.Y = this.ClientRectangle.Height / 4;

            Brick2.Position.X = this.ClientRectangle.Width / 2 + (3 * Brick1.Width);
            Brick2.Position.Y = this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4;

            Brick3.Position.X = this.ClientRectangle.Width / 2 + (3 * Brick1.Width);
            Brick3.Position.Y = this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4;

            Brick4.Position.X = this.ClientRectangle.Width / 2 - (3 * Brick4.Width);
            Brick4.Position.Y = this.ClientRectangle.Height / 4;

            Brick5.Position.X = this.ClientRectangle.Width / 2 - (3 * Brick5.Width);
            Brick5.Position.Y = this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4;

            Brick6.Position.X = this.ClientRectangle.Width / 2 - (3 * Brick6.Width);
            Brick6.Position.Y = this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4;
            

            Brick7.Position.X = this.ClientRectangle.Width / 2;
            Brick7.Position.Y = this.ClientRectangle.Height / 4;

            Brick8.Position.X = this.ClientRectangle.Width / 2;
            Brick8.Position.Y = (this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4) - 25;

            Brick9.Position.X = this.ClientRectangle.Width / 2;
            Brick9.Position.Y = (this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4) - 50;

            Brick10.Position.X = this.ClientRectangle.Width / 2;
            Brick10.Position.Y = (this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4 + this.ClientRectangle.Height / 4) - 75;
            
            Menu dlg = new Menu();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                kNumberOfTries =Convert.ToInt32(dlg.Tries);
            }

            BallOut = new SoundPlayer("BallOut.wav");
            HitWall = new SoundPlayer("WallHit.wav");
            HitPaddle = new SoundPlayer("PaddleHit.wav");
            Win = new SoundPlayer("TaDa.wav");

        }
        
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e) 
        {
       
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Black, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            TheScore.Draw(g);
            TheScore2.Draw(g);
            ThePaddle.Draw(g);
            ThePaddle2.Draw(g);
            TheBall.Draw(g);

            if (Brick1.Visible == true) 
                Brick1.Draw(g);
            if (Brick2.Visible == true)
                Brick2.Draw(g);
            if (Brick3.Visible == true)
                Brick3.Draw(g);
            if (Brick4.Visible == true)
                Brick4.Draw(g);
            if (Brick5.Visible == true)
                Brick5.Draw(g);
            if (Brick6.Visible == true)
                Brick6.Draw(g);
            if (Brick7.Visible == true)
                Brick7.Draw(g);
            if (Brick8.Visible == true)
                Brick8.Draw(g);
            if (Brick9.Visible == true)
                Brick9.Draw(g);
            if (Brick10.Visible == true)
                Brick10.Draw(g);
        }

        private void timer1_Tick(object sender, System.EventArgs e) //runs one round of the game, when started
        {
            TheBall.UpdateBounds(); //gets the ball position
            Invalidate(TheBall.GetBounds()); //redraws the ball
            if (speed < 5)
                TheBall.Move(); //moves the ball 
            else if (speed < 10)
                TheBall.MoveFast();
            else if (speed < 15)
                TheBall.MoveFaster();
            else
                TheBall.MoveFastest();
            TheBall.UpdateBounds(); //updates position of the ball
            Invalidate(TheBall.GetBounds()); //redraws the boll
            CheckForCollision(); //checks for collision
          
        }


        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string result = e.KeyData.ToString();
            
            switch (result)
            {
                case "Up":
                    ThePaddle2.MoveUp();
                    Invalidate(ThePaddle2.GetBounds());
                    if (timer1.Enabled == false)
                        timer1.Start();
                    break;
                case "Down":
                    ThePaddle2.MoveDown(ClientRectangle.Bottom-30);
                    Invalidate(ThePaddle2.GetBounds());
                    if (timer1.Enabled == false) 
                        timer1.Start();
                    break;
                case "W":
                    ThePaddle.MoveUp();
                    Invalidate(ThePaddle.GetBounds());
                    if (timer1.Enabled == false) 
                        timer1.Start();
                    break;
                case "S":
                    ThePaddle.MoveDown(ClientRectangle.Bottom - 30);
                    Invalidate(ThePaddle.GetBounds());
                    if (timer1.Enabled == false) 
                        timer1.Start();
                    break;
                default:
                    break;

            }
        }
        private int HitsPaddle(Point p) // function that check if there are collision with the paddles
        {
            Rectangle PaddleRect = ThePaddle.GetBounds(); 
            Rectangle PaddleRect2 = ThePaddle2.GetBounds();

            if (p.X <= this.ClientRectangle.Left + (PaddleRect.Width))
            {
                if ((p.Y > PaddleRect.Top) && (p.Y < PaddleRect.Bottom)) //ball hits the paddle!
                {
                      if ((p.Y > PaddleRect.Top) && (p.Y <= PaddleRect.Top + PaddleRect.Height / 4))
                          return 1; //hits topest quarter of the paddle
                      else if ((p.Y > PaddleRect.Top + PaddleRect.Height / 4) && (p.Y <= PaddleRect.Top + PaddleRect.Height / 2))
                          return 2; //hits the second top quarter of the paddle
                      else if ((p.Y > PaddleRect.Top + PaddleRect.Height / 2) && (p.Y <= PaddleRect.Top + (PaddleRect.Height / 2)+ PaddleRect.Height / 4))
                          return 3; //hits the third top quarter of the paddle
                      else if ((p.Y > PaddleRect.Top + (PaddleRect.Height / 2) + PaddleRect.Height / 4) && (p.Y <= PaddleRect.Bottom))
                         return 4; //hits the bottomest quarter of the paddle
                }
            }

            if (p.X >= (this.ClientRectangle.Right - (PaddleRect2.Width + TheBall.Width)))
            {
                 if ((p.Y > PaddleRect2.Top) && (p.Y < PaddleRect2.Bottom)) //ball hits the paddle!
                 {
                     if ((p.Y > PaddleRect2.Top) && (p.Y <= PaddleRect2.Top + PaddleRect2.Height / 4))
                         return 5; //hits topest quarter of the paddle
                     else if ((p.Y > PaddleRect2.Top + PaddleRect2.Height / 4) && (p.Y <= PaddleRect2.Top + PaddleRect2.Height / 2))
                         return 6; //hits the second top quarter of the paddle
                     else if ((p.Y > PaddleRect2.Top + PaddleRect2.Height / 2) && (p.Y <= PaddleRect2.Top + (PaddleRect2.Height / 2) + PaddleRect2.Height / 4))
                         return 7; //hits the third top quarter of the paddle
                     else if ((p.Y > PaddleRect2.Top + (PaddleRect2.Height / 2) + PaddleRect2.Height / 4) && (p.Y <= PaddleRect2.Bottom))
                         return 8; //hits the bottomest quarter of the paddle 
                }
                
            }

            return -1;
        }


        private void Reset(int score_side) //resets the ball, stops timer, and redraws the main form
        {

            if (score_side == 1)
            {
                TheBall.XStep = 5;
                TheBall.YStep = 5;
                TheBall.Position.Y = this.ClientRectangle.Top + 50;
                TheBall.Position.X = this.ClientRectangle.Left + 10;
            }
            else if(score_side == 2)
            {
                TheBall.XStep = -5;
                TheBall.YStep = 5;
                TheBall.Position.Y = this.ClientRectangle.Top + 50;
                TheBall.Position.X = this.ClientRectangle.Right - 20;
            }

            timer1.Stop();
            TheBall.UpdateBounds();
            Invalidate(TheBall.GetBounds());
            speed = 0;
        }

        private int HitBrick(Point p) // function that deals with the bricks in the game
        {
            Rectangle Br1 = Brick1.GetBounds();
            Rectangle Br2 = Brick2.GetBounds(); 
            Rectangle Br3 = Brick3.GetBounds();
            Rectangle Br4 = Brick4.GetBounds();
            Rectangle Br5 = Brick5.GetBounds();
            Rectangle Br6 = Brick6.GetBounds();
            Rectangle Br7 = Brick7.GetBounds();
            Rectangle Br8 = Brick8.GetBounds();
            Rectangle Br9 = Brick9.GetBounds();
            Rectangle Br10 = Brick10.GetBounds();

            // Bricks on the right side, they reflect the ball to the right
            if (Brick1.Visible == true)
            {

                if ((p.X <= ((this.ClientRectangle.Width / 2) + (3 * Br1.Width))) && (p.X >= ((this.ClientRectangle.Width / 2) + (2 * Br1.Width))))
                {
                    if ((p.Y > Br1.Top) && (p.Y < Br1.Bottom)) //ball hits the paddle!
                    {
                        if ((p.Y > Br1.Top) && (p.Y <= Br1.Top + Br1.Height / 4))
                        {
                            Brick1.Visible = false;
                            Invalidate();
                            return 1; //hits topest quarter of the paddle
                        }
                        else if ((p.Y > Br1.Top + Br1.Height / 4) && (p.Y <= Br1.Top + Br1.Height / 2))
                        {
                            Brick1.Visible = false;
                            Invalidate();
                            return 2; //hits the second top quarter of the paddle
                        }
                        else if ((p.Y > Br1.Top + Br1.Height / 2) && (p.Y <= Br1.Top + (Br1.Height / 2) + Br1.Height / 4))
                        {
                            Brick1.Visible = false;
                            Invalidate();
                            return 3; //hits the third top quarter of the paddle
                        }
                        else if ((p.Y > Br1.Top + (Br1.Height / 2) + Br1.Height / 4) && (p.Y <= Br1.Bottom))
                        {
                            Brick1.Visible = false;
                            Invalidate();
                            return 4; //hits the bottomest quarter of the paddle
                        }

                    }
                }
            }

            if (Brick2.Visible == true)
            {

                if ((p.X <= ((this.ClientRectangle.Width / 2) + (3 * Br2.Width))) && (p.X >= ((this.ClientRectangle.Width / 2) + (2 * Br2.Width))))
                {
                    if ((p.Y > Br2.Top) && (p.Y < Br2.Bottom)) //ball hits the paddle!
                    {
                        if ((p.Y > Br2.Top) && (p.Y <= Br2.Top + Br2.Height / 4))
                        {
                            Brick2.Visible = false;
                            Invalidate();
                            return 1; //hits topest quarter of the paddle
                        }
                        else if ((p.Y > Br2.Top + Br2.Height / 4) && (p.Y <= Br2.Top + Br2.Height / 2))
                        {
                            Brick2.Visible = false;
                            Invalidate();
                            return 2; //hits the second top quarter of the paddle
                        }
                        else if ((p.Y > Br2.Top + Br2.Height / 2) && (p.Y <= Br2.Top + (Br2.Height / 2) + Br2.Height / 4))
                        {
                            Brick2.Visible = false;
                            Invalidate();
                            return 3; //hits the third top quarter of the paddle
                        }
                        else if ((p.Y > Br2.Top + (Br2.Height / 2) + Br2.Height / 4) && (p.Y <= Br2.Bottom))
                        {
                            Brick2.Visible = false;
                            Invalidate();
                            return 4; //hits the bottomest quarter of the paddle
                        }

                    }
                }
            }
            
            if (Brick3.Visible == true)
            {

                if ((p.X <= ((this.ClientRectangle.Width / 2) + (3 * Br3.Width))) && (p.X >= ((this.ClientRectangle.Width / 2) + (2 * Br3.Width))))
                {
                    if ((p.Y > Br3.Top) && (p.Y < Br3.Bottom)) //ball hits the paddle!
                    {
                        if ((p.Y > Br3.Top) && (p.Y <= Br3.Top + Br3.Height / 4))
                        {
                            Brick3.Visible = false;
                            Invalidate();
                            return 1; //hits topest quarter of the paddle
                        }
                        else if ((p.Y > Br3.Top + Br3.Height / 4) && (p.Y <= Br3.Top + Br3.Height / 2))
                        {
                            Brick3.Visible = false;
                            Invalidate();
                            return 2; //hits the second top quarter of the paddle
                        }
                        else if ((p.Y > Br3.Top + Br3.Height / 2) && (p.Y <= Br3.Top + (Br3.Height / 2) + Br3.Height / 4))
                        {
                            Brick3.Visible = false;
                            Invalidate();
                            return 3; //hits the third top quarter of the paddle
                        }
                        else if ((p.Y > Br3.Top + (Br3.Height / 2) + Br3.Height / 4) && (p.Y <= Br3.Bottom))
                        {
                            Brick3.Visible = false;
                            Invalidate();
                            return 4; //hits the bottomest quarter of the paddle
                        }

                    }
                }
            }

            //Bricks on the left side, they reflect the ball to the left

            if (Brick4.Visible == true)
            {

                if ((p.X <= ((this.ClientRectangle.Width / 2) - (2 * Br4.Width))) && (p.X >= ((this.ClientRectangle.Width / 2) - (3 * Br4.Width))))
                {
                    if ((p.Y > Br4.Top) && (p.Y < Br4.Bottom)) //ball hits the paddle!
                    {
                        if ((p.Y > Br4.Top) && (p.Y <= Br4.Top + Br4.Height / 4))
                        {
                            Brick4.Visible = false;
                            Invalidate();
                            return 5; //hits topest quarter of the paddle
                        }
                        else if ((p.Y > Br4.Top + Br4.Height / 4) && (p.Y <= Br4.Top + Br4.Height / 2))
                        {
                            Brick4.Visible = false;
                            Invalidate();
                            return 6; //hits the second top quarter of the paddle
                        }
                        else if ((p.Y > Br4.Top + Br4.Height / 2) && (p.Y <= Br4.Top + (Br4.Height / 2) + Br4.Height / 4))
                        {
                            Brick4.Visible = false;
                            Invalidate();
                            return 7; //hits the third top quarter of the paddle
                        }
                        else if ((p.Y > Br4.Top + (Br4.Height / 2) + Br4.Height / 4) && (p.Y <= Br4.Bottom))
                        {
                            Brick4.Visible = false;
                            Invalidate();
                            return 8; //hits the bottomest quarter of the paddle
                        }

                    }
                }
            }

            if (Brick5.Visible == true)
            {

                if ((p.X <= ((this.ClientRectangle.Width / 2) - (2 * Br5.Width))) && (p.X >= ((this.ClientRectangle.Width / 2) - (3 * Br5.Width))))
                {
                    if ((p.Y > Br5.Top) && (p.Y < Br5.Bottom)) //ball hits the paddle!
                    {
                        if ((p.Y > Br5.Top) && (p.Y <= Br5.Top + Br5.Height / 4))
                        {
                            Brick5.Visible = false;
                            Invalidate();
                            return 5; //hits topest quarter of the paddle
                        }
                        else if ((p.Y > Br5.Top + Br5.Height / 4) && (p.Y <= Br5.Top + Br5.Height / 2))
                        {
                            Brick5.Visible = false;
                            Invalidate();
                            return 6; //hits the second top quarter of the paddle
                        }
                        else if ((p.Y > Br5.Top + Br5.Height / 2) && (p.Y <= Br5.Top + (Br5.Height / 2) + Br5.Height / 4))
                        {
                            Brick5.Visible = false;
                            Invalidate();
                            return 7; //hits the third top quarter of the paddle
                        }
                        else if ((p.Y > Br5.Top + (Br5.Height / 2) + Br5.Height / 4) && (p.Y <= Br5.Bottom))
                        {
                            Brick5.Visible = false;
                            Invalidate();
                            return 8; //hits the bottomest quarter of the paddle
                        }

                    }
                }
            }

            if (Brick6.Visible == true)
            {

                if ((p.X <= ((this.ClientRectangle.Width / 2) - (2 * Br6.Width))) && (p.X >= ((this.ClientRectangle.Width / 2) - (3 * Br6.Width))))
                {
                    if ((p.Y > Br6.Top) && (p.Y < Br6.Bottom)) //ball hits the paddle!
                    {
                        if ((p.Y > Br6.Top) && (p.Y <= Br6.Top + Br6.Height / 4))
                        {
                            Brick6.Visible = false;
                            Invalidate();
                            return 5; //hits topest quarter of the paddle
                        }
                        else if ((p.Y > Br6.Top + Br6.Height / 4) && (p.Y <= Br6.Top + Br6.Height / 2))
                        {
                            Brick6.Visible = false;
                            Invalidate();
                            return 6; //hits the second top quarter of the paddle
                        }
                        else if ((p.Y > Br6.Top + Br6.Height / 2) && (p.Y <= Br6.Top + (Br6.Height / 2) + Br6.Height / 4))
                        {
                            Brick6.Visible = false;
                            Invalidate();
                            return 7; //hits the third top quarter of the paddle
                        }
                        else if ((p.Y > Br6.Top + (Br6.Height / 2) + Br6.Height / 4) && (p.Y <= Br6.Bottom))
                        {
                            Brick6.Visible = false;
                            Invalidate();
                            return 8; //hits the bottomest quarter of the paddle
                        }

                    }
                }
            }


            // Bricks in the middle, the ball gets through them without collision
            if (Brick7.Visible == true)
            {
                if ((p.X <= ((this.ClientRectangle.Width / 2) + (Br7.Width / 2))) && (p.X >= ((this.ClientRectangle.Width / 2) - (Br7.Width / 2))))
                {
                    if ((p.Y > Br7.Top) && (p.Y < Br7.Bottom)) 
                    {
                        Brick7.Visible = false;
                    }
                }
     
            }

            if (Brick8.Visible == true)
            {
                if ((p.X <= ((this.ClientRectangle.Width / 2) + (Br8.Width / 2))) && (p.X >= ((this.ClientRectangle.Width / 2) - ( Br8.Width / 2))))
                {
                    if ((p.Y > Br8.Top) && (p.Y < Br8.Bottom)) 
                    {
                        Brick8.Visible = false;
                    }
                }

            }

            if (Brick9.Visible == true)
            {
                if ((p.X <= ((this.ClientRectangle.Width / 2) + (Br9.Width / 2))) && (p.X >= ((this.ClientRectangle.Width / 2) - (Br9.Width / 2))))
                {
                    if ((p.Y > Br9.Top) && (p.Y < Br9.Bottom))
                    {
                        Brick9.Visible = false;
                    }
                }

            }

            if (Brick10.Visible == true)
            {
                if ((p.X <= ((this.ClientRectangle.Width / 2) + (Br10.Width / 2))) && (p.X >= ((this.ClientRectangle.Width / 2) - (Br10.Width / 2))))
                {
                    if ((p.Y > Br10.Top) && (p.Y < Br10.Bottom)) 
                    {
                        Brick10.Visible = false;
                    }
                }

            }

            return -1;
           
        }


        public void ColorChange() // function that changes the background color after scoring
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Purple);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            System.Threading.Thread.Sleep(400);
            
        }
       
        private void CheckForCollision() // collision with the paddle
        {


            if (TheBall.Position.Y < 0)  // hit the top of the form
            {
                TheBall.YStep *= -1;
                TheBall.Position.Y += TheBall.YStep;
                HitWall.Play();
            }

            if (TheBall.Position.Y > this.ClientRectangle.Bottom - TheBall.Width)  // hit the bottom of the form
            {
                TheBall.YStep *= -1;
                TheBall.Position.Y += TheBall.YStep;
                HitWall.Play();
            }

            if (TheBall.Position.X > this.ClientRectangle.Right)  // hit the right side
            {

                BallOut.Play();

                ColorChange();
                Invalidate();
                TheScore.Increment();
                Invalidate(TheScore.GetFrame());

               
                if (TheScore.Count >= kNumberOfTries) //check if you reached the number of points
                {
                    timer1.Stop();
                    Win.Play();
                    string message = "Player Left Won!!! \n Scores = " + TheScore.Count;
                    string caption = "Game Over";
                    MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
                    DialogResult result;

                    result = MessageBox.Show(this, message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Cancel)
                    {

                        Application.Exit();

                    }
                    else if (result == DialogResult.Retry)
                    {
                        Application.Restart();
                    }

                }

                else
                {
                    Reset(1);
                }
            }

            if (TheBall.Position.X < 0)  // hit the left side
            {
                BallOut.Play();

                ColorChange();
                Invalidate();
                TheScore2.Increment();
                Invalidate(TheScore2.GetFrame());


                if (TheScore2.Count >= kNumberOfTries)//check if you reached the number of points
                {
                    timer1.Stop();
                    Win.Play();
                    string message = "Player Right Won!!! \n Scores = " + TheScore2.Count;
                    string caption = "Game Over";
                    MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
                    DialogResult result;

                    result = MessageBox.Show(this, message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Cancel)
                    {

                        Application.Exit();

                    }
                    else if (result == DialogResult.Retry)
                    {
                        Application.Restart();
                    }

                }
                else
                {
                    Reset(2);
                }

            }


            int hp = HitsPaddle(TheBall.Position); //check if the ball hit the paddle
            if (hp > -1)// checks if the ball is not lost
            {
                HitPaddle.Play();
                speed++;
                switch (hp) //new direction of the ball depends on which quarter of the paddle is hit
                {
                   
                    case 1:
                        TheBall.XStep = 3;
                        TheBall.YStep = -7;
                        break;
                    case 2:
                        TheBall.XStep = 5;
                        TheBall.YStep = -5;
                        break;
                    case 3:
                        TheBall.XStep = 5;
                        TheBall.YStep = 5;
                        break;
                    case 4:
                        TheBall.XStep = 3;
                        TheBall.YStep = 7;
                        break;

                    case 5:
                        TheBall.XStep = -3;
                        TheBall.YStep = -7;
                        break;
                    case 6:
                        TheBall.XStep = -5;
                        TheBall.YStep = -5;
                        break;
                    case 7:
                        TheBall.XStep = -5;
                        TheBall.YStep = 5;
                        break;
                    case 8:
                        TheBall.XStep = -3;
                        TheBall.YStep = 7;
                        break;

                }
            }
        
            int hb = HitBrick(TheBall.Position); //check if the ball hit one of the bricks
            if (hb > -1)
            {               
                HitPaddle.Play();
                switch (hb) 
                {
                    
                    case 1:
                        TheBall.XStep = 3;
                        TheBall.YStep = -7;
                        break;
                    case 2:
                        TheBall.XStep = 5;
                        TheBall.YStep = -5;
                        break;
                    case 3:
                        TheBall.XStep = 5;
                        TheBall.YStep = 5;
                        break;
                    case 4:
                        TheBall.XStep = 3;
                        TheBall.YStep = 7;
                        break;

                    case 5:
                        TheBall.XStep = -3;
                        TheBall.YStep = -7;
                        break;
                    case 6:
                        TheBall.XStep = -5;
                        TheBall.YStep = -5;
                        break;
                    case 7:
                        TheBall.XStep = -5;
                        TheBall.YStep = 5;
                        break;
                    case 8:
                        TheBall.XStep = -3;
                        TheBall.YStep = 7;
                        break;
                }   
            }

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
