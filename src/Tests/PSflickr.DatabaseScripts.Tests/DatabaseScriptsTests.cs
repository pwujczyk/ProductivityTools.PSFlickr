using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSFlickr.Autofac;
using PSFlickr.Tests.Configuration;

namespace PSFlickr.DatabaseScripts.Tests
{
    [TestClass]
    public class DatabaseScriptsTests
    {
        [TestMethod]
        public void CheckDatabaseCreation()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<PSFlickr.Tests.Configuration.AutofacModule>();
            AutofacContainer.Container = builder.Build();

            DatabaseUpdate databaseUpdate = new DatabaseUpdate();
            databaseUpdate.PerformDatabaseUpdate();

            DBTools dBTools = new DBTools();
            Assert.IsTrue(dBTools.CheckDataBaseExistance());
        }
    }
}
