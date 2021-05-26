using Data;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;

namespace Api
{
    public class GetPlatformWellActual : Base
    {
        protected override string RelativeUrl => "PlatformWell/GetPlatformWellActual";

        protected string bearerToken;
        
        public GetPlatformWellActual(string bearerToken)
        {
            this.bearerToken = bearerToken;
        }

        public GetPlatformWellActual(string bearerToken, string url) :  this(bearerToken)
        {
           this.BaseUrl = url;
        }

        public List<PlatformWellActual> Response()
        {
            var client = new RestClient(FullUrl)
            {
                Authenticator = new JwtAuthenticator(bearerToken)
            };

            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<PlatformWellActual>>(response.Content);
            }
            else
            {
                throw new Exception($"Access API {RelativeUrl} Failed, Response Return : {response.Content}");
            }
        }
    }
}
