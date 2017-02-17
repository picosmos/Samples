namespace Koopakiller.Apps.Snake.Portable.Future
{
    using System;

    /// <summary>
    /// This class already exists in the .NET Framework, see
    /// <see href="https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx">here</see>,
    /// but not in the other frameworks.
    /// </summary>
    internal class InvalidEnumArgumentException : ArgumentException
    {
        public InvalidEnumArgumentException(String parameterName, Int32 value, Type enumType) : base($"Invalid Enum value {value} for Enum Type {enumType}", parameterName)
        {
        }
    }
}
