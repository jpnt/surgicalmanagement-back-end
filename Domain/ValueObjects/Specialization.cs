using System.ComponentModel;

namespace surgicalmanagement_back_end.Domain.ValueObjects;

public enum Specialization
{
    [Description("None")]
    None, 

    [Description("Cardiology")]
    Cardiology,

    [Description("Dermatology")]
    Dermatology,

    [Description("Pediatrics")]
    Pediatrics,

    [Description("Neurology")]
    Neurology,

    [Description("Oncology")]
    Oncology,

    [Description("Radiology")]
    Radiology,

    [Description("Surgery")]
    Surgery,

    [Description("Orthopedics")]
    Orthopedics,

    [Description("Psychiatry")]
    Psychiatry
}