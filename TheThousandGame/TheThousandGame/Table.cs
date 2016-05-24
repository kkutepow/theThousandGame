using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheThousandGame
{
    class TablePanel : IUpdatable
    {
        Panel panel = new Panel();
        PictureBox[] cards = new PictureBox[3];
        int l, t, w, h;
        public Panel Create(int left, int top, int width, int height)
        {
            this.l = left;
            this.t = top;
            this.w = width;
            this.h = height;

            for (int i = 0; i < cards.Length; i++)
            {
                this.cards[i] = new PictureBox();
                this.cards[i].Left = (int)(i * w * 0.35);
                this.cards[i].Top = 0;
                this.cards[i].Width = (int)(w * 0.3);
                this.cards[i].Height = h;
                this.cards[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cards[i].Image = Properties.Resources.Рубашка;
            }

            this.panel.Left = this.l;
            this.panel.Top = this.t;
            this.panel.Width = this.w;
            this.panel.Height = this.h;
            this.panel.BackColor = Color.Transparent;
            this.panel.Controls.AddRange(cards);
            return this.panel;
        }

        public void Update(object obj)
        {
        }
    }
}
