using CropCare.Interfaces;
using Firebase.Database;
using Firebase.Database.Offline;
using System.Collections.ObjectModel;

namespace CropCare.Services
{
    public class DatabaseService<T> : IDataStore<T> where T : class, IHasKey
    {
        private ObservableCollection<T> _items;
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
