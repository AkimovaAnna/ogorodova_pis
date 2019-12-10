using pavlovLab.Models;

namespace pavlovLab.Storage
{
    public class StorageService
    {
        private readonly IStorage<MyModData> _storage;

        public StorageService(IStorage<MyModData> storage)
        {
            _storage = storage;
        }

        public string GetStorageType()
        {
            return _storage.StorageType;
        }

        public int GetNumberOfItems()
        {
            return _storage.All.Count;
        }
    }
} 