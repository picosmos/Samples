using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _1a400e3b_fbc8_423f_9404_36ef4c746e8e.Annotations;

namespace _1a400e3b_fbc8_423f_9404_36ef4c746e8e
{
    public class HomeViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Node> nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return this.nodes; }
            set
            {
                if (value != this.Nodes)
                {
                    this.nodes = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        public HomeViewModel()
        {
            this.Nodes = new ObservableCollection<Node>();
            var node = new Node();
            node.Location = "Raum1";
            this.Nodes.Add(node);

            node = new Node();
            node.Location = "Raum2";
            this.Nodes.Add(node);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Node
    {
        public string Location { get; set; }
    }
}