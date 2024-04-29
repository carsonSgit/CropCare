using System.Collections.ObjectModel;

namespace CropCare.Interfaces
{
    /// <summary>
    /// Interface for a data store.
    /// </summary>
    /// <typeparam name="T">The type of items stored in the data store.</typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Gets the collection of items in the data store.
        /// </summary>
        public ObservableCollection<T> Items { get; }

        /// <summary>
        /// Adds an item to the data store asynchronously.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        Task<bool> AddItemAsync(T item);

        /// <summary>
        /// Retrieves items from the data store asynchronously.
        /// </summary>
        /// <param name="forceRefresh">A flag indicating whether to force a refresh of the data.</param>
        /// <returns>A collection of items.</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        /// <summary>
        /// Updates an item in the data store asynchronously.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        Task<bool> UpdateItemAsync(T item);

        /// <summary>
        /// Deletes an item from the data store asynchronously.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        Task<bool> DeleteItemAsync(T item);
    }
}