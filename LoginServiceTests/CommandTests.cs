using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using LoginService.Commands;
using LoginService.Handlers;
using LoginService.Models;
using LoginService.Repositories.Contracts;
using MediatR;
using Moq;
using Xunit;

namespace LoginServiceTests
{
    public class CommandTests
    {
        private readonly Mapper _mapper;

        public CommandTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMappingProfile>()));
        }
        
        [Fact]
        public async Task LoginCommand_New_Login_CreatedSuccessfully()
        {
            // Arrange
            const string username = "ravenaidy";
            const string password = "password";
            var login = new Login {Username = username, Password = password};
            
            var loginRepository = new Mock<ILoginRepository>();
            loginRepository.Setup(x => x.RegisterLogin(login)).Verifiable();
            
            var registerLoginCommand = new RegisterLoginCommand {Username = username, Password = password};
            var registerLoginCommandHandler = new RegisterLoginCommandHandler(loginRepository.Object, _mapper);

            // Act
            var result = await registerLoginCommandHandler.Handle(registerLoginCommand, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
        }
        
        [Fact]
        public async Task AccountCreateHandler_DBFails_ThrowsException()
        {
            // Arrange

            var loginRepository = new Mock<ILoginRepository>();
            loginRepository.Setup(x => x.RegisterLogin(It.IsAny<Login>())).Throws<Exception>();
            
            var registerLoginCommand = new RegisterLoginCommand {Username = "username", Password = "password"};
            var registerLoginCommandHandler = new RegisterLoginCommandHandler(loginRepository.Object, _mapper);

            // Act
            Func<Task<Unit>> action = async () => await registerLoginCommandHandler.Handle(registerLoginCommand, CancellationToken.None);
            
            // Assert
            await action.Should().ThrowAsync<Exception>();
        }
    }
}