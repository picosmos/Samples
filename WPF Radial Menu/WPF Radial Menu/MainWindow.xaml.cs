namespace WPF_Radial_Menu
{
    using System;

    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void RadialMenu_OnClick(Object sender, RadialMenuField e)
        {
            this.OutputBox.Text = e + Environment.NewLine + this.OutputBox.Text;
        }
    }
}
