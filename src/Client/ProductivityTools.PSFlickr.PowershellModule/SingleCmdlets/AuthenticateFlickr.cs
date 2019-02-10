using ProductivityTools.PSCmdlet;
using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Cmdlet
{
    [Cmdlet("Authenticate", "Flickr")]
    public class AuthenticateFlickr : System.Management.Automation.PSCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            FlickrOperations autentication = new FlickrOperations();
            autentication.OpenAutorizeAddress();

            Console.WriteLine("To authorize application Flickr site will be open");
            Console.WriteLine("Please copy the code which will be shown and paste it here:");
            string token =Console.ReadLine(); ;
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("You must provide token to autorize. No action taken");
            }
            else
            {
                autentication.GetAndSaveAccessToken(token);
            }
            base.ProcessRecord();
            Console.Write("xxxx");
            Console.Read();
            Console.Write("xxxx");
        }
    }
}
