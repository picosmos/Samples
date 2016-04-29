using System;
using System.Windows.Forms;

namespace Move_Control_with_Timer__WinForms_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }

        private HorizontalDirection _horizontalVerticalDirection;
        private VerticalDirection _verticalVerticalDirection;
        private HorizontalDirection _diagonalHorizontalVerticalDirection;
        private VerticalDirection _diagonalVerticalVerticalDirection;

        private void buttonHorizontal_Click(object sender, EventArgs e)
        {
            this.timerHorizontal.Enabled = !this.timerHorizontal.Enabled;
        }

        private void buttonVertical_Click(object sender, EventArgs e)
        {
            this.timerVertical.Enabled = !this.timerVertical.Enabled;
        }

        private void buttonDiagonal_Click(object sender, EventArgs e)
        {
            this.timerDiagonal.Enabled = !this.timerDiagonal.Enabled;
        }

        private void timerHorizontal_Tick(object sender, EventArgs e)
        {
            if (this._horizontalVerticalDirection == HorizontalDirection.Left)
            {
                if (this.buttonHorizontal.Left <= 0)
                {
                    this._horizontalVerticalDirection = HorizontalDirection.Right;
                    ++this.buttonHorizontal.Left;
                }
                else
                {
                    --this.buttonHorizontal.Left;
                }
            }
            else
            {
                if (this.buttonHorizontal.Right >= this.ClientSize.Width)
                {
                    this._horizontalVerticalDirection = HorizontalDirection.Left;
                    --this.buttonHorizontal.Left;
                }
                else
                {
                    ++this.buttonHorizontal.Left;
                }
            }
        }

        private void timerVertical_Tick(object sender, EventArgs e)
        {
            if (this._verticalVerticalDirection == VerticalDirection.Top)
            {
                if (this.buttonVertical.Top <= 0)
                {
                    this._verticalVerticalDirection = VerticalDirection.Bottom;
                    ++this.buttonVertical.Top;
                }
                else
                {
                    --this.buttonVertical.Top;
                }
            }
            else
            {
                if (this.buttonVertical.Bottom >= this.ClientSize.Height)
                {
                    this._verticalVerticalDirection = VerticalDirection.Top;
                    --this.buttonVertical.Top;
                }
                else
                {
                    ++this.buttonVertical.Top;
                }
            }
        }

        private void timerDiagonal_Tick(object sender, EventArgs e)
        {
            if (this._diagonalHorizontalVerticalDirection == HorizontalDirection.Left)
            {
                if (this.buttonDiagonal.Left <= 0)
                {
                    this._diagonalHorizontalVerticalDirection = HorizontalDirection.Right;
                    ++this.buttonDiagonal.Left;
                }
                else
                {
                    --this.buttonDiagonal.Left;
                }
            }
            else
            {
                if (this.buttonDiagonal.Right >= this.ClientSize.Width)
                {
                    this._diagonalHorizontalVerticalDirection = HorizontalDirection.Left;
                    --this.buttonDiagonal.Left;
                }
                else
                {
                    ++this.buttonDiagonal.Left;
                }
            }

            if (this._diagonalVerticalVerticalDirection == VerticalDirection.Top)
            {
                if (this.buttonDiagonal.Top <= 0)
                {
                    this._diagonalVerticalVerticalDirection = VerticalDirection.Bottom;
                    ++this.buttonDiagonal.Top;
                }
                else
                {
                    --this.buttonDiagonal.Top;
                }
            }
            else
            {
                if (this.buttonDiagonal.Bottom >= this.ClientSize.Height)
                {
                    this._diagonalVerticalVerticalDirection = VerticalDirection.Top;
                    --this.buttonDiagonal.Top;
                }
                else
                {
                    ++this.buttonDiagonal.Top;
                }
            }
        }
    }

    public enum VerticalDirection
    {
        Bottom,
        Top,
    }

    public enum HorizontalDirection
    {
        Left,
        Right,
    }
}
