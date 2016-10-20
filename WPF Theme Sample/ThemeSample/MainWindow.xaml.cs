using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ThemeSample.Annotations;

namespace ThemeSample
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private ResourceDictionary _selectedTheme;

        public MainWindow()
        {
            this.Themes = new ObservableCollection<KeyValuePair<string, ResourceDictionary>>()
            {
                new KeyValuePair<string, ResourceDictionary>("Light", new ResourceDictionary() {Source = new Uri("/Themes/Light.xaml", UriKind.RelativeOrAbsolute)}),
                new KeyValuePair<string, ResourceDictionary>("Dark", new ResourceDictionary() {Source = new Uri("/Themes/Dark.xaml", UriKind.RelativeOrAbsolute)}),
                new KeyValuePair<string, ResourceDictionary>("Green", new ResourceDictionary() {Source = new Uri("/Themes/Green.xaml", UriKind.RelativeOrAbsolute)}),
            };
            this.SelectedTheme = this.Themes.Cast<KeyValuePair<string, ResourceDictionary>?>().FirstOrDefault()?.Value;
            this.InitializeComponent();
        }

        public ObservableCollection<KeyValuePair<string, ResourceDictionary>> Themes { get; }

        public ResourceDictionary SelectedTheme
        {
            get { return this._selectedTheme; }
            set
            {
                this.Resources.MergedDictionaries.Remove(this._selectedTheme);
                this._selectedTheme = value;
                this.Resources.MergedDictionaries.Add(this._selectedTheme);

                this.RaisePropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
