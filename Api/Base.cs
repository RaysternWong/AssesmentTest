namespace Api
{
    public abstract class Base
    {
        protected abstract string RelativeUrl { get; }

        protected string BaseUrl { get; set; } = "http://test-demo.aemenersol.com/api/";

        protected string FullUrl => BaseUrl + RelativeUrl;
    }
}
