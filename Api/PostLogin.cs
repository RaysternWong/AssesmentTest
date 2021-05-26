using Newtonsoft.Json;
using RestSharp;
using System;

namespace Api
{
    public class PostLogin : Base
    {
        protected override string RelativeUrl => "Account/Login";

        [JsonProperty("username")]
        private string name;

        [JsonProperty("password")]
        private string password;

        public PostLogin(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public PostLogin(string name, string password, string url) : this(name, password)
        {
            this.BaseUrl = url;
        }

        public string GetBearerToken()
        {
            var client = new RestClient(FullUrl);

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(this));

            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                throw new Exception($"Failed To Login, response : {response.Content}");
            }
        }
    }
}
