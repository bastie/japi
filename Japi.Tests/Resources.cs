﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NightlyCode.Japi.Tests {
    public static class Resources {

        public static IEnumerable<ResourceData<Stream>> ObjectStreams
        {
            get
            {
                foreach (string resource in typeof(ObjectStreamTests).Assembly.GetManifestResourceNames().Where(n => n.StartsWith("NightlyCode.Japi.Tests.Data.ObjectStreams")))
                    yield return new ResourceData<Stream>(resource, typeof(ObjectStreamTests).Assembly.GetManifestResourceStream(resource));
            }
        }

        public static IEnumerable<ResourceData<string>> JSon
        {
            get
            {
                foreach (string resource in typeof(ObjectStreamTests).Assembly.GetManifestResourceNames().Where(n => n.StartsWith("NightlyCode.Japi.Tests.Data.Json")))
                {
                    using (Stream data = typeof(ObjectStreamTests).Assembly.GetManifestResourceStream(resource))
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            yield return new ResourceData<string>(resource, reader.ReadToEnd());
                        }
                    }
                }
            }
        }

        public static IEnumerable<ResourceData<byte[]>> JsonBytes
        {
            get
            {
                foreach(string resource in typeof(ObjectStreamTests).Assembly.GetManifestResourceNames().Where(n => n.StartsWith("NightlyCode.Japi.Tests.Data.Json"))) {
                    using(Stream data = typeof(ObjectStreamTests).Assembly.GetManifestResourceStream(resource)) {
                        using(MemoryStream memorystream = new MemoryStream()) {
                            data.CopyTo(memorystream);
                            yield return new ResourceData<byte[]>(resource, memorystream.ToArray());
                        }
                    }
                }
            }
        }
    }
}