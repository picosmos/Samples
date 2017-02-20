using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Koopakiller.Apps.Snake.Portable;

namespace Koopakiller.Apps.Snake.Windows.Forms
{
    public partial class GameForm : Form, IGameDisplay
    {
        public GameForm()
        {
            this.InitializeComponent();
        }

        private Int32 widthInBlocks = 20;
        private Int32 heightInBlocks = 10;
        private Int32 pixelPerBlock = 25;

        private void GameFormLoad(Object sender, EventArgs e)
        {
            this.ClientSize = new Size(this.widthInBlocks * this.pixelPerBlock, this.heightInBlocks * this.pixelPerBlock + this.MainMenu.Height);
        }

        private void GamePanelPaint(Object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 32, 32, 32)), 0, 0, this.Width, this.Height);
            for (var col = 0; col < this.widthInBlocks; ++col)
            {
                for (var row = 0; row < this.heightInBlocks; ++row)
                {
                    this.DrawEmptyBlock(e.Graphics, col, row);
                }
            }
        }

        void DrawEmptyBlock(Graphics g, Int32 col, Int32 row)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 32, 32, 32)), col * this.pixelPerBlock, row * this.pixelPerBlock, (col + 1) * this.pixelPerBlock, (row + 1) * this.pixelPerBlock);
            g.DrawLine(Pens.Teal, col * this.pixelPerBlock, row * this.pixelPerBlock, (col + 1) * this.pixelPerBlock, row * this.pixelPerBlock);
            g.DrawLine(Pens.Teal, col * this.pixelPerBlock, row * this.pixelPerBlock, col * this.pixelPerBlock, (row + 1) * this.pixelPerBlock);
        }

        public void DrawSnake(Portable.Snake snake)
        {
            throw new NotImplementedException();
        }

        public void RemoveSnake(Portable.Snake snake)
        {
            throw new NotImplementedException();
        }

        public void DrawItem(Position position)
        {
            throw new NotImplementedException();
        }

        public void ResetPosition(Position position)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
