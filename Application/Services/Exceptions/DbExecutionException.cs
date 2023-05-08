using System;
namespace Application.DBExceptions
{
    public class DbExecutionException : Exception
    {

        public DbExecutionException(string message)
            : base(message)
        {
        }
    }
}
