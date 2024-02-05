using System.Net.Http;
using System.Threading.Tasks;

namespace IIABackend
{
    /// <summary>
    /// Class to manage external APIs
    /// </summary>
    public class APICallers
    {
        /*public static async Task<CryptoRates> GetWazirxTickers()
        {
            var path = "https://api.wazirx.com/api/v2/tickers";
            return new CryptoRates(await GetAsync<WazirXMarket>(path).ConfigureAwait(false));
        }*/

        private static async Task<T> GetAsync<T>(string path)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }
    }
}
