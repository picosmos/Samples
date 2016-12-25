namespace WPF_Radial_Menu
{
    using System;

    public struct RadialMenuField
    {
        public RadialMenuField(Int32 ring, Int32 segment) : this()
        {
            this.Ring = ring;
            this.Segment = segment;
        }

        public Int32 Ring { get; }

        public Int32 Segment { get; }

        public override String ToString()
        {
            return $"{(Char)(this.Segment + 'A')}{this.Ring}";
        }

        public override Int32 GetHashCode()
        {
            return this.Ring.GetHashCode() * 13 ^ this.Segment.GetHashCode();
        }

        public override Boolean Equals(Object obj)
        {
            var rmf = obj as RadialMenuField?;
            return rmf != null && rmf.Value.Ring == this.Ring && rmf.Value.Segment == this.Segment;
        }
    }
}