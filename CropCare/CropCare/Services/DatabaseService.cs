using CropCare.Interfaces;
using Firebase.Database;
using Firebase.Database.Offline;
using System.Collections.ObjectModel;

namespace CropCare.Services
{
    /// <summary>
    /// Service for interacting with Firebase Realtime Database.
    /// </summary>
    /// <typeparam name="T">The type of data to be stored/retrieved.</typeparam>
    public class DatabaseService<T> : IDataStore<T> where T : class, IHasKey
    {
        private ObservableCollection<T> _items;

        /// <summary>
        /// Collection of items retrieved from the database.
        /// </summary>
        public ObservableCollection<T> Items
        {
            get
            {
                if (_items == null)
                    Task.Run(() => LoadItems()).Wait();
                return _items;
            }
        }

        private async Task LoadItems()
        {
            _items = new ObservableCollection<T>(await GetItemsAsync());
        }

        private readonly RealtimeDatabase<T> _realtimeDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService{T}"/> class.
        /// </summary>
        /// <param name="user">The Firebase user.</param>
        /// <param name="path">The path in the database.</param>
        /// <param name="BaseUrl">The base URL of the Firebase database.</param>
        /// <param name="customKey">The custom key.</param>
        public DatabaseService(Firebase.Auth.User user, string path, string BaseUrl, string customKey = "")
        {
            FirebaseOptions options = new FirebaseOptions()
            {
                OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s),
                AuthTokenAsyncFactory = async () => await user.GetIdTokenAsync()
            };
            var client = new FirebaseClient(BaseUrl, options);
            _realtimeDb =
                client.Child(path)
                .AsRealtimeDatabase<T>(customKey, "", StreamingOptions.LatestOnly, InitialPullStrategy.MissingOnly, true);
        }

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the operation succeeds, false otherwise.</returns>
        public async Task<bool> AddItemAsync(T item)
        {
            try
            {
                item.Key = _realtimeDb.Post(item);

                _realtimeDb.Put(item.Key, item);
                Items.Add(item);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the operation succeeds, false otherwise.</returns>
        public async Task<bool> DeleteItemAsync(T item)
        {
            try
            {
                _realtimeDb.Delete(item.Key);
                Items.Remove(item);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Retrieves all items from the database.
        /// </summary>
        /// <param name="forceRefresh">Optional. Specifies whether to force a refresh of the data from the database.</param>
        /// <returns>A task representing the asynchronous operation, returning the collection of items retrieved from the database.</returns>
        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            if (_realtimeDb.Database?.Count == 0 || forceRefresh)
            {
                try
                {
                    await _realtimeDb.PullAsync();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            IEnumerable<T> result = _realtimeDb.Once().Select(x => x.Object);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Retrieves a specific item from the database.
        /// </summary>
        /// <param name="key">The key of the item to retrieve.</param>
        /// <param name="forceRefresh">Optional. Specifies whether to force a refresh of the data from the database.</param>
        /// <returns>A task representing the asynchronous operation, returning the item retrieved from the database.</returns>
        public async Task<T> GetItemAsync(string key, bool forceRefresh = false)
        {
            if (_realtimeDb.Database?.Count == 0 || forceRefresh)
            {
                try
                {
                    await _realtimeDb.PullAsync();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            var result = _realtimeDb.Once().FirstOrDefault(x => key == x.Key);
            return await Task.FromResult(result.Object);
        }

        /// <summary>
        /// Updates an item in the database.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the operation succeeds, false otherwise.</returns>
        public async Task<bool> UpdateItemAsync(T item)
        {
            try
            {
                _realtimeDb.Put(item.Key, item);
                Items[Items.IndexOf(Items.FirstOrDefault(x => x.Key == item.Key))] = item;
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
    }
}