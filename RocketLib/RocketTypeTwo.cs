using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace RocketLib
{
    class RocketTypeTwo : Rocket
    {
        Control Control = new Control();
        private decimal lCost = 10000;
        private string tRocket = "Дорогая ракета";
        private int weightToSpace = 450;

        public override decimal LaunchCost { get { return lCost; } }
        public override string TypeRocket { get { return tRocket; } }
        //public override int chanceCrush { get { return getChanceCrush(); }  }

        public RocketTypeTwo(string name, string path)
        {
            NameRocket = name;
            PathToImage = path;
        }

        public override int getChanceCrush()
        {
            Random random = new Random();

            int chance = random.Next(0, 1000);
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
            if (chance < 25)
            {
                for (int i = 0; i < ((chance * weightToSpace) / 100); i++)
                {
                    Thread.Sleep(10);
                    OnMove(this, pictureBox);
                    k = i;
                }
                this.OnCrushEvent(this);
                OnReMove(this, pictureBox, k);
            }
            else if (chance > 25)
            {
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
    }
}
