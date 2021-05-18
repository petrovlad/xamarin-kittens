using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ByPetravetsKittens.Services
{
    class AvatarService
    {
        private const string DefaultAvatarUuid = "c06f89064c8a49119c29ea1dbd1aab82";
        private const string AvatarUrlFormat = "https://crafatar.com/avatars/{0}?size=150&overlay";
        private const string UuidFetchUrlFormat = "https://api.mojang.com/users/profiles/minecraft/{0}";

        public async Task<string> GetAvatarUrlByNickname(string nickname)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var respone = await client.GetAsync(String.Format(UuidFetchUrlFormat, nickname)))
                    {
                        using (var content = respone.Content)
                        {
                            var jsonResponse = await content.ReadAsStringAsync();
                            var responseFields = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResponse);
                            if (responseFields.ContainsKey("id"))
                            {
                                return GetAvatarUrl(responseFields["id"]);
                            }
                        }
                    }
                }
            } catch (Exception) { }

            return GetDefaultAvatarUrl();
        }

        private string GetAvatarUrl(string uuid)
        {
            return String.Format(AvatarUrlFormat, uuid);
        }

        public string GetDefaultAvatarUrl()
        {
            return GetAvatarUrl(DefaultAvatarUuid);
        }
    }
}
