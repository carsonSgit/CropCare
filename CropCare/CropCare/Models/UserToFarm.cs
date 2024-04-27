using CropCare.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Models
{
    public class UserToFarm : IHasKey
    {
        public string Key { get; set; }
        public string UserId { get; set; }
        public string FarmId { get; set; }

        public UserToFarm(string userId, string farmId)
        {
            UserId = userId;
            FarmId = farmId;
        }
    }
}
