using IngfoPolinema.Core.DomainModels;
using System.Threading.Tasks;

namespace IngfoPolinema.Core.Services.Interfaces;

public interface IVisitor
{
    Task<PingHistory> VisitAsync(PingTarget website);
}
