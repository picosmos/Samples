using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _080d9e05e17f43298e63b9aa00914f79
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private Boolean _admin;

        public bool Admin
        {
            get
            {
                return this._admin;
            }
            set
            {
                this._admin = value;
                this.RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] String propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Admin = true;

            dgPerson.ItemsSource = new Item[]
                                      {
                                          new Item(){ Vorn="Max",Name="Mustermann",},
                                          new Item(){ Vorn="Tom",Name="Ich",},
                                      };
        }
    }

    public class Item
    {
        public string Vorn { get; set; }
        public string Name { get; set; }
    }
}
