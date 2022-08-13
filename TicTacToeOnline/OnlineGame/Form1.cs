using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Size = new Size(0, 0);
            tableLayoutPanel3.Size = new Size(0, 0);
        }

        // Один игрок
        private void button1_Click(object sender, EventArgs e)
        {

        }

        // Два игрока
        private void button2_Click(object sender, EventArgs e)
        {
            /*
            OnlineGameSetts onlineGameSetts = new OnlineGameSetts();
            onlineGameSetts.ShowDialog();*/
            tableLayoutPanel1.Dock = DockStyle.None;
            tableLayoutPanel1.Size = new Size(0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
        }

        // Настройки
        private void button3_Click(object sender, EventArgs e)
        {

        }

        // Выход
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        //=========================================================================================

        // Сетевая игра
        private void button5_Click(object sender, EventArgs e)
        {
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel2.Size = new Size(0, 0);
            tableLayoutPanel2.Dock = DockStyle.None;
        }

        // Локальная игра
        private void button6_Click(object sender, EventArgs e)
        {

        }

        // Назад
        private void button7_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Size = new Size(0, 0);
            tableLayoutPanel2.Dock = DockStyle.None;
        }


        //=========================================================================================


        
        // Создать игру
        private void button10_Click(object sender, EventArgs e)
        {
            OnlineGameCreate onlineGameCreate = new OnlineGameCreate();
            onlineGameCreate.ShowDialog();
        }

        // Присоединиться к игре
        private void button9_Click(object sender, EventArgs e)
        {
            OnlineGameJoin onlineGameJoin = new OnlineGameJoin();
            onlineGameJoin.ShowDialog();
        }

        // Назад
        private void button8_Click(object sender, EventArgs e)
        {
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel3.Size = new Size(0, 0);
            tableLayoutPanel3.Dock = DockStyle.None;
        }
    }
}
