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
    public partial class OnlineGameWaitUser : Form
    {
        MongoClient client = new MongoClient(ConnectionString.Connection);
        bool is_find = false;
        

        Game game;
        public OnlineGameWaitUser(Game game)
        {
            InitializeComponent();

            this.game = game;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == true)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
            else if (pictureBox2.Visible == true)
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
            }
            else if (pictureBox3.Visible == true)
            {
                pictureBox3.Visible = false;
                pictureBox1.Visible = true;
            }

            IMongoDatabase database = client.GetDatabase("Core");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(game.collection_name);

            var filter2 = new BsonDocument("id", "body");
            List<BsonDocument> body = await collection.Find(filter2).ToListAsync();

            foreach (var p in body)
            {
                if (p.GetValue("player2").ToString() != "")
                {
                    is_find = true;
                    Close();
                }
            }
        }

        private void OnlineGameWaitUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!is_find)
            {
                game.Close();
            }
        }
    }
}
