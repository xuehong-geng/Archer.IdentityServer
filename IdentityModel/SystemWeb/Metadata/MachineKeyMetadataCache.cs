/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see LICENSE
 */

using System;
using System.Web.Security;
using Thinktecture.IdentityModel.Metadata;

namespace Thinktecture.IdentityModel.SystemWeb
{
    class MachineKeyMetadataCache : IMetadataCache
    {
        const string Purpose = "MachineKeyMetadataCache";

        IMetadataCache inner;
        public MachineKeyMetadataCache(IMetadataCache inner)
        {
            if (inner == null) throw new ArgumentNullException("inner");

            this.inner = inner;
        }

        public TimeSpan Age
        {
            get { return inner.Age; }
        }

        public byte[] Load()
        {
            var bytes = inner.Load();
            try
            {
                return MachineKey.Unprotect(bytes, Purpose);
            }
            catch
            {
                inner.Save(null);
            }
            return null;
        }

        public void Save(byte[] data)
        {
            data = MachineKey.Protect(data, Purpose);
            inner.Save(data);
        }
    }
}
