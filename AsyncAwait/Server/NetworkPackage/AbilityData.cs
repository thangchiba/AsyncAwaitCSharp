using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Server.NetworkPackage
{
    [Serializable]
    public class AbilityData : Package
    {
        string id = Guid.NewGuid().ToString();
        float position = 16f;
        public AbilityData()
        {

        }
        public AbilityData(string id, float position)
        {
            this.id = id;
            this.position = position;
        }
    }
}

