using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.UnitTests.Fakes
{
    class GUIDStub : IGuid
    {
        public string CreateGUID()
        {
            return ("1234");
        }
    }
}
