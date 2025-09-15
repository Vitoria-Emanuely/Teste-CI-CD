using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using UserApi.Api;
using UserApi.Api.Models;
using UserApi.Api.Services;
using Xunit;

namespace UserApi.Tests
{
    public class UsersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UsersControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Criar uma instância da API em memória
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("User-api");
                // Mock do IUserService para não depender do MongoDB real
                builder.ConfigureServices(services =>
                {
                    var mockService = new Mock<IUserService>();
                    mockService.Setup(s => s.Get()).Returns(new List<User>
                    {
                        new User { Name = "Vitória", Email = "vitoria@email.com" }
                    });
                    mockService.Setup(s => s.Create(It.IsAny<User>())).Returns<User>(u =>
                    {
                        u.Id = "123"; // Id fictício
                        return u;
                    });

                    // Remove o registro real e adiciona o mock
                    services.AddSingleton(mockService.Object);
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetUsuarios_DeveRetornarLista()
        {
            var response = await _client.GetAsync("/api/users");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Vitória", responseString);
        }

        [Fact]
        public async Task PostUsuario_DeveCriarUsuario()
        {
            var user = new User { Name = "João", Email = "joao@email.com" };
            var json = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/users", json);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("João", responseString);
            Assert.Contains("123", responseString); // Id do mock
        }
    }
}
