using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using LoginService.Commands;
using LoginService.PipelineBehaviors;
using LoginService.Repositories.Contracts;
using LoginService.Validations;
using MediatR;
using Moq;
using Xunit;
using ValidationException = Infrastructure.Exceptions.ValidationException;

namespace LoginServiceTests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", "password")]
        [InlineData("ravenaidy", "")]
        [InlineData("james", "This@I45Iene#$")]
        public async Task LoginService_RegisterLogin_ValidationFail_Then_ThrowException(string username,
            string password)
        {
            // Arrange
            var loginRepository = new Mock<ILoginRepository>();
            var registerLogin = new RegisterLoginCommand {Username = username, Password = password};
            loginRepository.Setup(x => x.UserNameExists(username)).ReturnsAsync(true);
            var validators = new List<IValidator<RegisterLoginCommand>>
            {
                new RegisterLoginValidation(loginRepository.Object)
            };
            var behavior = new ValidationBehavior<RegisterLoginCommand, Unit>(validators);

            // Act 
            Func<Task<Unit>> action = async () =>
                await behavior.Handle(registerLogin, CancellationToken.None, It.IsAny<RequestHandlerDelegate<Unit>>());

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task LoginService_RegisterLogin_ValidationSuccess_Then_Login_Successful()
        {
            // Arrange
            const string username = "ravenaidy";
            const string password = "P@55w0rd";
            var commandHandlerDelegate = new Mock<RequestHandlerDelegate<Unit>>();
            commandHandlerDelegate.Setup(x => x.Invoke()).ReturnsAsync(new Unit());
            var loginRepository = new Mock<ILoginRepository>();
            var registerLogin = new RegisterLoginCommand {Username = username, Password = password};
            loginRepository.Setup(x => x.UserNameExists(username)).ReturnsAsync(false);
            var validators = new List<IValidator<RegisterLoginCommand>>
            {
                new RegisterLoginValidation(loginRepository.Object)
            };
            var behavior = new ValidationBehavior<RegisterLoginCommand, Unit>(validators);
            
            // Act
            var response = await behavior.Handle(registerLogin, CancellationToken.None, commandHandlerDelegate.Object);

            // Assert
            response.Should().Be(Unit.Value);
        }
    }
}