using System.Collections.Concurrent;
using TokenJwtBearer.Models;

namespace TokenJwtBearer.Services
{
    public class CacheService
    {
        public ConcurrentDictionary<string, Registros> Cache { get; }

        public CacheService()
        {
            Cache = new ConcurrentDictionary<string, Registros>();
        }
    }
}
