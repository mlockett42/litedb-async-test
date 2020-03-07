using System;
using Xunit;
using litedbasync;
using System.IO;

namespace litedbasynctest
{
    public class SimplePerson
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}