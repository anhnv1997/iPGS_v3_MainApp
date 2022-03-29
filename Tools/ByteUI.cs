using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Tools
{
    public class ByteUI
    {
        // Convert from Decimal to Base
        private const int base10 = 10;
        private static char[] cHexa = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        private static int[] iHexaNumeric = new int[] { 10, 11, 12, 13, 14, 15 };
        private static int[] iHexaIndices = new int[] { 0, 1, 2, 3, 4, 5 };
        private const int asciiDiff = 48;

        public static string DecimalToBase(int iDec, int numbase)
        {
            try
            {
                string strBin = "";
                int[] result = new int[32];
                int MaxBit = 32;
                for (; iDec > 0; iDec /= numbase)
                {
                    int rem = iDec % numbase;
                    result[--MaxBit] = rem;
                }
                for (int i = 0; i < result.Length; i++)
                    if ((int)result.GetValue(i) >= base10)
                        strBin += cHexa[(int)result.GetValue(i) % base10];
                    else
                        strBin += result.GetValue(i);
                strBin = strBin.Substring(30, 2);
                return strBin;
            }
            catch
            {
                return "0";
            }
        }

        public static int Asc(string c1)
        {
            char c = Convert.ToChar(c1);
            int converted = c;
            if (converted >= 0x80)
            {
                byte[] buffer = new byte[2];
                // if the resulting conversion is 1 byte in length, just use the value
                if (System.Text.Encoding.Default.GetBytes(new char[] { c }, 0, 1, buffer, 0) == 1)
                {
                    converted = buffer[0];
                }
                else
                {
                    // byte swap bytes 1 and 2;
                    converted = buffer[0] << 16 | buffer[1];
                }
            }
            return converted;
        }


        // convert from array buffer to array string
        public static string[] Get_Message(byte[] bytes, int count, ref string viewraw)
        {
            string[] message = new string[count];
            for (int i = 0; i < count; i++)
            {
                message[i] = DecimalToBase(bytes[i], 16);
                if (viewraw == "")
                    viewraw = message[i];
                else
                    viewraw = viewraw + " " + message[i];
            }
            return message;
        }

        // Ma hoa du lieu
        public static string Disorder(string szData)
        {
            int I, nValue, nChar;
            string temp = "";
            if (szData != "")
            {
                for (I = 0; I < szData.Length; I++)
                {
                    int i = I + 1;
                    nChar = Asc(szData.Substring(I, 1));
                    nValue = nChar ^ (i * 3);
                    if (nChar % 2 == 0)
                        temp = temp + DecimalToBase(nValue, 16).Substring(DecimalToBase(nValue + 256, 16).Length - 2, 2);
                    else
                        temp = DecimalToBase(nValue, 16).Substring(DecimalToBase(nValue + 256, 16).Length - 2, 2) + temp;
                }
            }
            return temp;
        }
    }

    public class CRC16
    {
        const ushort polynomial = 0xA001;
        ushort[] table = new ushort[256];

        public ushort ComputeChecksum(byte[] bytes, int start, int stop)
        {
            ushort crc = 0;
            crc = 0xFFFF;
            for (int i = start; i < stop; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            ushort crc = ComputeChecksum(bytes, 0, bytes.Length);
            return BitConverter.GetBytes(crc);
        }

        public CRC16()
        {
            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
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
                table[i] = value;
            }
        }
    }
}