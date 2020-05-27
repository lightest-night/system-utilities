using System;

namespace LightestNight.System.Utilities.Generators
{
    /// <summary>
    /// Used for generating UUID based on RFC 4122.
    /// </summary>
    /// <seealso href="http://www.ietf.org/rfc/rfc4122.txt">RFC 4122 - A Universally Unique Identifier (UUID) URN Namespace</seealso>
    public static class GuidGenerator
    {
        private const int ByteArraySize = 16;
        
        private const int VariantByte = 8;
        private const int VariantByteMask = 0x3f;
        private const int VariantByteShift = 0x80;
        
        private const int VersionByte = 7;
        private const int VersionByteMask = 0x0f;
        private const int VersionByteShift = 4;

        private const byte TimestampByte = 0;
        private const byte GuidClockSequenceByte = 8;
        private const byte NodeByte = 10;
        
        // Offset to move from 1/1/0001, which is 0-time for .Net, to gregorian 0-time of 15/10/1582
        private static readonly DateTimeOffset GregorianCalendarStart = new DateTimeOffset(1582, 10, 15, 0, 0, 0, TimeSpan.Zero);

        private static readonly byte[] DefaultClockSequence = new byte[2];
        private static readonly byte[] DefaultNode = new byte[6];

        static GuidGenerator()
        {
            var random = new Random();
            random.NextBytes(DefaultClockSequence);
            random.NextBytes(DefaultNode);
        }

        public static GuidVersion GetVersion(this Guid id)
        {
            var bytes = id.ToByteArray();
            return (GuidVersion) ((bytes[VersionByte] & 0xFF) >> VersionByteShift);
        }

        public static DateTimeOffset GetDateTimeOffset(Guid id)
        {
            var bytes = id.ToByteArray();
            bytes[VersionByte] &= VersionByteMask;
            bytes[VersionByte] |= (byte) GuidVersion.TimeBased >> VersionByteShift;

            var timestampBytes = new byte[8];
            Array.Copy(bytes, TimestampByte, timestampBytes, 0, 8);

            var timestamp = BitConverter.ToInt64(timestampBytes, 0);
            var ticks = timestamp + GregorianCalendarStart.Ticks;
            
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }

        public static DateTime GetDateTime(Guid id)
            => GetDateTimeOffset(id).DateTime;

        public static DateTime GetLocalDateTime(Guid id)
            => GetDateTimeOffset(id).LocalDateTime;

        public static DateTime GetUtcDateTime(Guid id)
            => GetDateTimeOffset(id).UtcDateTime;

        public static Guid GenerateTimeBasedGuid()
            => GenerateTimeBasedGuid(DateTimeOffset.UtcNow, DefaultClockSequence, DefaultNode);

        public static Guid GenerateTimeBasedGuid(DateTime dateTime)
            => GenerateTimeBasedGuid(dateTime, DefaultClockSequence, DefaultNode);

        public static Guid GenerateTimeBasedGuid(DateTimeOffset dateTime)
            => GenerateTimeBasedGuid(dateTime, DefaultClockSequence, DefaultNode);

        private static Guid GenerateTimeBasedGuid(DateTime dateTime, byte[] clockSequence, byte[] node)
            => GenerateTimeBasedGuid(new DateTimeOffset(dateTime), clockSequence, node);

        private static Guid GenerateTimeBasedGuid(DateTimeOffset dateTime, byte[] clockSequence, byte[] node)
        {
            const string clockSequenceMemberName = nameof(clockSequence);
            const string nodeMemberName = nameof(node);
            if (clockSequence == null) throw new ArgumentNullException(clockSequenceMemberName);
            if (node == null) throw new ArgumentNullException(nodeMemberName);
            if (clockSequence.Length != 2) throw new ArgumentOutOfRangeException(clockSequenceMemberName, $"The {clockSequenceMemberName} must be 2 bytes");
            if (node.Length != 6) throw new ArgumentOutOfRangeException(nodeMemberName, $"The {nodeMemberName} must be 6 bytes");

            var ticks = (dateTime - GregorianCalendarStart).Ticks;
            var guid = new byte[ByteArraySize];
            var timestamp = BitConverter.GetBytes(ticks);
            
            Array.Copy(node, 0, guid, NodeByte, Math.Min(6, node.Length));
            Array.Copy(clockSequence, 0, guid, GuidClockSequenceByte, Math.Min(2, clockSequence.Length));
            Array.Copy(timestamp, 0, guid, TimestampByte, Math.Min(8, timestamp.Length));

            guid[VariantByte] &= VariantByteMask;
            guid[VariantByte] |= VariantByteShift;

            guid[VersionByte] &= VersionByteMask;
            guid[VersionByte] |= (byte) GuidVersion.TimeBased << VersionByteShift;

            return new Guid(guid);
        }
    }

    public enum GuidVersion
    {
        TimeBased = 0x01,
        Reserved = 0x02,
        NameBased = 0x03,
        Random = 0x04
    }
}