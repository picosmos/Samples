using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_AutoSuggestBox_set_SelectionStart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.DataContext = this;
            this.InitializeComponent();
        }

        private TextBox _textBox;
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Get the type of the control
            var type = typeof(AutoSuggestBox);

            //Get Method from Type
            var method = type.GetMethod("GetTemplateChild", BindingFlags.Instance | BindingFlags.NonPublic);

            //Call method of the object "Asb" and pass parameter "TextBox" (name of the control to obtain)      
            this._textBox = (TextBox)method.Invoke(this.Asb, new object[] { "TextBox" });

            this._textBox.SelectionChanged += this._textBox_SelectionChanged;
        }

        private void _textBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            this.Tb.Text = "SelectionStart = " + this._textBox.SelectionStart;
        }

        private void OnSetSelectionStart(object sender, RoutedEventArgs e)
        {
            this._textBox.SelectionStart = 2;
            this._textBox.Focus(FocusState.Pointer);
        }
    }
}
