using Newtonsoft.Json.Linq;

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
