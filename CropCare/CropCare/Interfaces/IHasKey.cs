namespace CropCare.Interfaces
{
    /// <summary>
    /// Interface for objects that have a key.
    /// </summary>
    public interface IHasKey
    {
        /// <summary>
        /// The key of the object.
        /// </summary>
        string Key { get; set; }
    }
}