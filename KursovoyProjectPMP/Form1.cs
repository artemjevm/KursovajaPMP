using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RocketLib;
using System.Reflection;
using System.IO;
using System.Threading;

namespace KursovoyProjectPMP
{
    public partial class Form1 : Form
    {
        int W = 0;
        Assembly asm;
        IEnumerable<Type> list;
        List<Rocket> rockets = new List<Rocket>();
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<Astronaut> astronauts = new List<Astronaut>()
        {
            new Astronaut("Александров М.А.", "Пилот"),
            new Astronaut("Михайлов С.Г.", "Пилот"),
            new Astronaut("Назаров В.В.", "Пилот")
        };
        //RocketTypeOne rocket1;
        Image rocketImg;
        Bank bank;
        public string path = "C:\\Users\\Overgoodl\\source\\repos\\KursovoyProjectPMP\\KursovoyProjectPMP\\rocket.png";
        
        public Form1()
        {
            InitializeComponent();
            bank = new Bank();
            bank.Cash = 10000;
            ScoreBank.Text = bank.Cash.ToString();
        }

        private void crash(Rocket rocket)
        {
            Action action = () => {
                MessageBox.Show("Ракета взорвалась");
                bank.Cash -= rocket.getCompensation();
                ScoreBank.Text = bank.Cash.ToString();
                rocket.onCrushRocket -= crash;
            };

            Invoke(action);

        }

        private void succesLaunch(Rocket rocket) {
            Action action = () => {
                MessageBox.Show("Ракета вышла в космос");
                bank.Cash += rocket.LaunchCost * 2;
                ScoreBank.Text = bank.Cash.ToString();
                rocket.onSuccesfullLaunch -= crash;
            };

            Invoke(action); 
        }

        public void Move(Rocket rocket, PictureBox pictureBox)
        {
            Action action = () => { pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y - 1); };

            Invoke(action);
        }

        public void RefreshMove(Rocket rocket, PictureBox pictureBox, int i)
        {
            Action action = () => { pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y + i); };
            Invoke(action);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //rocket1 = new RocketTypeOne("Ракета 1", path);
            //rocketImg = new Bitmap(rocket1.PathToImage);
            //pictureBox1.Image = rocketImg;

            foreach (Astronaut astr in astronauts) {
                comboBox2.Items.Add(astr.FullName);
            }

            try
            {
                asm = Assembly.Load(File.ReadAllBytes("RocketLib.dll"));

                Type ourtype = asm.GetType("RocketLib.Rocket");

                list = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype));

                foreach (Type itm in list)
                {
                    comboBox1.Items.Add(itm.Name);
                }


            }
            catch { MessageBox.Show("Путь не найден или неправильный"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rockets.Count != 0 && pictureBoxes.Count != 0)
            {
                for (int i = 0; i < rockets.Count; i++)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(_work));
                    thread.Start(i);
                    //rockets[i].LaunchStart(pictureBoxes[i], bank);
                }

            }
            else return;
            
        }

        public void _work(object x) {
            rockets[(int)x].LaunchStart(pictureBoxes[(int)x], bank);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PictureBox picture = new PictureBox();
            Astronaut astronaut1 = null;
            Rocket rocket = null;
            string typeAssemblyQualifiedName = null;
            ConstructorInfo info = null;

            AssemblyName assemblyName = AssemblyName.GetAssemblyName("RocketLib.dll");
            
            if (assemblyName != null)
            {
                typeAssemblyQualifiedName = string.Join(", ", $"RocketLib.{comboBox1.SelectedItem}", assemblyName.FullName);
            }
            else return;
            
            Type type = Type.GetType(typeAssemblyQualifiedName);

            if (type != null) {
                info = type.GetConstructor(new[] { typeof(string), typeof(string) });
            }

            if (textBox2 == null || textBox2.Text == "")
            {
                var foo = info.Invoke(new object[] { textBox1.Text, path });
                rocket = (Rocket)foo;
                

            }
            else { var foo = info.Invoke(new object[] { textBox1.Text, textBox2.Text });
                rocket = (Rocket)foo;
            }
            
            foreach (Astronaut astronaut in astronauts) {
                if (astronaut.FullName == comboBox2.SelectedItem.ToString()) {
                    astronaut1 = astronaut;
                }
            }

            rocket.Astronaut = astronaut1;
            rocket.onCrushRocket += crash;
            rocket.onSuccesfullLaunch += succesLaunch;
            rocket.onMove += Move;
            rocket.onReMove += RefreshMove;
            
            picture.Width = 110;
            picture.Height = 240;
            rocketImg = new Bitmap(rocket.PathToImage);
            picture.Image = rocketImg;
            picture.Location = new Point(1228 - W, 477);
        
            rockets.Add(rocket);
            pictureBoxes.Add(picture);
           
            this.Controls.Add(picture);
            W += 130;


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = rockets;
        }
    }
}
