using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace managementHR.Healpers
{
    public static class Extensions
    {
        public static void AddAplicationError(this HttpResponse response,string message)
        {
            response.Headers.Add("Aplication-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Aplication-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

        }
    }
}
