using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AuthApi.Models.Users;

namespace AuthApi.Tests;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public AuthControllerTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }




    [Fact]
    public async Task GivenAValidUser_WhenLoggingIn_ThenReturnsJwtToken()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();

        var user = new AuthenticateRequest();
        user.Username = "Admin";
        user.Password = "Admin_P@ssw0rd";

        //// Act
        var result = await client.PostAsJsonAsync("api/users/authenticate", user);
        var content = await result.Content.ReadAsStringAsync();
        var jwtToken = JsonConvert.DeserializeObject<AuthenticationToken>(content);

        //// Assert
        Assert.NotNull(jwtToken);
    }

    [Fact]
    public async Task GivenAnInvalidUser_WhenLoggingIn_ThenReturnsUnauthorized()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();
        var user = new AuthenticateRequest();
        user.Username = "Adminxxxx";
        user.Password = "Admin_P@ssw0rdxxx";

        // Act
        var result = await client.PostAsJsonAsync("api/users/authenticate", user);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }
}
