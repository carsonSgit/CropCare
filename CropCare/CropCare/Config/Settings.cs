using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiFitness.Config
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Contains essentail connection strings for accessing firebase and IOT Hub
    public class Settings
    {
        /// <summary>
        /// API Key for accessing firebase
        /// </summary>
        public string FireBaseApiKey { get; set; }

        /// <summary>
        /// Firebase Authorization Domain
        /// </summary>
        public string FirebaseAuthorizedDomain { get; set; }

        /// <summary>
        /// Firebase Database URL
        /// </summary>
        public string FireBaseDatabaseURL { get; set; }

        /// <summary>
        /// The IOT Hub Connection string
        /// </summary>
        public string IOTHubConnectionString { get; set; }

        public string EventHubConnectionString { get; set; }
    }
}
