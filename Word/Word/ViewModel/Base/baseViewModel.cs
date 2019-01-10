using System;
using PropertyChanged;
using System.ComponentModel;



namespace Word
{
    [AddINotifyPropertyChangedInterface]
    class baseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    /// <summary>
    /// Call this to fire a property changed event
    /// </summary>
    /// <param name="name"></param>
    public void OnPropertyChanged(string name)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(name));
    }
    }
}
