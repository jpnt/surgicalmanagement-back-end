namespace surgicalmanagement_back_end.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string Email { get; set; }
}