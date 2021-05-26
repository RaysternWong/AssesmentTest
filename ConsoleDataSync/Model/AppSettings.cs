namespace ConsoleDataSync.Model
{
    public interface IAppSettings
    {
        IAPI Api { get; set; }
        ISql Sql { get; set; }
    }

    public interface IAPI
    {
        public string Url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public interface ISql
    {
        public string connection { get; set; }
    }
}
