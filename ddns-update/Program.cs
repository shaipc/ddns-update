using System;
using System.Collections.Generic;
using System.Text;

namespace ddns_update
{
    class Program
    {
        static void Main(string[] args)
        {
            IpDetector detector = new IpDetector(IpDetector.IpDetectionMethod.UPnP);

            while (true)
            {
                Console.WriteLine(detector.GetIP());
                Console.ReadKey();
            }

        }
    }
}
