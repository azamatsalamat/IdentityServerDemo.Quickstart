using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Runtime;

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var request = new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "client1",
    ClientSecret = "secret1",
    Scope = "api1"
};

var tokenResponse = await client.RequestClientCredentialsTokenAsync(request);

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:6001/identity");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(JArray.Parse(content));
}

Console.ReadLine();