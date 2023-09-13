using System.IO;

namespace Excel2Any
{
    public class CRC
    {
        private static ushort[] crcTable = new ushort[256];
        private static ushort Calculate(byte[] data)
        {
            ushort crc = 0xFFFF;

            for (int i = 0; i < data.Length; i++)
            {
                byte index = (byte)(crc ^ data[i]);
                crc = (ushort)((crc >> 8) ^ crcTable[index]);
            }

            return crc;
        }
        private static void GenerateCRCTable(ushort polynomial)
        {
            for (ushort i = 0; i < 256; i++)
            {
                ushort value = 0;
                ushort temp = i;

                for (byte j = 0; j < 8; j++)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }

                    temp >>= 1;
                }

                crcTable[i] = value;
            }
        }
        public static ushort GetFileCRC(string path)
        {
            // 生成CRC表
            CRC.GenerateCRCTable(0x1021);
            // 计算CRC校验值
            ushort crc = CRC.Calculate(ReadAllBytes(path));
            return crc;
        }

        private static byte[] ReadAllBytes(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                int num = 0;
                long length = fileStream.Length;

                int num2 = (int)length;
                byte[] array = new byte[num2];
                while (num2 > 0)
                {
                    int num3 = fileStream.Read(array, num, num2);

                    num += num3;
                    num2 -= num3;
                }

                return array;
            }
        }
    }
}
