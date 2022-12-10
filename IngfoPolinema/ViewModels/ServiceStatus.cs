using System.ComponentModel;

namespace IngfoPolinema.ViewModels;
public enum ServiceStatus
{
    [Description("Operational")]
    Operational,
    [Description("Internal Server Error")]
    InternalServerError,
    [Description("Unknown")]
    Unknown,
};