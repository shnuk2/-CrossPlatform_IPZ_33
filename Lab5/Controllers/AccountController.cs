using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Lab5.Models;
using System.Net.Http;
using System.Security.Claims;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        public async Task Login(string redirectUrl = "/")
        {
            var authenticationProps = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(redirectUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProps);
        }

        [Authorize]
        public async Task Logout()
        {
            var logoutProps = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, logoutProps);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var userProfile = new UserViewModel
            {
                Username = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value,
                Email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value
            };

            if (userId != null)
            {
                var userMetadata = await FetchUserMetadataAsync(userId);
                if (userMetadata != null)
                {
                    userProfile.FullName = userMetadata.FullName;
                    userProfile.PhoneNumber = userMetadata.PhoneNumber;
                }
            }

            return View(userProfile);
        }

        private async Task<UserMetadata> FetchUserMetadataAsync(string userId)
        {
            using var client = new HttpClient();
            var token = await RetrieveManagementApiTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var domain = _config["Auth0:Domain"];
            var response = await client.GetAsync($"https://{domain}/api/v2/users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var auth0User = await response.Content.ReadFromJsonAsync<Auth0UserProfile>();
                return auth0User?.UserMetadata;
            }

            return null;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var client = new HttpClient();
            var token = await RetrieveManagementApiTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var newUser = new
            {
                email = model.Email,
                user_metadata = new
                {
                    full_name = model.FullName,
                    phone_number = model.PhoneNumber
                },
                connection = "Username-Password-Authentication",
                password = model.Password,
                username = model.Username
            };

            var response = await client.PostAsJsonAsync($"https://{_config["Auth0:Domain"]}/api/v2/users", newUser);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Registration failed: {response.StatusCode}, {errorContent}");
            ModelState.AddModelError(string.Empty, $"Registration failed: {errorContent}");

            return View(model);
        }

        private async Task<string> RetrieveManagementApiTokenAsync()
        {
            using var client = new HttpClient();
            var clientId = _config["Auth0:ClientId"];
            var clientSecret = _config["Auth0:ClientSecret"];
            var domain = _config["Auth0:Domain"];

            var tokenRequest = new
            {
                client_id = clientId,
                client_secret = clientSecret,
                audience = $"https://{domain}/api/v2/",
                grant_type = "client_credentials"
            };

            var response = await client.PostAsJsonAsync($"https://{domain}/oauth/token", tokenRequest);
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

            return tokenResponse?.AccessToken;
        }

        private class TokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
        }

        private class Auth0UserProfile
        {
            [JsonPropertyName("user_id")]
            public string UserId { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("user_metadata")]
            public UserMetadata UserMetadata { get; set; }
        }

        private class UserMetadata
        {
            [JsonPropertyName("full_name")]
            public string FullName { get; set; }

            [JsonPropertyName("phone_number")]
            public string PhoneNumber { get; set; }
        }
    }
}
