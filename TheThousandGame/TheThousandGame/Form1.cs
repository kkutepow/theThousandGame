using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheThousandGame
{
    public partial class Form1 : Form
    {
        Player player1 = new Player("Player1", Properties.Resources.Рубашка);
        Player player2 = new Player("Player2", Properties.Resources.Рубашка);
        Player player3 = new Player("Player3", Properties.Resources.Рубашка);
        Player player4 = new Player("Player4", Properties.Resources.Рубашка);
        PlayerPanel topPlayer;
        PlayerPanel midPlayer;
        PlayerPanel botPlayer;
        ScoreBoardPanel scoreBoard;
        TablePanel table;

        public Form1()
        {
            InitializeComponent();
            UpdateAll();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void UpdateAll()
        {
            this.Controls.Clear();
            topPlayer = new PlayerPanel();
            midPlayer = new PlayerPanel();
            botPlayer = new PlayerPanel();
            scoreBoard = new ScoreBoardPanel();
            table = new TablePanel();
            double margin = this.Height * 0.025;
            double height = this.Height * 0.25;
            double width = this.Width * 0.5;
            this.Controls.Add(topPlayer.Create((int)Math.Round(margin), (int)Math.Round(margin), (int)Math.Round(width), (int)Math.Round(height)));
            this.Controls.Add(midPlayer.Create((int)Math.Round(margin), (int)Math.Round(2 * margin + height), (int)Math.Round(width), (int)Math.Round(height)));
            this.Controls.Add(botPlayer.Create((int)Math.Round(margin), (int)Math.Round(3 * margin + 2 * height), (int)Math.Round(width), (int)Math.Round(height)));
            this.Controls.Add(scoreBoard.Create((int)Math.Round(this.Width * 0.65), (int)Math.Round(this.Height * 0.05), (int)Math.Round(this.Width * 0.3), (int)Math.Round(this.Height * 0.07)));
            this.Controls.Add(table.Create((int)Math.Round(this.Width * 0.6), (int)Math.Round(this.Height * 0.4), (int)Math.Round(this.Width * 0.3), (int)Math.Round(this.Height * 0.2)));
            topPlayer.Update(player1);
            midPlayer.Update(player2);
            botPlayer.Update(player3);
            scoreBoard.Update(new Player[4]{ player1, player2, player3, player4 });
            table.Update(null);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            UpdateAll();
        }
    }
}
