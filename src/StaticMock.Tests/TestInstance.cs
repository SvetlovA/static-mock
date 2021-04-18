using System;

namespace StaticMock.Tests
{
    public class TestInstance : IEquatable<TestInstance>
    {
        public int IntProperty { get; set; }
        public object ObjectProperty { get; set; }

        public bool Equals(TestInstance other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IntProperty == other.IntProperty && Equals(ObjectProperty, other.ObjectProperty);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TestInstance) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IntProperty, ObjectProperty);
        }
    }
}
