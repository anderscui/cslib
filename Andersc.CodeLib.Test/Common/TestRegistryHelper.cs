using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestRegistryHelper
    {
        [Test]
        public void TestSetRegistryKey()
        {
            //Assert.That(RegistryHelper.RootKey.Name, Is.EqualTo(Registry.CurrentUser.Name));

            //RegistryHelper.SetRegistryKey(Registry.LocalMachine);
            //Assert.That(RegistryHelper.RootKey.Name, Is.EqualTo(Registry.LocalMachine.Name));
        }
    }
}
