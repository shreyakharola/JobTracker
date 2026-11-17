namespace JobTracker.Core.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}