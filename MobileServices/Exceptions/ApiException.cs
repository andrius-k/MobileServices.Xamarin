using System;
namespace MobileServices.Exceptions
{
    /// <summary>
    /// API exception is thrown when API returns non-success status code.
    /// </summary>
    public class ApiException : Exception
    {
        public ApiException(string message) : base (message)
        {
        }
    }
}
