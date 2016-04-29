using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ConsoleClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var token = GetClientToken();
                if (token.IsError || token.IsHttpError)
                {
                    Console.WriteLine("Failed to get token from identity server. Err:{0}", token.Error);
                    return;
                }
                Console.WriteLine("Token get from identity server. /n Token={0}", token.AccessToken);
                CallApi(token);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
                "http://localhost:10367/connect/token",
                "silicon",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");

            return client.RequestClientCredentialsAsync("api1").Result;
        }

        static TokenResponse GetUserToken()
        {
            var client = new TokenClient(
                "http://localhost:10367/connect/token",
                "carbon",
                "21B5F798-BE55-42BC-8AA8-0025B903DC3B");

            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "api1").Result;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("http://localhost:10835/test").Result);
        }
    }
}
