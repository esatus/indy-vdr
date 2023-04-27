namespace indy_vdr_dotnet
{
    public class Consts
    {
#if __IOS__
        public const string LIBINDY_VDR_NAME = "__Internal";
#else
        public const string LIBINDY_VDR_NAME = "indy_vdr";
#endif
    }
}
