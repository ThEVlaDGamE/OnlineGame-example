using MongoDB.Bson;
using MongoDB.Driver;
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
    public partial class Game : Form
    {
        MongoClient client = new MongoClient(ConnectionString.Connection);

        public string collection_name;
        OnlineGameWaitUser onlineGameWaitUser;

        PictureBox[] pictureBoxes = new PictureBox[9];
        int id = 0;


        string[] items = new string[9];
        string id_turn = "";
        int number_turn = 0;

        string player1 = "";
        string player2 = "";

        void Construct(string collection_name, int id)
        {
            pictureBoxes[0] = pictureBox1;
            pictureBoxes[1] = pictureBox2;
            pictureBoxes[2] = pictureBox3;
            pictureBoxes[3] = pictureBox4;
            pictureBoxes[4] = pictureBox5;
            pictureBoxes[5] = pictureBox6;
            pictureBoxes[6] = pictureBox7;
            pictureBoxes[7] = pictureBox8;
            pictureBoxes[8] = pictureBox9;

            for (int i = 0; i < 9; i++)
            {
                items[i] = "";
            }

            Text = collection_name;
            this.collection_name = collection_name;
            this.id = id;
        }

        public Game(string collection_name, OnlineGameCreate online, int id)
        {
            InitializeComponent();

            Construct(collection_name, id);
            online.Close();

            Random random = new Random();
            id = random.Next(1, 99999999) + 
                random.Next(0, 99999999) + 
                random.Next(0, 99999999) + 
                random.Next(0, 99999999) + 
                random.Next(0, 99999999) + 
                random.Next(0, 99999999);


            timer1.Enabled = true;
        }
        public Game(string collection_name, OnlineGameJoin online, int id)
        {
            InitializeComponent();

            Construct(collection_name, id);
            online.Close();

            timer1.Enabled = true;
        }

        public void BreakGame()
        {
            MongoClient client = new MongoClient(ConnectionString.Connection);
            IMongoDatabase database = client.GetDatabase("Core");
            database.DropCollection(collection_name);
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            BreakGame();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            onlineGameWaitUser = new OnlineGameWaitUser(this);
            onlineGameWaitUser.ShowDialog();

            id_turn = "";
        }

        void CheckPictureBoxes()
        {
            for (int i = 0; i < 9; i++)
            {
                if ((items[i] == "0") && (pictureBoxes[i].Image != Properties.Resources.circle))
                {
                    pictureBoxes[i].Image = Properties.Resources.circle;
                }
                else if ((items[i] == "1") && (pictureBoxes[i].Image != Properties.Resources.cross))
                {
                    pictureBoxes[i].Image = Properties.Resources.cross;
                }
            }
        }

        async void DownloadStorage()
        {
            IMongoDatabase database = client.GetDatabase("Core");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collection_name);

            var filter = new BsonDocument("id", "game");
            List<BsonDocument> head = await collection.Find(filter).ToListAsync();

            var filter2 = new BsonDocument("id", "body");
            List<BsonDocument> body = await collection.Find(filter2).ToListAsync();

            foreach (var p in body)
            {
                if (p.GetValue("player1").ToInt32() == id)
                {
                    number_turn = 1;
                }
                else if (p.GetValue("player2").ToInt32() == id)
                {
                    number_turn = 0;
                }

                player1 = p.GetValue("player1").ToString();
                player2 = p.GetValue("player2").ToString();
            }

            foreach (var p in head)
            {
                id_turn = p.GetValue("turn").ToString();
                if (id_turn == id.ToString())
                {
                    label1.Text = "Ваш ход";
                }
                else
                {
                    label1.Text = "Ход соперника";
                }

                items[0] = p.GetValue("picture1").ToString();
                items[1] = p.GetValue("picture2").ToString();
                items[2] = p.GetValue("picture3").ToString();
                items[3] = p.GetValue("picture4").ToString();
                items[4] = p.GetValue("picture5").ToString();
                items[5] = p.GetValue("picture6").ToString();
                items[6] = p.GetValue("picture7").ToString();
                items[7] = p.GetValue("picture8").ToString();
                items[8] = p.GetValue("picture9").ToString();
            }

            CheckPictureBoxes();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (id_turn != id.ToString())
            {
                DownloadStorage();
            }

            if (
                (
                (items[0] == "0") &&
                (items[1] == "0") &&
                (items[2] == "0")
                ) ||
                (
                (items[3] == "0") &&
                (items[4] == "0") &&
                (items[5] == "0")
                ) ||
                (
                (items[6] == "0") &&
                (items[7] == "0") &&
                (items[8] == "0")
                ) ||
                (
                (items[0] == "0") &&
                (items[3] == "0") &&
                (items[6] == "0")
                ) ||
                (
                (items[1] == "0") &&
                (items[4] == "0") &&
                (items[7] == "0")
                ) ||
                (
                (items[2] == "0") &&
                (items[5] == "0") &&
                (items[8] == "0")
                ) ||
                (
                (items[0] == "0") &&
                (items[4] == "0") &&
                (items[8] == "0")
                ) ||
                (
                (items[2] == "0") &&
                (items[4] == "0") &&
                (items[6] == "0")
                )
               )
            {
                timer1.Enabled = false;
                if (number_turn == 0)
                {
                    MessageBox.Show("Вы победили!");
                }
                else
                {
                    MessageBox.Show("Вы проиграли!");
                }
                Close();
            }
            else
                if (
                (
                (items[0] == "1") &&
                (items[1] == "1") &&
                (items[2] == "1")
                ) ||
                (
                (items[3] == "1") &&
                (items[4] == "1") &&
                (items[5] == "1")
                ) ||
                (
                (items[6] == "1") &&
                (items[7] == "1") &&
                (items[8] == "1")
                ) ||
                (
                (items[0] == "1") &&
                (items[3] == "1") &&
                (items[6] == "1")
                ) ||
                (
                (items[1] == "1") &&
                (items[4] == "1") &&
                (items[7] == "1")
                ) ||
                (
                (items[2] == "1") &&
                (items[5] == "1") &&
                (items[8] == "1")
                ) ||
                (
                (items[0] == "1") &&
                (items[4] == "1") &&
                (items[8] == "1")
                ) ||
                (
                (items[2] == "1") &&
                (items[4] == "1") &&
                (items[6] == "1")
                )
               )
            {
                timer1.Enabled = false;
                if (number_turn == 1)
                {
                    MessageBox.Show("Вы победили!");
                }
                else
                {
                    MessageBox.Show("Вы проиграли!");
                }   
                Close();
            }
            else 
                if 
                (
                    (items[0] == "0" || items[0] == "1") &&
                    (items[1] == "0" || items[1] == "1") &&
                    (items[2] == "0" || items[2] == "1") &&
                    (items[3] == "0" || items[3] == "1") &&
                    (items[4] == "0" || items[4] == "1") &&
                    (items[5] == "0" || items[5] == "1") &&
                    (items[6] == "0" || items[6] == "1") &&
                    (items[7] == "0" || items[7] == "1") &&
                    (items[8] == "0" || items[8] == "1")
                )
            {
                timer1.Enabled = false;
                MessageBox.Show("Ничья!");
                Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            int index = Convert.ToInt32(pictureBox.Tag);

            if (items[index - 1] == "")
            {
                if (id.ToString() == id_turn)
                {
                    items[index - 1] = number_turn.ToString();
                    CheckPictureBoxes();

                    if (id_turn == player1)
                    {
                        id_turn = player2;
                    }
                    else if (id_turn == player2)
                    {
                        id_turn = player1;
                    }

                    BsonDocument filter = new BsonDocument("id", "game");

                    IMongoDatabase database = client.GetDatabase("Core");
                    IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collection_name);
                    List<BsonDocument> head = await collection.Find(filter).ToListAsync();

                    BsonDocument game = new BsonDocument {
                        { "id", head[0].GetValue("id") },
                        { "turn", id_turn },
                        { "picture1", items[0]},
                        { "picture2", items[1]},
                        { "picture3", items[2]},
                        { "picture4", items[3]},
                        { "picture5", items[4]},
                        { "picture6", items[5]},
                        { "picture7", items[6]},
                        { "picture8", items[7]},
                        { "picture9", items[8]},
                    };

                    await collection.FindOneAndUpdateAsync(filter, game);
                }
            }
        }
    }
}
