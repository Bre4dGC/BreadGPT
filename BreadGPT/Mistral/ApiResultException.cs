
namespace Mistral
{
    [Serializable]
    internal class ApiResultException : Exception
    {
        public ApiResultException()
        {
        }

        public ApiResultException(string? message) : base(message)
        {
        }

        public ApiResultException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}