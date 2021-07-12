using System;

namespace TerrariaProtocol.DataStructures
{
    // Token: 0x02000142 RID: 322
    public struct Point16
    {

        // Token: 0x06000D96 RID: 3478 RVA: 0x0039C825 File Offset: 0x0039AA25
        public Point16(int X, int Y)
        {
            this.X = (short)X;
            this.Y = (short)Y;
        }

        // Token: 0x06000D97 RID: 3479 RVA: 0x0039C837 File Offset: 0x0039AA37
        public Point16(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }

        // Token: 0x06000D98 RID: 3480 RVA: 0x0039C847 File Offset: 0x0039AA47
        public static Point16 Max(int firstX, int firstY, int secondX, int secondY)
        {
            return new Point16((firstX > secondX) ? firstX : secondX, (firstY > secondY) ? firstY : secondY);
        }

        // Token: 0x06000D99 RID: 3481 RVA: 0x0039C85E File Offset: 0x0039AA5E
        public Point16 Max(int compareX, int compareY)
        {
            return new Point16(((int)this.X > compareX) ? ((int)this.X) : compareX, ((int)this.Y > compareY) ? ((int)this.Y) : compareY);
        }

        // Token: 0x06000D9A RID: 3482 RVA: 0x0039C889 File Offset: 0x0039AA89
        public Point16 Max(Point16 compareTo)
        {
            return new Point16((this.X > compareTo.X) ? this.X : compareTo.X, (this.Y > compareTo.Y) ? this.Y : compareTo.Y);
        }

        // Token: 0x06000D9B RID: 3483 RVA: 0x0039C8C8 File Offset: 0x0039AAC8
        public static bool operator ==(Point16 first, Point16 second)
        {
            return first.X == second.X && first.Y == second.Y;
        }

        // Token: 0x06000D9C RID: 3484 RVA: 0x0039C8E8 File Offset: 0x0039AAE8
        public static bool operator !=(Point16 first, Point16 second)
        {
            return first.X != second.X || first.Y != second.Y;
        }

        // Token: 0x06000D9D RID: 3485 RVA: 0x0039C90C File Offset: 0x0039AB0C
        public override bool Equals(object obj)
        {
            Point16 point = (Point16)obj;
            return this.X == point.X && this.Y == point.Y;
        }

        // Token: 0x06000D9E RID: 3486 RVA: 0x0039C93F File Offset: 0x0039AB3F
        public override int GetHashCode()
        {
            return (int)this.X << 16 | (int)((ushort)this.Y);
        }

        // Token: 0x06000D9F RID: 3487 RVA: 0x0039C952 File Offset: 0x0039AB52
        public override string ToString()
        {
            return string.Format("{{{0}, {1}}}", this.X, this.Y);
        }

        // Token: 0x06000DA0 RID: 3488 RVA: 0x0039C974 File Offset: 0x0039AB74
        // Note: this type is marked as 'beforefieldinit'.
        static Point16()
        {
        }

        // Token: 0x04002BD9 RID: 11225
        public readonly short X;

        // Token: 0x04002BDA RID: 11226
        public readonly short Y;

        // Token: 0x04002BDB RID: 11227
        public static Point16 Zero = new Point16(0, 0);

        // Token: 0x04002BDC RID: 11228
        public static Point16 NegativeOne = new Point16(-1, -1);
    }
}
