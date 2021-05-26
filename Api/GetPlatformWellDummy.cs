using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class GetPlatformWellDummy: GetPlatformWellActual
    {
        protected override string RelativeUrl => "PlatformWell/GetPlatformWellDummy";

        public GetPlatformWellDummy(string bearerToken) : base(bearerToken) {}

        public GetPlatformWellDummy(string bearerToken, string url) : base(bearerToken, url) {}
    }
}
