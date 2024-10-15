namespace surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;

public class UpdateOperationRequestDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid OperationTypeId { get; set; }
    public DateTime RequestedDate { get; set; }
    public string Priority { get; set; }
}