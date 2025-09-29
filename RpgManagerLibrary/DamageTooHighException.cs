
namespace RpgManagerLibrary
{
    [Serializable]
    public class DamageTooHighException : Exception
    {
        public DamageTooHighException()
        {
        }

        public DamageTooHighException(string? message) : base(message)
        {
        }

        public DamageTooHighException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}