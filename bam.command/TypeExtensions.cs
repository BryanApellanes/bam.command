namespace Bam.Command
{
    /// <summary>
    /// Provides extension methods for <see cref="Type"/> used in the command framework.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type is a numeric type (sbyte, byte, short, ushort, int, uint, long, ulong, float, double, or decimal).
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is a numeric type; otherwise false.</returns>
        public static bool IsNumberType(this Type type)
        {
            return type == typeof(sbyte)
                || type == typeof(byte)
                || type == typeof(short)
                || type == typeof(ushort)
                || type == typeof(int)
                || type == typeof(uint)
                || type == typeof(long)
                || type == typeof(ulong)
                || type == typeof(float) 
                || type == typeof(double) 
                || type == typeof(decimal);
        }
    }
}
