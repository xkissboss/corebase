using CSRedis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace common.CSRedis
{
    public partial class RedisHelper : QuickHelperBase
    {
        internal static IConfigurationRoot Configuration;
        public static void InitializeConfiguration(IConfigurationRoot cfg)
        {
            Configuration = cfg;
            int port, poolsize, database;
            string ip, pass;
            if (!int.TryParse(cfg["ConnectionStrings:redis:port"], out port)) port = 6379;
            if (!int.TryParse(cfg["ConnectionStrings:redis:poolsize"], out poolsize)) poolsize = 50;
            if (!int.TryParse(cfg["ConnectionStrings:redis:database"], out database)) database = 0;
            ip = cfg["ConnectionStrings:redis:ip"];
            pass = cfg["ConnectionStrings:redis:pass"];
            Name = cfg["ConnectionStrings:redis:name"];
            Instance = new ConnectionPool(ip, port, poolsize);
            Instance.Connected += (s, o) => {
                RedisClient rc = s as RedisClient;
                if (!string.IsNullOrEmpty(pass)) rc.Auth(pass);
                if (database > 0) rc.Select(database);
            };
        }
    }

    public partial class RedisHelper : QuickHelperBase
    {
        public static void SetEntity<T>(string key, T value, int expireSeconds = -1)
        {
            RedisHelper.Set(key, JsonConvert.SerializeObject(value), expireSeconds);
        }

        public static T GetEntity<T>(string key)
        {
            var value = RedisHelper.Get(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

    }
}
