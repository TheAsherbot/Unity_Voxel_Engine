using System;

using UnityEngine;

public struct BitArray
{


    public BitArray(byte size)
    {
        bitArray = new Bit[size];
    }


    private Bit[] bitArray;


    public string GetBinaryValue_String()
    {
        string binaryValue = "";
        for (int i = 0; i < bitArray.Length; i++)
        {
            binaryValue += bitArray[i];
        }
        return binaryValue;
    }
    public byte GetValue_Byte()
    {
        byte bitArrayValue = 0;
        byte maxNumberOfValues = Convert.ToByte(bitArray.Length > 8 ? 8 : bitArray.Length);
        for (int i = 0; i < maxNumberOfValues; i++)
        {
            bitArrayValue += Convert.ToByte(Mathf.RoundToInt(Mathf.Pow(2, i)) * bitArray[i]);
        }
        return bitArrayValue;
    }
    public short GetValue_Short()
    {
        short bitArrayValue = 0;
        byte maxNumberOfValues = Convert.ToByte(bitArray.Length > 16 ? 16 : bitArray.Length);
        for (int i = 0; i < maxNumberOfValues; i++)
        {
            bitArrayValue += Convert.ToInt16(Mathf.RoundToInt(Mathf.Pow(2, i)) * bitArray[i]);
        }
        return bitArrayValue;
    }
    public int GetValue_Int()
    {
        int bitArrayValue = 0;
        byte maxNumberOfValues = Convert.ToByte(bitArray.Length > 32 ? 32 : bitArray.Length);
        for (int i = 0; i < maxNumberOfValues; i++)
        {
            bitArrayValue += Convert.ToInt32(Mathf.RoundToInt(Mathf.Pow(2, i)) * bitArray[i]);
        }
        return bitArrayValue;
    }
    public long GetValue_Long()
    {
        long bitArrayValue = 0;
        byte maxNumberOfValues = Convert.ToByte(bitArray.Length > 64 ? 64 : bitArray.Length);
        for (int i = 0; i < maxNumberOfValues; i++)
        {
            bitArrayValue += Convert.ToInt64(Mathf.RoundToInt(Mathf.Pow(2, i)) * bitArray[i]);
        }
        return bitArrayValue;
    }


    public void SetBit(byte index, Bit bit)
    {
        bitArray[index] = bit;
    }
    
    public Bit GetBit(byte index)
    {
        return bitArray[index];
    }


    public override string ToString()
    {
        return GetBinaryValue_String();
    }

}
