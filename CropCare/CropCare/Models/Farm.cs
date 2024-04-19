using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models
{
    public class Farm : INotifyPropertyChanged, IHasKey
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Key { get; set; }
        public string Name { get; set; }
        public float SoilMoisture { get; set; }
        public float WaterLevel { get; set; }
        public float Luminosity { get; set; }
        public bool Light { get; set; }
        public bool DoorLocked { get; set; }
        public bool DoorOpen { get; set; }
        public float Pitch { get; set; } 
        public float Roll { get; set; }
        public string Location { get; set; }
        public bool Vibration { get; set; }
        public float Noise { get; set; }
        public bool FanState { get; set; }
        public bool BuzzerState { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public Farm(string farmName)
        {
            Name = farmName;
        }
    }
}
