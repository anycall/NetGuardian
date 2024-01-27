using System.Net;

namespace NetGuardian;

public static class IPHelper
{

    private static readonly List<(long, long)> ReservedIPRanges = new List<(long, long)>
	{
		(ToLongIPAddress("0.0.0.0"), ToLongIPAddress("0.255.255.255")),
		(ToLongIPAddress("10.0.0.0"), ToLongIPAddress("10.255.255.255")),
        // Add other reserved ranges here...
    };

    /// <summary>
    /// 将传入的opts地址转换为IP地址集合
    /// </summary>
    /// <param name="opts">元素的格式可能为以下几种：xxx.xxx.xxx.xxx,xxx.xxx.xxx.x/xx,xxx.xxx.xxx.xxx-xxx</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string[] Convert(IEnumerable<string> opts)
    {


        throw new NotImplementedException();
    }

    /// <summary>
    /// 一个合法的IPv4地址由四个数字组成，每个数字在0到255之间，数字之间用点（.）分隔。例如，"192.168.1.1"就是一个合法的IPv4地址。
    /// 每个数字代表一个8位的二进制数（也称为一个字节），所以IPv4地址总共有32位。这意味着IPv4地址的范围从"0.0.0.0"到"255.255.255.255"。
    /// 注意，虽然理论上所有这些地址都是有效的，但某些地址被保留用于特殊用途，例如私有网络（如192.168.0.0/16）或者自我分配的IP（如169.254.0.0/16），并且不应被用于公共互联网。
    /// IPv6地址是一个更新的版本，由8组16位的十六进制数组成，每组之间用冒号（:）分隔。例如，"2001:0db8:85a3:0000:0000:8a2e:0370:7334"是一个合法的IPv6地址。
    /// </summary>
    /// <param name="ipString">仅支持IPv4</param>
    /// <returns></returns>
    public static List<string> ConvertIPArea(string ipString)
    {
        var parts = ipString.Split('-');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid input format.");
        }

        var ipStartParts = parts[0].Split('.');
        if (ipStartParts.Length != 4)
        {
            throw new ArgumentException("Invalid IP address format.");
        }

        if (!int.TryParse(ipStartParts[3], out int start) || start < 0 || start > 255)
        {
            throw new ArgumentException("Invalid start range.");
        }

        if (!int.TryParse(parts[1], out int end) || end < 0 || end > 255)
        {
            throw new ArgumentException("Invalid end range.");
        }

        var ipAddresses = new List<string>();
        for (int i = start; i <= end; i++)
        {
            var ip = $"{ipStartParts[0]}.{ipStartParts[1]}.{ipStartParts[2]}.{i}";
            if (IPAddress.TryParse(ip, out var ipAddress) && !IsReserved(ipAddress))
            {
                ipAddresses.Add(ip);
            }
        }

        return ipAddresses;

    }



    private static bool IsReserved(IPAddress ipAddress)
    {
        var longIP = ToLongIPAddress(ipAddress.ToString());
        foreach (var range in ReservedIPRanges)
        {
            if (longIP >= range.Item1 && longIP <= range.Item2)
            {
                return true;
            }
        }

        return false;
    }

    private static long ToLongIPAddress(string ipAddress)
    {
        var parts = ipAddress.Split('.');
        return (long.Parse(parts[0]) << 24) + (long.Parse(parts[1]) << 16) + (long.Parse(parts[2]) << 8) + long.Parse(parts[3]);
    }
}