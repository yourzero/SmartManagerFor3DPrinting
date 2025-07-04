using System.Security.Cryptography;

namespace Manager_for_3_D_Printing.Encryption;

public class Hashing
{
    /// <summary>
    /// Computes the SHA-256 hash of the given stream (e.g. a FileStream).
    /// </summary>
    public static string ComputeSha256Hash(Stream dataStream)
    {
        // leave the stream position where you found it
        long origPos = dataStream.CanSeek ? dataStream.Position : 0;
        if (dataStream.CanSeek)
            dataStream.Position = 0;

        using var sha = SHA256.Create();
        byte[] hashBytes = sha.ComputeHash(dataStream);

        if (dataStream.CanSeek)
            dataStream.Position = origPos;

        // .NET 5+ Convert.ToHexString avoids manual formatting
        return Convert.ToHexString(hashBytes);
    }

    /// <summary>
    /// If you already have the bytes in memory, you can pass them here.
    /// You could accept a ReadOnlySpan<byte> instead of byte[] for zero-alloc slicing,
    /// but SHA256 APIs still operate on byte[], so you’d typically do .ToArray() anyway.
    /// </summary>
    public static string ComputeSha256Hash(byte[] data)
    {
        using var sha = SHA256.Create();
        byte[] hashBytes = sha.ComputeHash(data);
        return Convert.ToHexString(hashBytes);
    }
}