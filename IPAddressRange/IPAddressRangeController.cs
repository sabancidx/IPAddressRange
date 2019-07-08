using System;
using System.Net;

namespace IPAddressRange
{
    public class IPAddressRangeController
    {
        private IPAddress CreateByHostBitLength(int hostPartLength)
        {
            var netPartLength = 32 - hostPartLength;

            var binaryMask = new byte[4];

            for (var i = 0; i < 4; i++)
            {
                if (i * 8 + 8 <= netPartLength)
                {
                    binaryMask[i] = 255;
                }
                else if (i * 8 > netPartLength)
                {
                    binaryMask[i] = 0;
                }
                else
                {
                    var oneLength = netPartLength - i * 8;
                    var binaryDigit = string.Empty.PadLeft(oneLength, '1').PadRight(8, '0');
                    binaryMask[i] = Convert.ToByte(binaryDigit, 2);
                }
            }

            return new IPAddress(binaryMask);
        }

        private IPAddress GetNetworkAddress(IPAddress ipAddress, IPAddress subnetMask)
        {
            var ipAddressBytes = ipAddress.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();

            var broadcastAddress = new byte[ipAddressBytes.Length];

            for (var i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAddressBytes[i] & subnetMaskBytes[i]);
            }

            return new IPAddress(broadcastAddress);
        }

        public bool IsInSameSubnet(IPAddress referenceIPAdress, IPAddress targetIPAdress, int netBitLength)
        {
            var ipSubnetAddress = CreateByHostBitLength(32 - netBitLength);
            var referenceNetwork = GetNetworkAddress(referenceIPAdress, ipSubnetAddress);
            var targetNetwork = GetNetworkAddress(targetIPAdress, ipSubnetAddress);

            return referenceNetwork.Equals(targetNetwork);
        }
    }
}