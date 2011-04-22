/* MD5 */
protected Digest CalculateMD5Value()
{
    Digest digest = new Digest();
    byte[] bMsg = this.CreatePaddedBuffer();
    uint num = (uint) ((bMsg.Length * 8) / 0x20);
    for (uint i = 0; i < (num / 0x10); i++)
    {
        this.CopyBlock(bMsg, i);
        this.PerformTransformation(ref digest.A, ref digest.B, ref digest.C, ref digest.D);
    }
    return digest;
}

protected byte[] CreatePaddedBuffer()
{
    int num4 = 0x1c0 - ((this.m_byteInput.Length * 8) % 0x200);
    uint num = (uint) ((num4 + 0x200) % 0x200);
    if (num == 0)
    {
        num = 0x200;
    }
    uint num3 = (uint) ((this.m_byteInput.Length + (num / 8)) + ((ulong) 8L));
    ulong num2 = (ulong) (this.m_byteInput.Length * 8L);
    byte[] buffer = new byte[num3];
    for (int i = 0; i < this.m_byteInput.Length; i++)
    {
        buffer[i] = this.m_byteInput[i];
    }
    buffer[this.m_byteInput.Length] = (byte) (buffer[this.m_byteInput.Length] | 0x80);
    for (int j = 8; j > 0; j--)
    {
        buffer[(int) ((IntPtr) (num3 - j))] = (byte) ((num2 >> ((8 - j) * 8)) & ((ulong) 0xffL));
    }
    return buffer;
}

protected void CopyBlock(byte[] bMsg, uint block)
{
    block = block << 6;
    for (uint i = 0; i < 0x3d; i += 4)
    {
        this.X[i >> 2] = (uint) ((((bMsg[(int) ((IntPtr) (block + (i + 3)))] << 0x18) | (bMsg[(int) ((IntPtr) (block + (i + 2)))] << 0x10)) | (bMsg[(int) ((IntPtr) (block + (i + 1)))] << 8)) | bMsg[block + i]);
    }
}


