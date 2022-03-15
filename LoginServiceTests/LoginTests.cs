using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using LoginService.Commands;
using LoginService.PipelineBehaviors;
using LoginService.Validations;
using MediatR;
using Moq;
using Xunit;
using ValidationException = Infrastructure.Exceptions.ValidationException;

namespace LoginServiceTests
{
    public class LoginTests
    {
        [Theory]
        [InlineData("", "")]
        public async Task LoginService_RegisterLogin_ValidationFail_Then_ThrowException(string username,
            string password)
        {
            // Arrange
            var registerLogin = new RegisterLoginCommand {Username = username, Password = password};
            var validators = new List<IValidator<RegisterLoginCommand>>
            {
                new RegisterLoginValidation()
            };
            var behavior = new ValidationBehavior<RegisterLoginCommand, Unit>(validators);

            // Act 
            Func<Task<Unit>> action = async () =>
                await behavior.Handle(registerLogin, CancellationToken.None, It.IsAny<RequestHandlerDelegate<Unit>>());

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}