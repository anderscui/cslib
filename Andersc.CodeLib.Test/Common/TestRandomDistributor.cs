using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;
using NUnit.Framework;

using Andersc.CodeLib.Algorithm.Problems;

namespace Andersc.CodeLib.Tester.Common
{
    public class Lead
    {
        public int LeadId { get; set; }
        public string School { get; set; }
        public string Source { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    public class TestRandomDistributor
    {
        private List<Lead> Data { get; set; }
        private List<DispatchTarget<User>> Targets { get; set; }
        private List<string> Schools { get; set; }
        private List<string> Sources { get; set; }
        
        [SetUp]
        public void Setup()
        {
            Schools = new List<string>() { "BJ1", "BJ2", "BJ3", "SH1", "SH2" };
            Sources = new List<string>() { "Online", "Offline", "Others" };

            Targets = new List<DispatchTarget<User>>()
            {
                new DispatchTarget<User>()
                {
                    Target = new User() { Id = 1, Name = "John" },
                    Weight = 2
                },
                new DispatchTarget<User>()
                {
                    Target = new User() { Id = 2, Name = "Chandler" },
                    Weight = 2
                },
                new DispatchTarget<User>()
                {
                    Target = new User() { Id = 3, Name = "Nancy" },
                    Weight = 1
                }
            };

            var leadsCount = 1003;
            Data = new List<Lead>(leadsCount);
            for (var i = 0; i < leadsCount; i++)
            {
                var lead = new Lead() {LeadId = i, School = Schools[i%Schools.Count], Source = Sources[i%Sources.Count]};
                Data.Add(lead);
            }
        }
            
        [Test]
        public void TestStringOfIndexWithAutoPadding()
        {
            var dist = new RandomDispatcher<User, Lead>(Targets, Data);
            var distResult = dist.Dispatch();
            //Console.WriteLine(distResult.Count);
            foreach (var result in distResult)
            {
                Console.WriteLine("User: {0}, Weight: {1}, Count: {2}", result.Target.Target.Name, result.Target.Weight, result.Data.Count);
            }
        }
    }
}
