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
        }

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

        // Выход
        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
