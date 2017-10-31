using cowpi.Representations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CowpiTests
{
    public class StatementOfWorkTests
    {
        [Fact]
        public void StatementOfWorkWrite()
        {
            var sow = new StatementOfWork
            {
                new StatementOfWork.Task() { Description = "Do this crap" }
            };

            var sr = new StringWriter();
            sow.Save(sr);
            var sb = sr.GetStringBuilder();
            var output = sb.ToString();
        }
    }
}
