using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheThousandGame
{
    class CroupierPanel : IUpdatable
    {
        Panel panel = new Panel();
        PictureBox photo = new PictureBox();
        Label name = new Label();
        Label roundPts = new Label();
        int l, t, w, h;
        public Panel Create(int left, int top, int width, int height)
        {
            this.l = left;
            this.t = top;
            this.w = width;
            this.h = height;

            photoCustomize();
            nameCustomize();
            roundPtsCustomize();

            this.panel.Left = this.l;
            this.panel.Top = this.t;
            this.panel.Width = this.w;
            this.panel.Height = this.h;
            this.panel.BackColor = Color.Transparent;
            this.panel.Controls.Add(photo);
            this.panel.Controls.Add(name);
            this.panel.Controls.Add(roundPts);
            return this.panel;
        }

        private void roundPtsCustomize()
        {
            this.roundPts.Left = 0;
            this.roundPts.Top = (int)(h * 0.8);
            this.roundPts.Width = w;
            this.roundPts.Height = (int)(h * 0.2);
            this.roundPts.Font = new Font("consolas", 14);
            this.roundPts.ForeColor = Color.SteelBlue;
            this.roundPts.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void nameCustomize()
        {
            this.name.Left = 0;
            this.name.Top = (int)(h * 0.6);
            this.name.Width = w;
            this.name.Height = (int)(h * 0.2);
            this.name.Font = new Font("consolas", 12);
            this.name.ForeColor = Color.SteelBlue;
            this.name.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void photoCustomize()
        {
            this.photo = new PictureBox();
            this.photo.Left = 0;
            this.photo.Top = 0;
            this.photo.Width = w;
            this.photo.Height = (int)(h * 0.6);
            this.photo.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public void Update(object player)
        {
            var _player = (Player)player;
            this.name.Text = _player.Name;
            this.roundPts.Text = _player.RoundPts.ToString();
            this.photo.Image = _player.Photo;
        }
    }
}
