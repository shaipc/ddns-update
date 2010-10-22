using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace ddns_update
{
    class IpDetector
    {
        public enum IpDetectionMethod { UPnP, URL };
        private IpDetectionMethod m_method;
        private bool UpnpDiscovered = false;

        public IpDetector(IpDetectionMethod method)
        {
            m_method = method;
        }

        private bool UpnpDiscovery()
        {
            UpnpDiscovered = UPnP.NAT.Discover();
            if (!UpnpDiscovered)
            {
                Console.WriteLine("UPnP discovery failed.");
            }
            return UpnpDiscovered;
        }

        public IPAddress GetIP()
        {
            switch(m_method)
            {
                case IpDetectionMethod.UPnP:
                {
                    try
                    {
                        // check for UPnP devices
                        if (UpnpDiscovered == false)
                        {
                            UpnpDiscovery();
                        }

                        // get ip
                        IPAddress temp = UPnP.NAT.GetExternalIP();
                        if (temp.Equals(IPAddress.Any))
                        {
                            Console.WriteLine("UPnP IP detection failed. No internet connection.");
                            return null;
                        }
                        else
                        {
                            return temp;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("UPnP IP detection failed.");
                        return null;
                    }
                }
                case IpDetectionMethod.URL:
                    try
                    {
                        WebClient client = new WebClient();
                        string temp = client.DownloadString("http://www.whatismyip.com/automation/n09230945.asp");
                        return IPAddress.Parse(temp);
                    }
                    catch
                    {
                        Console.WriteLine("URL IP detection failed.");
                        return null;
                    }
                default:
                    return null;
            }
        }
    }
}
