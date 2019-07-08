using System.Net;

namespace IPAddressRange
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var referenceIPAddress = IPAddress.Parse("192.168.0.1");
            var targetIPAddress = IPAddress.Parse("192.168.0.50");

            var isInSameSubnet = new IPAddressRangeController().IsInSameSubnet(referenceIPAddress, targetIPAddress, 24);
        }
    }
}