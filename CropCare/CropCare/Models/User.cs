using CropCare.Interfaces;
using System.ComponentModel;

namespace CropCare.Models
{
    public class User : INotifyPropertyChanged, IHasKey
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> farmKeys;

        public string Key { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsOwner { get; set; }

        public User(string email, string name, bool isOwner)
        {
            Email = email;
            Name = name;
            IsOwner = isOwner;
        }
    }
}