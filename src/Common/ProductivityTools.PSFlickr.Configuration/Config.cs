using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlickr.Configuration
{
    public class Config : IConfig
    {
        public string DataSource
        {
            get
            {
                return @".\sql2017";
            }
        }
        public string DatabaseName
        {
            get
            {
                return "FlickrTest";
            }
        }
    }
}
