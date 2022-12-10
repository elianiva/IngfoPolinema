using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace IngfoPolinema.Infrastructure.Models;

public sealed class PingHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid PingHistoryId { get; set; }

    required public TimeSpan Duration { get; init; }
    required public HttpStatusCode StatusCode { get; init; }
    required public DateTimeOffset TimeStamp { get; init; }

    public Guid PingTargetId { get; set; }
    public PingTarget? PingTarget { get; }
}
