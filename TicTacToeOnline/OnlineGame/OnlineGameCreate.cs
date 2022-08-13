using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineGame
{
    public partial class OnlineGameCreate : Form
    {
        public OnlineGameCreate()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
            if (!textBox2.Enabled)
            {
                textBox2.Text = "";
            }
        }

        async void CreateGame()
        {
            // Создание
            string connectionString = ConnectionString.Connection;

            Random random = new Random();
            int id = random.Next(1, 999999) +
                random.Next(0, 999999) +
                random.Next(0, 999999) +
                random.Next(0, 999999) +
                random.Next(0, 999999) +
                random.Next(0, 999999);

            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("Core");
            await database.CreateCollectionAsync(textBox1.Text);

            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(textBox1.Text);
            BsonDocument game = new BsonDocument {
                        { "id", "head" },
                        { "name", textBox1.Text },
                        { "password", textBox2.Text },                        
                        { "opponent", "false"}
                    };
            await collection.InsertOneAsync(game);

            BsonDocument game2 = new BsonDocument {
                        { "id", "body" },
                        { "player1", id.ToString() },
                        { "player2", ""}
                    };
            await collection.InsertOneAsync(game2);

            BsonDocument game3 = new BsonDocument {
                        { "id", "game" },
                        { "turn", id.ToString() },
                        { "picture1", ""},
                        { "picture2", ""},
                        { "picture3", ""},
                        { "picture4", ""},
                        { "picture5", ""},
                        { "picture6", ""},
                        { "picture7", ""},
                        { "picture8", ""},
                        { "picture9", ""},
                    };
            await collection.InsertOneAsync(game3);

            MessageBox.Show("Игра успешно создана!");
            Game form_game = new Game(textBox1.Text, this, id);
            try
            {
                form_game.ShowDialog();
            }
            catch{}

        }

        // Создать
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 3)
            {
                if ((textBox2.Enabled == true))
                {
                    if (textBox2.Text.Length >= 3)
                    {
                        CreateGame();
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать минимум 3 символа!");
                    } 
                }
                else
                {
                    CreateGame();
                }
            }
            else
            {
                MessageBox.Show("Название должно содержать минимум 3 символа!");
            }
        }
    }
}
