using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.UnitTests.Fakes
{
    public class GUIDMock : IGuid
    {
        public bool GuidCalled { get; set; }
        public string CreateGUID()
        {
            GuidCalled = true;
            return default(string);
        }
    }
}
