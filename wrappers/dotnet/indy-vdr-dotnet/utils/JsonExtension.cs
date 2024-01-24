using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hyperledger.Indy.utils
{
    internal static class JsonExtension
    {
        public static bool NodeHasChild(this JToken data )
        {
            if (data.HasValues)
            {
                return true;
            }
            return false;
        }
    }
}
