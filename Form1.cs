using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOGame.Properties;

namespace XOGame
{

    enum enPlayer
    {
        Player1,
        Player2
    }
    enum enWinner 
    {
        Player1,
        Player2,
        Draw,
        InProgress
    }
    struct stGameStatus 
    {
        public enWinner Winner;
        public bool GameOver;
        public short PlayCount;

    }
    
    public partial class Form1 : Form
    {
        enPlayer playerTurn = enPlayer.Player1;
        stGameStatus GameStatus;
        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);

            Pen whitePen = new Pen(white);
            whitePen.Width = 10;

            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(whitePen, 350, 200, 650, 200);
            e.Graphics.DrawLine(whitePen, 350, 300, 650, 300);

            e.Graphics.DrawLine(whitePen, 450, 100, 450, 400);
            e.Graphics.DrawLine(whitePen, 550, 100, 550, 400);

        }

        private void EndGame()
        {
            lblTurn.Text = "Game Over";
            GameStatus.GameOver = true;

            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;

                case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;

                case enWinner.Draw:
                    lblWinner.Text = "draw";
                    break;
            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool CheckValues(Button btn1,Button btn2,Button btn3)
        {

            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn2.Tag.ToString()  == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if(btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    EndGame();
                    return true;
                }
                
            }

            GameStatus.GameOver = false;
            return false;

        }

        private void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;
        }

        private void changeImage(Button btn)
        {
            if (GameStatus.GameOver)
                MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                if (btn.Tag.ToString() == "?")
                {
                    switch (playerTurn)
                    {
                        case enPlayer.Player1:
                            btn.Image = Resources.X;
                            playerTurn = enPlayer.Player2;
                            lblTurn.Text = "Player 2";
                            btn.Tag = 'X';
                            GameStatus.PlayCount++;
                            CheckWinner();
                            break;

                        case enPlayer.Player2:
                            btn.Image = Resources.O;
                            playerTurn = enPlayer.Player1;
                            lblTurn.Text = "Player 1";
                            btn.Tag = 'O';
                            GameStatus.PlayCount++;
                            CheckWinner();
                            break;
                    }
                }
                else
                    MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (GameStatus.PlayCount == 9 && !GameStatus.GameOver)
                {

                    GameStatus.Winner = enWinner.Draw;
                    EndGame();
                }
            }
           
        }

        private void ResetButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
        }

        private void RestartGame()
        {
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            playerTurn = enPlayer.Player1;
            lblTurn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.Winner = enWinner.InProgress;
            GameStatus.GameOver = false;
            lblWinner.Text = "In Progress";

        }

        private void button_Click(object sender, EventArgs e)
        {
            changeImage((Button) sender);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
