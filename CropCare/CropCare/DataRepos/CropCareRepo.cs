using CropCare.Interfaces;
using CropCare.Models;
using CropCare.Services;

namespace CropCare.DataRepos
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Repository for accessing and managing CropCare data.
    public class CropCareRepo
    {
        private DatabaseService<User> usersDb;

        /// <summary>
        /// Database service for users.
        /// </summary>
        public DatabaseService<User> UsersDb
        {
            get
            {
                return usersDb ??= new DatabaseService<User>(AuthService.UserCreds.User, nameof(User), App.Settings.FireBaseDatabaseURL);
            }
        }

        private DatabaseService<Farm> farmsDb;

        /// <summary>
        /// Database service for farms.
        /// </summary>
        public DatabaseService<Farm> FarmsDb
        {
            get
            {
                return farmsDb ??= new DatabaseService<Farm>(AuthService.UserCreds.User, nameof(Farm), App.Settings.FireBaseDatabaseURL);
            }
        }

        private DatabaseService<UserToFarm> userToFarmDb;

        /// <summary>
        /// Database service for user-to-farm associations.
        /// </summary>
        public DatabaseService<UserToFarm> UserToFarmDb
        {
            get
            {
                return userToFarmDb ??= new DatabaseService<UserToFarm>(AuthService.UserCreds.User, nameof(UserToFarm), App.Settings.FireBaseDatabaseURL);
            }
        }
    }
}