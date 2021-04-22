namespace StaticMock.Services.Injection.Entities
{
    internal struct MethodMemoryInfo<TMethodMemoryValue> where TMethodMemoryValue : unmanaged
    {
        public byte Byte1 { get; set; }
        public byte Byte2 { get; set; }
        public TMethodMemoryValue MethodMemoryValue { get; set; }
        public byte Byte1AfterMethod { get; set; }
        public byte Byte2AfterMethod { get; set; }
        public byte Byte3AfterMethod { get; set; }
    }
}