using CropCare.Interfaces;

namespace CropCare.Models
{
    /// <summary>
    /// Represents the association between a user and a farm.
    /// </summary>
    public class UserToFarm : IHasKey
    {
        /// <summary>
        /// Gets or sets the key of the association.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the farm.
        /// </summary>
        public string FarmId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserToFarm"/> class with the specified user ID and farm ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="farmId">The ID of the farm.</param>
        public UserToFarm(string userId, string farmId)
        {
            UserId = userId;
            FarmId = farmId;
        }
    }
}
