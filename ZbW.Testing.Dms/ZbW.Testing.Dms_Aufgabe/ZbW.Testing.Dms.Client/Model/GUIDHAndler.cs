using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Model
{
    [ExcludeFromCodeCoverage]
    public class GuidHandler : IGuid
    {
        public string CreateGUID()
        {
            Guid g = Guid.NewGuid();
            return g.ToString();
        }
    }
}
