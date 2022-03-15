using AccountService.Models;
using MediatR;

namespace AccountService.Commands;
public record RegisterAccountCommand(string FirstName, string LastName, int IdNumber, Gender Gender, Address Address) : IRequest;
