﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.IdentityModel.SystemWeb
{
    public interface ITokenCacheRepository
    {
        void AddOrUpdate(TokenCacheItem item);
        TokenCacheItem Get(string key);
        void Remove(string key);
    }

    public class TokenCacheItem
    {
        [Key]
        public string Key { get; set; }
        public DateTime Expires { get; set; }
        public byte[] Token { get; set; }
    }
}
