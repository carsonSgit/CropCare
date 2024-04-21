using CropCare.Interfaces;
using CropCare.Models;
using CropCare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.DataRepos
{
    public class CropCareRepo
    {
        private DatabaseService<User> usersDb;
        public DatabaseService<User> UsersDb
        {
            get
            {
                return usersDb ??= new DatabaseService<User>(AuthService.UserCreds.User, nameof(User), App.Settings.FireBaseDatabaseURL);
            }
        }
        private DatabaseService<Farm> farmsDb;
        public DatabaseService<Farm> FarmsDb
        {
            get
            {
                return farmsDb ??= new DatabaseService<Farm>(AuthService.UserCreds.User, nameof(Farm), App.Settings.FireBaseDatabaseURL);
            }
        }
        private IDeviceController deviceController;
        public IDeviceController DeviceController
        {
            get
            {
                return deviceController ??= new MockDeviceController();
            }
        }
    }
}
