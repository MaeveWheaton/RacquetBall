using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RacquetBall
{
    public partial class Form1 : Form
    { 
        //global varibles
        Rectangle player1 = new Rectangle(10, 130, 10, 60);
        Rectangle player2 = new Rectangle(10, 250, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int playerTurn = 1;

        int playerSpeed = 4;
        int ballSpeed = 6; //speed variable that will always stay true for reseting speeds
        int ballXSpeed = 6;
        int ballYSpeed = -6;

        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool leftArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Lime);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen playerOutlinePen = new Pen(Color.White, 2);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move player 1
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            //move player 2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (rightArrowDown == true && player2.X < this.Width - player2.Height)
            {
                player2.X += playerSpeed;
            }

            //ball collision with top and bottom walls
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }
            else if (ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;
            }
            
            //ball collision with player
            if (playerTurn == 1)
            {
                if (player1.IntersectsWith(ball) && ballXSpeed < 0)
                {
                    ballXSpeed *= -1;
                    ball.X = player1.X + ball.Width;

                    playerTurn = 2;
                }
            }
            else
            {
                if (player2.IntersectsWith(ball) && ballXSpeed < 0)
                {
                    ballXSpeed *= -1;
                    ball.X = player2.X + ball.Width;

                    playerTurn = 1;
                }
            }

            //check for point scored
            if (ball.X < 0 && playerTurn == 1)
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 130;
                player1.X = 10;
                player2.Y = 250;
                player2.X = 10;


                ballXSpeed = ballSpeed;
            }
            else if (ball.X < 0 && playerTurn == 2)
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 130;
                player1.X = 10;
                player2.Y = 250;
                player2.X = 10;

                ballXSpeed = ballSpeed;
            }

            //check for game over
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";

                resetButton.Enabled = true;
                resetButton.Visible = true;
                exitButton.Enabled = true;
                exitButton.Visible = true;
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";

                resetButton.Enabled = true;
                resetButton.Visible = true;
                exitButton.Enabled = true;
                exitButton.Visible = true;
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //draw components
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);

            //outline active player
            if (playerTurn == 1)
            {
                e.Graphics.DrawRectangle(playerOutlinePen, player1);
            }
            else
            {
                e.Graphics.DrawRectangle(playerOutlinePen, player2);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            //reset positions and score
            ball.X = 295;
            ball.Y = 195;

            player1.Y = 130;
            player1.X = 10;
            player2.Y = 250;
            player2.X = 10;

            player1Score = 0;
            player2Score = 0;
            p1ScoreLabel.Text = $"{player1Score}";
            p2ScoreLabel.Text = $"{player2Score}";

            //restart gametimer and reset screen
            resetButton.Visible = false;
            resetButton.Enabled = false;
            exitButton.Visible = false;
            exitButton.Enabled = false;
            winLabel.Visible = false;
            gameTimer.Enabled = true;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
