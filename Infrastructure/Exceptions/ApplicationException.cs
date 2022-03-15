using System;

namespace Infrastructure.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string title, string message)
            : base(message) =>
            Title = title;

        private string Title { get; }
    }
}