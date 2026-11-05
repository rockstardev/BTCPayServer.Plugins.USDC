using System.Linq;
using Nethereum.Util;

namespace BTCPayServer.Plugins.USDC.Services;

public static class EVMAddressHelper
{
    public static bool IsValid(string? address)
    {
        if (string.IsNullOrWhiteSpace(address)) return false;
        // quick format check
        if (!AddressUtil.Current.IsValidEthereumAddressHexFormat(address)) return false;
        // Mixed case carries an EIP-55 checksum, which catches a mistyped receiving
        // address before payments go somewhere they cannot be recovered from.
        var hex = address[2..];
        if (hex.Any(char.IsLower) && hex.Any(char.IsUpper) && !AddressUtil.Current.IsChecksumAddress(address)) return false;
        return !string.Equals(address, "0x0000000000000000000000000000000000000000", System.StringComparison.OrdinalIgnoreCase);
    }
}
