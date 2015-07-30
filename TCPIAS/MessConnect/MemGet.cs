using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeIT.MemCached;

namespace TCPIAS.MessConnect
{
    class MemGet
    {
        private static MemcachedClient cache;

        public static void StartMemCa() {

            MemcachedClient.Setup("MyCache", new string[] { "115.29.172.236:11211" });

           cache = MemcachedClient.GetInstance("MyCache");

            //MemcachedClient configFileCache = MemcachedClient.GetInstance("MyConfigFileCache");

            cache.SendReceiveTimeout = 5000;
            cache.ConnectTimeout = 5000;
            cache.MinPoolSize = 1;
            cache.MaxPoolSize = 5;
           
        }

        public static string MemcacheGet(string value) {
            try
            {
                Console.WriteLine(cache.Get(value) as string + "11");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
            return cache.Get(value) as string;
            
        }

        public static void MemcacheSet(string key,string value)
        {
            try
            {
               cache.Set(key,value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
