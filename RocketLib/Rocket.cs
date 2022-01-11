using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RocketLib
{
    public abstract class Rocket : IInsuranceCompany
    {
        public delegate void MethodContainer(Rocket rocket);
        public delegate void Meth2(Rocket roket, PictureBox pictureBox);
        public delegate void Meth3(Rocket roket, PictureBox pictureBox, int k);
        public Astronaut Astronaut { get; set; }
        public string NameAstronaut { get { return Astronaut.FullName; } set { NameAstronaut = value; } }
        public string NameRocket { get; set; }
        public string PathToImage { get; set; }
        public abstract decimal LaunchCost { get;  }
        public abstract string TypeRocket { get;  }

        public abstract void LaunchStart(PictureBox pictureBox, Bank bank);

        public abstract decimal getCompensation();

        public abstract int getChanceCrush();

        public event MethodContainer onCrushRocket;
        public event MethodContainer onSuccesfullLaunch;
        public event Meth2 onMove;
        public event Meth3 onReMove;

        public void InvokeEvent()
        {
            onCrushRocket.Invoke(this);
            onSuccesfullLaunch.Invoke(this);
        }

        public void OnCrushEvent(Rocket rocket) {
            onCrushRocket(rocket);
        }

        public void OnSuccessfullLaunch(Rocket rocket) {
            onSuccesfullLaunch(rocket);
        }

        public void OnMove(Rocket roket, PictureBox pictureBox) {
            onMove(roket, pictureBox);
        }

        public void OnReMove(Rocket roket, PictureBox pictureBox, int k) {
            onReMove(roket, pictureBox, k);
        }

    }
}
