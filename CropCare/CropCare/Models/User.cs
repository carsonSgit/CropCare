﻿using CropCare.Interfaces;
using System.ComponentModel;
using Newtonsoft.Json;

namespace CropCare.Models
{

    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a user in the system.
    public class User : INotifyPropertyChanged, IHasKey
    {
        private List<string> farmKeys;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the key of the user.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is an owner.
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// Gets or sets the keys of the farms associated with the user.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether the user is assigned.
        /// </summary>
        [JsonIgnore]
        public bool IsAssigned { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with the specified email, name, and ownership status.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="name">The name of the user.</param>
        /// <param name="isOwner">A value indicating whether the user is an owner.</param>
        public User(string email, string name, bool isOwner)
        {
            Email = email;
            Name = name;
            IsOwner = isOwner;
        }
    }
}