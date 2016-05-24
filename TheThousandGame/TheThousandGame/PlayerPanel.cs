using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheThousandGame
{
    class PlayerPanel : IUpdatable
    {
        Panel panel = new Panel();
        Label name = new Label();
        Label pts = new Label();
        PictureBox photo = new PictureBox();
        PictureBox[] cards = new PictureBox[8];
        int l, t, w, h;
        public Panel Create(int left, int top, int width, int height)
        {
            this.l = left;
            this.t = top;
            this.w = width;
            this.h = height;

            photoCustomize();
            nameCustomize();
            cardsCustomize();
            ptsCustomize();

            this.panel.Left = this.l;
            this.panel.Top = this.t;
            this.panel.Width = this.w;
            this.panel.Height = this.h;
            this.panel.BackColor = Color.LightPink;
            this.panel.Controls.AddRange(cards);
            this.panel.Controls.Add(name);
            this.panel.Controls.Add(photo);
            this.panel.Controls.Add(pts);
            return this.panel;
        }
        public void Update(object player)
        {
            var _player = (Player)player;
            this.name.Text = _player.Name;
            this.pts.Text = _player.RoundPts.ToString();
            this.photo.Image = _player.Photo;
        }

        #region ***components customize***
        private void cardsCustomize()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                this.cards[i] = new PictureBox();
                this.cards[i].Left = (int)(w * 0.35 + i * w * 0.05);
                this.cards[i].Top = (int)(h * 0.25);
                this.cards[i].Width = (int)(w * 0.1);
                this.cards[i].Height = (int)(h * 0.5);
                this.cards[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cards[i].Image = Properties.Resources.Рубашка;
            }
        }
        private void nameCustomize()
        {
            this.name.Left = (int)(w * 0.02);
            this.name.Top = (int)(h * 0.8);
            this.name.Width = (int)(w * 0.2);
            this.name.Height = (int)(h * 0.1);
            this.name.Font = new Font("consolas", 16);
            this.name.ForeColor = Color.SteelBlue;
            this.name.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void photoCustomize()
        {
            this.photo.Left = (int)(w * 0.02);
            this.photo.Top = (int)(h * 0.1);
            this.photo.Width = (int)(w * 0.2);
            this.photo.Height = (int)(h * 0.65);
            this.photo.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void ptsCustomize()
        {
            this.pts.Left = (int)(w * 0.75);
            this.pts.Top = (int)(h * 0.0);
            this.pts.Width = (int)(w * 0.25);
            this.pts.Height = (int)(h * 1.0);
            this.pts.Font = new Font("consolas", 19);
            this.pts.ForeColor = Color.SteelBlue;
            this.pts.TextAlign = ContentAlignment.MiddleCenter;
        }
        #endregion
    }
}
