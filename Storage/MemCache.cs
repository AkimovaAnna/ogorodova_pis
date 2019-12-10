using System;
using System.Collections.Generic;
using System.Linq;
using pavlovLab.Models;

namespace pavlovLab.Storage
{
    public class MemCache : IStorage<MyModData>
    {
        private object _sync = new object();
        private List<MyModData> _memCache = new List<MyModData>();
        public MyModData this[Guid id] 
        { 
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectMyModDataException($"No MyModData with id {id}");

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectMyModDataException("Cannot request MyModData with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<MyModData> All => _memCache.Select(x => x).ToList();

        public void Add(MyModData value)
        {
            if (value.Id != Guid.Empty) throw new IncorrectMyModDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
        public string StorageType => $"{nameof(MemCache)}";
    }
}