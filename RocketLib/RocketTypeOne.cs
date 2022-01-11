using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Timer = System.Windows.Forms.Timer;

namespace RocketLib
{
    public class RocketTypeOne : Rocket
    {
        Control Control = new Control();
        private decimal lCost = 1000;
        private string tRocket = "Дешевая ракета";
        private int weightToSpace = 450;

        public override decimal LaunchCost { get { return lCost; }  }
        public override string TypeRocket { get { return tRocket; }  }
        //public override int chanceCrush { get { return getChanceCrush(); }  }

        public RocketTypeOne(string name, string path) {
            NameRocket = name;
            PathToImage = path;
        }

        public override int getChanceCrush()
        {
            Random random = new Random();
            
            int chance = random.Next(0, 100);
            return chance;
        }

        public override decimal getCompensation()
        {
            return lCost / 2;
        }


        public override void LaunchStart(PictureBox pictureBox, Bank bank)
        {
            int k = 0;
            bank.Cash -= LaunchCost;
            int chance = getChanceCrush();
            if (chance < 25) {
                for (int i = 0; i < ((chance * weightToSpace) / 100); i++) {
                    Thread.Sleep(10);
                    OnMove(this, pictureBox);
                    k = i;
                }
                this.OnCrushEvent(this);
                OnReMove(this, pictureBox, k);
            } else if (chance > 25){
                for (int i = 0; i < weightToSpace; i++)
                {
                    Thread.Sleep(10);
                    OnMove(this, pictureBox);
                    k = i;
                }
                this.OnSuccessfullLaunch(this);
                OnReMove(this, pictureBox, k);
            }
        }




        //public void Move(PictureBox pictureBox)
        //{
        //    Action action = () => { pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y - 1); };

        //    action.Invoke();
        //}

        //public void refreshMove(PictureBox pictureBox, int i)
        //{
        //    Action action = () => { pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y + i); };
        //    action.Invoke();
        //}

    }
}
