using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheThousandGame
{
    public partial class Form1
    {
        public void ee()
        {
            Pack pack = new Pack(true);
            int left = 20;
            int top = 20;
            foreach (Card card in pack.Cards)
            {
                card.Hide();
                this.Controls.Add(this.getCardPicture(card, left, top, 100));
                left += 120;
                if (left > 1400)
                {
                    left = 20;
                top += 230;

                }
            }
        }
        private PictureBox getCardPicture(Card card, int left, int top, int w)
        {
            PictureBox pic = new PictureBox();
            pic.Left = left;
            pic.Top = top;
            pic.Width = w;
            pic.SizeMode = PictureBoxSizeMode.AutoSize;
            pic.Image = card.Picture;
            return pic;
        }
    }
}
