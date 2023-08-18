using System.Text.Json;

namespace BetServer.Models.Function
{
    public class JsonHelper
    {
        public static async Task<JsonElement?> GetJsonPropertyFromRequestBody(Stream bodyStream, string propertyName)
        {
            using (StreamReader reader = new StreamReader(bodyStream))
            {
                string requestBody = await reader.ReadToEndAsync();
                var jsonDocument = JsonDocument.Parse(requestBody);
                if (jsonDocument.RootElement.TryGetProperty(propertyName, out var propertyElement))
                {
                    return propertyElement;
                }
                return null;
            }
        }
    }
}
