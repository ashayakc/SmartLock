using Newtonsoft.Json;
using SmartLock.Workflow.Test.Model;
using System.Net.Http.Headers;
using System.Text;

namespace SmartLock.Workflow.Test.DSL
{
    public class SmartLockAdapter
    {
        public async Task<HttpResponseMessage> LoginAsync(string userName, string password)
        {
            var client = new HttpClient();
            var body = new UserCredentialDto { UserName = userName, Password = password };
            return await client.PostAsync("http://localhost:5012/api/users/login", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> OpenDoorAsync(string comments, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await client.PostAsync("http://localhost:5012/api/doors/1/open", new StringContent(JsonConvert.SerializeObject(comments), Encoding.UTF8, "application/json"));
        }
    }
}
