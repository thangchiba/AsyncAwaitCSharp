﻿using System;
using System.Runtime.Serialization.Formatters.Binary;
using Server.ExtensionMethod;

namespace Server.NetworkPackage
{
    [Serializable]
    public abstract class Package
    {
        public Package()
        {
        }

        public long GetSize()
        {
            return this.Serialize().Length;
        }
    }
}

