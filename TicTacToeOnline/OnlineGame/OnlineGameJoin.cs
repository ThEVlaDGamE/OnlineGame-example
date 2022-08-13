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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OnlineGame
{
    public partial class OnlineGameJoin : Form
    {
        MongoClient client = new MongoClient(ConnectionString.Connection);
        IMongoDatabase database;

        BsonDocument filter = new BsonDocument("id", "head");
        BsonDocument filter2 = new BsonDocument("id", "body");
        BsonDocument filter3 = new BsonDocument("id", "game");

        List<string> collections;

        public OnlineGameJoin()
        {
            InitializeComponent();

            database = client.GetDatabase("Core");

            GetCollections();
        }

        async void GetCollections()
        {
            listBox1.Items.Clear(); 

            Text = "Обновление списка...";
            IAsyncCursor<string> collections_ = await database.ListCollectionNamesAsync();
            collections = collections_.ToList();

            for (int i = 0; i < collections.Count; i++)
            {
                if (collections[i] != "empty")
                {
                    IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collections[i]);

                    List<BsonDocument> head = await collection.Find(filter).ToListAsync();

                    foreach (var p in head)
                    {
                        if (p.GetValue("opponent").ToString() == "false")
                        {
                            listBox1.Items.Add(p.GetValue("name").ToString());
                        }
                        else
                        {
                            collections.RemoveAt(i);
                        }
                    }
                }
            }

            for (int i = 0; i < collections.Count; i++)
            {
                if (collections[i] == "empty")
                {
                    collections.RemoveAt(i);
                }
            }


            Text = "Список обновлён";
        }

        // Подключиться
        private async void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                if ((listBox1.SelectedIndex >= 0) && (listBox1.SelectedIndex < listBox1.Items.Count))
                {
                    IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collections[listBox1.SelectedIndex]);
                    List<BsonDocument> head = await collection.Find(filter).ToListAsync();

                    // Проверка пароля
                    if (head[0].GetValue("password") != "")
                    {
                        if (head[0].GetValue("password") != textBox1.Text)
                        {
                            MessageBox.Show("Пароль неверный!");
                            return;
                        }
                    }

                    Random random = new Random();
                    int id = random.Next(0, 9999999) +
                        random.Next(0, 9999999) +
                        random.Next(0, 9999999) +
                        random.Next(0, 9999999) +
                        random.Next(0, 9999999) +
                        random.Next(0, 9999999);

                    BsonDocument game = new BsonDocument {
                        { "id", head[0].GetValue("id")},
                        { "name", head[0].GetValue("name")},
                        { "password", head[0].GetValue("password") },
                        { "opponent", "true"}
                    };

                    List<BsonDocument> body = await collection.Find(filter2).ToListAsync();
                    BsonDocument game2 = new BsonDocument {
                        { "id", body[0].GetValue("id")},
                        { "player1", body[0].GetValue("player1")},
                        { "player2", id.ToString()}
                    };

                    if (random.Next(0, 2) == 0)
                    {
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

                        await collection.FindOneAndUpdateAsync(filter3, game3);
                    }

                    await collection.FindOneAndUpdateAsync(filter, game);
                    await collection.FindOneAndUpdateAsync(filter2, game2);

                    Game f_game = new Game(head[0].GetValue("name").ToString(), this, id);
                    try
                    {
                        f_game.ShowDialog();
                    }
                    catch { }
                }
            }
        }

        // Обновить
        private void button2_Click(object sender, EventArgs e)
        {
            GetCollections();
        }
    }
}
