using System;
using Newtonsoft.Json;
using Common.Constants;

namespace WebUI.Models.User
{
    public class SignInResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public string AccessTokenFromOauth { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public uint ExpiresIn { get; set; }

        [JsonProperty(ClaimsConstants.ClaimUserName)]
        public string UserName { get; set; }

        [JsonProperty(ClaimsConstants.ClaimUserId)]
        public string UserId { get; set; }

        [JsonProperty(".issued")]
        public DateTimeOffset Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTimeOffset Expires { get; set; }

        [JsonProperty(ClaimsConstants.ClaimRole)]
        public string Roles { get; set; }
    }
}