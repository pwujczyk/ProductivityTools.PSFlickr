using PSFlickr.Configuration;
using Autofac;
using PSFlickr.Autofac;
using System.Reflection;

namespace PSFlickr.DatabaseScripts
{
    public class DatabaseUpdate
    {
        public void PerformDatabaseUpdate()
        {
            using (var scope = AutofacContainer.Container.BeginLifetimeScope())
            {
                IConfig config = scope.Resolve<IConfig>();
                DBUpPT.DBUp dbup = new DBUpPT.DBUp("flickr");
                Assembly asembly = Assembly.GetExecutingAssembly();
                dbup.PerformUpdate(config.DataSource, config.DatabaseName, asembly, false);
            }
        }
    }
}