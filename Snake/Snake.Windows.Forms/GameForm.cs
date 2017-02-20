using System;
using System.Drawing;
using System.Windows.Forms;
using Koopakiller.Apps.Snake.Portable;
using System.Collections.Generic;
using System.Linq;

namespace Koopakiller.Apps.Snake.Windows.Forms
{
    public partial class GameForm : Form, IGameDisplay
    {
        public GameForm()
        {
            this.game = new Game(20, 10, 2, this);
            this.InitializeComponent();
        }

        private Int32 widthInBlocks = 20;
        private Int32 heightInBlocks = 10;
        private Int32 pixelPerBlock = 25;

        private readonly Game game;

        private readonly Dictionary<Int32, Brush> snakeBrushes = new Dictionary<Int32, Brush>()
        {
            [0] = new SolidBrush(Color.Green),
            [1] = new SolidBrush(Color.Yellow),
        };

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

            foreach (var snake in this.game.Snakes)
            {
                foreach (var position in snake.Positions)
                {
                    this.DrawSnakeBlock(e.Graphics, position.X, position.Y, this.snakeBrushes[snake.Id]);
                }
            }

            foreach (var position in this.game.Items)
            {
                this.DrawItemBlock(e.Graphics, position.X, position.Y);
            }
        }

        void DrawEmptyBlock(Graphics g, Int32 col, Int32 row)
        {
            this.DrawBlock(g, col, row, new SolidBrush(Color.FromArgb(255, 32, 32, 32)));
        }

        void DrawSnakeBlock(Graphics g, Int32 col, Int32 row, Brush brush)
        {
            this.DrawBlock(g, col, row, brush);
        }

        void DrawItemBlock(Graphics g, Int32 col, Int32 row)
        {
            this.DrawBlock(g, col, row, new SolidBrush(Color.Fuchsia));
        }

        void DrawBlock(Graphics g, Int32 col, Int32 row, Brush brush)
        {
            g.FillRectangle(brush, col * this.pixelPerBlock, row * this.pixelPerBlock, this.pixelPerBlock, this.pixelPerBlock);
            g.DrawLine(Pens.Teal, col * this.pixelPerBlock, row * this.pixelPerBlock, (col + 1) * this.pixelPerBlock, row * this.pixelPerBlock);
            g.DrawLine(Pens.Teal, col * this.pixelPerBlock, row * this.pixelPerBlock, col * this.pixelPerBlock, (row + 1) * this.pixelPerBlock);
        }

        public void DrawSnake(Portable.Snake snake)
        {
            this.GamePanel.Invalidate();
        }

        public void RemoveSnake(Portable.Snake snake)
        {
            this.GamePanel.Invalidate();
        }

        public void DrawItem(Position position)
        {
            this.GamePanel.Invalidate();
        }

        public void ResetPosition(Position position)
        {
            this.GamePanel.Invalidate();
        }

        public void Reset()
        {
            this.GamePanel.Invalidate();
        }

        private void nextIterationToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            this.game.MoveNext();
        }

        private void addItemToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            this.game.AddItem();
        }

        private void ChangePlayerDirectionClick(Object sender, EventArgs e)
        {
            var tsmi = (ToolStripMenuItem) sender;
            var tagParts = tsmi.Tag.ToString().Split('_');
            var snakeId = Int32.Parse(tagParts[0]) - 1;
            var direction = (Direction)Enum.Parse(typeof(Direction), tagParts[1]);
            this.game.Snakes.FirstOrDefault(snake => snake.Id == snakeId)?.TrySetDirection(direction);
        }
    }
}
