using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models
{
    public class User : INotifyPropertyChanged, IHasKey
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Key { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsOwner { get; set; }
        public List<string> FarmKeys { get; set; }

        public User(string email, string name, bool isOwner)
        {
            Email = email;
            Name = name;
            IsOwner = isOwner;
        }

    }
}