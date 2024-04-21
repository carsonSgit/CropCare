using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropCare.Interfaces
{
    interface IDataStore<T>
    {
        public ObservableCollection<T> Items { get; }

        Task<bool> AddItemAsync(T item);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(T item);
    }
}
