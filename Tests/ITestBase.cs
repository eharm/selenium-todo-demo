using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public interface ITestBase
    {
        public void BeforeEach();

        public void AfterEach();
    }
}
