using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacew_race
{
    public partial class Form1 : Form
    { // this is every variable brush and other thing we need up here 
        bool wPressed = false;
        bool sPressed = false;
        bool upPressed = false;
        bool downPressed = false;

        List<Rectangle> balls = new List<Rectangle>();
        List<int> ballsSpeed = new List<int>();
        List<int> ballsSize = new List<int>();
        List<string> ballsSide = new List<string>();

        Rectangle player1 = new Rectangle(195, 300, 15, 15);
        Rectangle player2 = new Rectangle(395, 300, 15, 15);
        Rectangle newBall = new Rectangle(0, 0, 0, 0);
        Rectangle Line = new Rectangle(290, 500, 290, 0);

        SolidBrush white = new SolidBrush(Color.White);
        Pen White = new Pen(Color.White, 10);
        SolidBrush Black = new SolidBrush(Color.Black);

        int playerSpeed = 10;
        int i = 0;
        int player1Score = 0;
        int player2Score = 0;
        int y = 0;
        double timer = 100;
        int check = 0;
        string pick;

        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();


        }

        private void timer1_Tick(object sender, EventArgs e)
        { // check what button is pressed to move the player in that direction if they can
            if (upPressed == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }
            if (downPressed == true && player1.Y < 350)
            {
                player1.Y += playerSpeed;
            }
            if (wPressed == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }
            if (sPressed == true && player2.Y < 350)
            {
                player2.Y += playerSpeed;
            }
           // check if it wants to make a new ball and does
            int check = randGen.Next(1, 101);
            int ballspeed = randGen.Next(10, 16);
            int size = randGen.Next(5, 16);

            if (check > 85)
            {
                int y = randGen.Next(50, 350);

                check = randGen.Next(1, 3);
                if (check > 1)
                {
                    Rectangle newBall = new Rectangle(0, y, size, size);
                    balls.Add(newBall);
                    ballsSpeed.Add(ballspeed);
                    ballsSize.Add(size);
                    ballsSide.Add("left");
                }
                else
                {
                    Rectangle newBall = new Rectangle(this.Width - size, y, size, size);
                    balls.Add(newBall);
                    ballsSpeed.Add(ballspeed);
                    ballsSize.Add(size);
                    ballsSide.Add("right");
                }
            }

            //this put balls on the left and right side
            for (int i = 0; i < balls.Count; i++)
            {
                if (ballsSide[i] == "left")
                {
                    int x = balls[i].X + ballsSpeed[i];
                    balls[i] = new Rectangle(x, balls[i].Y, ballsSize[i], ballsSize[i]);
                }
                else
                {
                    int x = balls[i].X - ballsSpeed[i];
                    balls[i] = new Rectangle(x, balls[i].Y, ballsSize[i], ballsSize[i]);
                }


            }
            // this checks if you've reached the top so you score points
            if (player1.Y < 10)
            {
                player1Score++;
                player1.Y = this.Height + 10;
                label1.Text = $"Player 1 Score is {player1Score}";
            }

            if (player2.Y < 10)
            {
                player2Score++;
                player2.Y = this.Height + 10;
                label2.Text = $"Player 2 Score is {player2Score}";
            }

            for (int i = 0; i < balls.Count; i++)
            {

                // this checks if you've been hit
                if (player1.IntersectsWith(balls[i]))
                {
                    player1.Y = this.Height + 10;
                }
                if (player2.IntersectsWith(balls[i]))
                {
                    player2.Y = this.Height + 10;
                }
            }
           // this checks if the game is over
            if (timer > 500)
            {
                if (player1Score < player2Score)
                {
                    gameWinner.Text = $"player 2 has won";
                }
                else if (player1Score == player2Score)
                {
                    gameWinner.Text = $"the Game is a draw";
                }
                else
                {
                    gameWinner.Text = "player 1 has won";
                }

                if (gameWinner.Text == "player 1 has won" || gameWinner.Text == "player 2 has won"|| gameWinner.Text == $"the Game is a draw")
                {
                    timer1.Enabled = false;
                }
                
            }
            //this is the timer
            timer = timer + .104;
            Refresh();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // key ups
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;

            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // key downs
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;

            }




        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // paints the 2 players and the balls also paints the timer which is the giant LINE
            e.Graphics.FillRectangle(white, player1);
            e.Graphics.FillRectangle(white, player2);
            e.Graphics.DrawLine(White, 290, 500, 290, (int)timer);
            e.Graphics.FillRectangle(Black, Line);
            for (int i = 0; i < balls.Count; i++)
            {
                e.Graphics.FillEllipse(white, balls[i]);

            }


        }
    }
}
