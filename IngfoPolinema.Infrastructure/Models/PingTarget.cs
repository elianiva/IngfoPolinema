using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IngfoPolinema.Infrastructure.Models;

public sealed class PingTarget
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid PingTargetId { get; set; }

    required public string Name { get; init; }
    required public string Description { get; init; }
    required public Uri Address { get; init; }
    required public TimeSpan Interval { get; init; }
    required public TimeSpan Timeout { get; init; }

    public List<PingHistory> Histories { get; set; } = new();
}