protected void PerformTransformation(ref uint A, ref uint B, ref uint C, ref uint D)
{
    uint num = A;
    uint num2 = B;
    uint num3 = C;
    uint num4 = D;
    this.TransF(ref A, B, C, D, 0, 7, 1);
    this.TransF(ref D, A, B, C, 1, 12, 2);
    this.TransF(ref C, D, A, B, 2, 0x11, 3);
    this.TransF(ref B, C, D, A, 3, 0x16, 4);
    this.TransF(ref A, B, C, D, 4, 7, 5);
    this.TransF(ref D, A, B, C, 5, 12, 6);
    this.TransF(ref C, D, A, B, 6, 0x11, 7);
    this.TransF(ref B, C, D, A, 7, 0x16, 8);
    this.TransF(ref A, B, C, D, 8, 7, 9);
    this.TransF(ref D, A, B, C, 9, 12, 10);
    this.TransF(ref C, D, A, B, 10, 0x11, 11);
    this.TransF(ref B, C, D, A, 11, 0x16, 12);
    this.TransF(ref A, B, C, D, 12, 7, 13);
    this.TransF(ref D, A, B, C, 13, 12, 14);
    this.TransF(ref C, D, A, B, 14, 0x11, 15);
    this.TransF(ref B, C, D, A, 15, 0x16, 0x10);
    this.TransG(ref A, B, C, D, 1, 5, 0x11);
    this.TransG(ref D, A, B, C, 6, 9, 0x12);
    this.TransG(ref C, D, A, B, 11, 14, 0x13);
    this.TransG(ref B, C, D, A, 0, 20, 20);
    this.TransG(ref A, B, C, D, 5, 5, 0x15);
    this.TransG(ref D, A, B, C, 10, 9, 0x16);
    this.TransG(ref C, D, A, B, 15, 14, 0x17);
    this.TransG(ref B, C, D, A, 4, 20, 0x18);
    this.TransG(ref A, B, C, D, 9, 5, 0x19);
    this.TransG(ref D, A, B, C, 14, 9, 0x1a);
    this.TransG(ref C, D, A, B, 3, 14, 0x1b);
    this.TransG(ref B, C, D, A, 8, 20, 0x1c);
    this.TransG(ref A, B, C, D, 13, 5, 0x1d);
    this.TransG(ref D, A, B, C, 2, 9, 30);
    this.TransG(ref C, D, A, B, 7, 14, 0x1f);
    this.TransG(ref B, C, D, A, 12, 20, 0x20);
    this.TransH(ref A, B, C, D, 5, 4, 0x21);
    this.TransH(ref D, A, B, C, 8, 11, 0x22);
    this.TransH(ref C, D, A, B, 11, 0x10, 0x23);
    this.TransH(ref B, C, D, A, 14, 0x17, 0x24);
    this.TransH(ref A, B, C, D, 1, 4, 0x25);
    this.TransH(ref D, A, B, C, 4, 11, 0x26);
    this.TransH(ref C, D, A, B, 7, 0x10, 0x27);
    this.TransH(ref B, C, D, A, 10, 0x17, 40);
    this.TransH(ref A, B, C, D, 13, 4, 0x29);
    this.TransH(ref D, A, B, C, 0, 11, 0x2a);
    this.TransH(ref C, D, A, B, 3, 0x10, 0x2b);
    this.TransH(ref B, C, D, A, 6, 0x17, 0x2c);
    this.TransH(ref A, B, C, D, 9, 4, 0x2d);
    this.TransH(ref D, A, B, C, 12, 11, 0x2e);
    this.TransH(ref C, D, A, B, 15, 0x10, 0x2f);
    this.TransH(ref B, C, D, A, 2, 0x17, 0x30);
    this.TransI(ref A, B, C, D, 0, 6, 0x31);
    this.TransI(ref D, A, B, C, 7, 10, 50);
    this.TransI(ref C, D, A, B, 14, 15, 0x33);
    this.TransI(ref B, C, D, A, 5, 0x15, 0x34);
    this.TransI(ref A, B, C, D, 12, 6, 0x35);
    this.TransI(ref D, A, B, C, 3, 10, 0x36);
    this.TransI(ref C, D, A, B, 10, 15, 0x37);
    this.TransI(ref B, C, D, A, 1, 0x15, 0x38);
    this.TransI(ref A, B, C, D, 8, 6, 0x39);
    this.TransI(ref D, A, B, C, 15, 10, 0x3a);
    this.TransI(ref C, D, A, B, 6, 15, 0x3b);
    this.TransI(ref B, C, D, A, 13, 0x15, 60);
    this.TransI(ref A, B, C, D, 4, 6, 0x3d);
    this.TransI(ref D, A, B, C, 11, 10, 0x3e);
    this.TransI(ref C, D, A, B, 2, 15, 0x3f);
    this.TransI(ref B, C, D, A, 9, 0x15, 0x40);
    A += num;
    B += num2;
    C += num3;
    D += num4;
}

protected void TransF(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
{
    a = b + MD5Helper.RotateLeft(((a + ((b & c) | (~b & d))) + this.X[k]) + T[(int) ((IntPtr) (i - 1))], s);
}

protected void TransG(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
{
    a = b + MD5Helper.RotateLeft(((a + ((b & d) | (c & ~d))) + this.X[k]) + T[(int) ((IntPtr) (i - 1))], s);
}

protected void TransH(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
{
    a = b + MD5Helper.RotateLeft(((a + ((b ^ c) ^ d)) + this.X[k]) + T[(int) ((IntPtr) (i - 1))], s);
}

protected void TransI(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
{
    a = b + MD5Helper.RotateLeft(((a + (c ^ (b | ~d))) + this.X[k]) + T[(int) ((IntPtr) (i - 1))], s);
}

/* MD5Helper */
public static uint RotateLeft(uint uiNumber, ushort shift)
{
    return ((uiNumber >> (0x20 - shift)) | (uiNumber << shift));
}

/* MD5.Digest */
public Digest()
{
    this.A = 0x67452301;
    this.B = 0xefcdab89;
    this.C = 0x98badcfe;
    this.D = 0x10325476;
}

