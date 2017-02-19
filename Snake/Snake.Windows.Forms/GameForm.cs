using System;
using System.Drawing;
using System.Windows.Forms;

namespace Koopakiller.Apps.Snake.Windows.Forms
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            this.InitializeComponent();
        }

        private Int32 widthInBlocks = 20;
        private Int32 heightInBlocks = 10;
        private Int32 pixelPerBlock = 25;

        private void GameForm_Load(Object sender, EventArgs e)
        {
            this.Width = this.widthInBlocks * this.pixelPerBlock;
            this.Height = this.heightInBlocks * this.pixelPerBlock;
        }

        private void GameForm_Paint(Object sender, PaintEventArgs e)
        {
            for (var col = 0; col < this.widthInBlocks; ++col)
            {
                e.Graphics.DrawLine(Pens.Teal, col * this.pixelPerBlock, 0, col * this.pixelPerBlock, this.Height);
            }
            for (var row = 0; row < this.heightInBlocks; ++row)
            {
                e.Graphics.DrawLine(Pens.Teal, 0, row * this.pixelPerBlock, this.Width, row * this.pixelPerBlock);
            }
        }
    }
}
