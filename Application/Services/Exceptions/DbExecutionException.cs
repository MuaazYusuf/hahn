using System;
namespace Application.DBExceptions
{
    public class DbExecutionException : Exception
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public DbExecutionException(string propertyName, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }
    }
}
