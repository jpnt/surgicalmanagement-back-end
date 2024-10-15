namespace surgicalmanagement_back_end.MergedInfraApp.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}