using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheThousandGame
{
    class ScoreBoardPanel : IUpdatable
    {
        Panel panel = new Panel();
        Label[] playerScores = new Label[4];
        int l, t, w, h;
        public Panel Create(int left, int top, int width, int height)
        {
            this.l = left;
            this.t = top;
            this.w = width;
            this.h = height;
            
            for (int i = 0; i < playerScores.Length; i++)
            {
                playerScores[i] = new Label();
                playerScores[i].BackColor = Color.LightGray;
                playerScores[i].ForeColor = Color.DarkSlateGray;
                playerScores[i].Left = (int)(i * 0.25 * this.w);
                playerScores[i].Top = 0;
                playerScores[i].Width = (int)(0.25 * this.w);
                playerScores[i].Height = this.h;
                playerScores[i].Font = new Font("consolas", 15);
                playerScores[i].TextAlign = ContentAlignment.MiddleCenter;
            }

            this.panel.Left = this.l;
            this.panel.Top = this.t;
            this.panel.Width = this.w;
            this.panel.Height = this.h;
            this.panel.BackColor = Color.Transparent;
            this.panel.Controls.AddRange(playerScores);
            return this.panel;
        }

        public void Update(object players)
        {
            Player[] _players = (Player[])players;
            for (int i = 0; i < playerScores.Length; i++)
            {
                playerScores[i].Text = _players[i].Name + "\n" + _players[i].Pts;
            }
        }
    }
}
