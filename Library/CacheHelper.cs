using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Library
{
    public static class CacheHelper
    {
        public static string SUPPORT_QUOTATION_STATUS = "SUPPORT_QUOTATION_STATUS";
        public static string SUPPORT_PLC_IMAGE_TYPE = "SUPPORT_PLC_IMAGE_TYPE";
        public static string SUPPORT_CONTAINER_TYPE = "SUPPORT_CONTAINER_TYPE";
        public static string SUPPORT_PRODUCT_TYPE = "SUPPORT_PRODUCT_TYPE";
        public static string SUPPORT_PRODUCT_GROUP = "SUPPORT_PRODUCT_GROUP";
        public static string SUPPORT_MANUFACTURER_COUNTRY = "SUPPORT_MANUFACTURER_COUNTRY";
        public static string SUPPORT_MODEL_STATUS = "SUPPORT_MODEL_STATUS";
        public static string SUPPORT_PRODUCT_CATEGORY = "SUPPORT_PRODUCT_CATEGORY";
        public static string SUPPORT_TASK_STATUS = "SUPPORT_TASK_STATUS";
        public static string SUPPORT_MOVEMENT_TERM = "SUPPORT_MOVEMENT_TERM";
        public static string SUPPORT_OCEAN_FREIGHT = "SUPPORT_OCEAN_FREIGHT";
        public static string SUPPORT_DELIVERY_TERM = "SUPPORT_DELIVERY_TERM";
        public static string SUPPORT_PAYMENT_TERM = "SUPPORT_PAYMENT_TERM";
        public static string USER_DATA = "USER_DATA";

        public static void ClearCache()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(o => o.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }

        public static void ClearCache(string key)
        {
            if (MemoryCache.Default.Contains(key))
            {
                MemoryCache.Default.Remove(key);
            }
        }

        public static object GetData(string key)
        {
            if (MemoryCache.Default.Contains(key))
            {
                return MemoryCache.Default.Get(key);
            }

            return null;
        }

        public static void SetData(string key, object data)
        {
            MemoryCache.Default.Set(key, data, DateTimeOffset.UtcNow.AddMonths(1));
        }
    }
}
