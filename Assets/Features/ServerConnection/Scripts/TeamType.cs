namespace Features.ServerConnection.Scripts
{
    public enum TeamType : byte
    {
        AutoAssign = 0,
        Blue = 1,
        Red = 2,
        None = byte.MaxValue
    }
}