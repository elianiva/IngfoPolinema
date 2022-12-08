using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngfoPolinema.Core.Services;

public interface Visitor
{
    Task<PingResult> VisitAsync(PingTarget website);
}
