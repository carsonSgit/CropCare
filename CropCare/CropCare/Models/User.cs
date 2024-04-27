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
        public List<string> FarmKeys 
        {
            get
            {
                return farmKeys ??= new List<string>();
            }
            set
            {
                if (value != null)
                    farmKeys = value;
            } 
        }

        public User(string email, string name, bool isOwner, List<string> farmKeys)
        {
            Email = email;
            Name = name;
            IsOwner = isOwner;
            FarmKeys = farmKeys;
        }
    }
}