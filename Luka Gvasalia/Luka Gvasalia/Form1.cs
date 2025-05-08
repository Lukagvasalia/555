using System;
using System.Drawing;
using System.Windows.Forms;

namespace Luka_Gvasalia
{
    public partial class Form1 : Form
    {
        private const int speed = 10;
        private bool isGameOver = false;
        private int countCoins = 0;
        private object CountCoins = 0;


        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            labelLose.Visible = false;
            btnRestart.Visible = false;
            labelCoins.ReadOnly = true;          
            labelCoins.TabStop = false;          
            labelCoins.Cursor = Cursors.Default; 
            labelCoins.BorderStyle = BorderStyle.None;
            labelCoins.Enabled = false;          

            labelWin.Visible = false; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            MoveBackground();
            MoveEnemies();
            CheckCollision();
            Coin.Top += speed;
        }

        private void MoveBackground()
        {
            int bgSpeed = 9;
            bg1.Top += bgSpeed;
            bg2.Top += bgSpeed;

            if (bg1.Top >= 650)
            {
                bg1.Top = 0;
                bg2.Top = -652;
            }
        }

        private void MoveEnemies()
        {
            int carSpeed = 7;
            enemy1.Top += carSpeed;
            enemy2.Top += carSpeed;

            if (enemy1.Top >= 650)
                ResetEnemy(enemy1, -130, 150, 300);

            if (Coin.Top >= 650)
            {
                Coin.Top = -50;
                Random rand = new Random();
                Coin.Left = rand.Next(170, 550);
            }

            if (enemy2.Top >= 650)
                ResetEnemy(enemy2, -400, 300, 560);
        }

        private void ResetEnemy(PictureBox enemy, int top, int minX, int maxX)
        {
            enemy.Top = top;
            Random rand = new Random();
            enemy.Left = rand.Next(minX, maxX);
        }

        private void CheckCollision()
        {
            if (player.Bounds.IntersectsWith(enemy1.Bounds) || player.Bounds.IntersectsWith(enemy2.Bounds))
            {
                EndGame();
            }
            if (player.Bounds.IntersectsWith(Coin.Bounds))
            {
                countCoins++;
                labelCoins.Text = "Coin: " + countCoins.ToString();

               
                if (countCoins >= 10)
                {
                    WinGame();  
                    return;     
                }

                
                Coin.Top = -50;
                Random rand = new Random();
                Coin.Left = rand.Next(170, 550);
            }
        }

        private void WinGame()
        {
            timer1.Enabled = false;  
            isGameOver = true;  

            
            enemy1.Visible = false;
            enemy2.Visible = false;
            Coin.Visible = false;

            
            labelWin.Visible = true;
        }

        private void EndGame()
        {
            timer1.Enabled = false;
            labelLose.Visible = true;
            btnRestart.Visible = true;
            isGameOver = true;
        }

        private void RestartGame()
        {
            player.Left = 300;
            player.Top = 500;
            ResetEnemy(enemy1, -130, 150, 300);
            ResetEnemy(enemy2, -400, 300, 560);
            labelLose.Visible = false;
            labelWin.Visible = false; 
            btnRestart.Visible = false;
            isGameOver = false;
            timer1.Enabled = true;
            CountCoins = 0;
            labelCoins.Text = "Coin: 0";
            Coin.Top = -500;

            
            enemy1.Visible = true;
            enemy2.Visible = true;
            Coin.Visible = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver) return;

            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && player.Left > 150)
                player.Left -= speed;
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && player.Right < 700)
                player.Left += speed;
            else if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && player.Top > 0)
                player.Top -= speed;
            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && player.Bottom < this.ClientSize.Height)
                player.Top += speed;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }



        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();


        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void coin_Click(object sender, EventArgs e)
        {

        }

        private void labelCoin_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelWin_Click(object sender, EventArgs e)
        {

        }
    }
}
