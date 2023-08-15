using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.UI.Cache;

public static class MemoryCacheService
{
    


    public static T Set<T>(this IMemoryCache _cache,  string key, T value) where T : class
    {


        var cacheOptions = new MemoryCacheEntryOptions()
           .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

        return _cache.Set(key, value, cacheOptions);



    }

    public static bool TryGetValue<T>(this IMemoryCache _cache, string key, out T? value) where T : class
    {
        T val = (T)_cache.Get(key);
        value = default;

        if (val == null) return false;

        value = val;

        return true;
    }



}
