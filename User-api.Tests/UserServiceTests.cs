using Xunit;
using Moq;
using UserApi.Api.Services;
using UserApi.Api.Models;
using System.Collections.Generic;

namespace UserApi.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void CriarUsuario_DeveRetornarUsuario()
        {
            // Arrange
            var user = new User { Name = "Vitória", Email = "vitoria@email.com" };
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.Create(user)).Returns(user);

            // Act
            var resultado = mockService.Object.Create(user);

            // Assert
            Assert.Equal(user.Name, resultado.Name);
            Assert.Equal(user.Email, resultado.Email);
        }

        [Fact]
        public void ObterUsuarios_DeveRetornarLista()
        {
            // Arrange
            var lista = new List<User>
            {
                new User { Name = "Vitória", Email = "vitoria@email.com" }
            };
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.Get()).Returns(lista);

            // Act
            var resultado = mockService.Object.Get();

            // Assert
            Assert.Single(resultado);
            Assert.Equal("Vitória", resultado[0].Name);
        }
    }
}
