using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVConverter
{
    public static string[] SerializeCSVNote(TextAsset csvData)
    {
        string[] lineArray = csvData.text.Replace("\n", string.Empty).TrimEnd("\r"[0]).Split("\r"[0]);
        return lineArray[0].Split(',');
    }

    public static string[] SerializeCSVType(TextAsset csvData)
    {
        string[] lineArray = csvData.text.Replace("\n", string.Empty).TrimEnd("\r"[0]).Split("\r"[0]);
        return lineArray[1].Split(',');
    }

    public static string[] SerializeCSVParameter(TextAsset csvData)
    {
        string[] lineArray = csvData.text.Replace("\n", string.Empty).TrimEnd("\r"[0]).Split("\r"[0]);
        return lineArray[2].Split(',');
    }

    public static string[][] SerializeCSVData(TextAsset csvData)
    {
        //AES
        //string text = AES.Decrypt_CBC(csvData.text, "1234567890123456", "zombiestestIV123");
        //string[] lineArray = text.Split("\n"[0]);

        //Normal
        string text = csvData.text;
        string[] lineArray = text.Replace("\n", string.Empty).TrimEnd("\r"[0]).Split("\r"[0]);

        string[][] csv;
        csv = new string[lineArray.Length - 3][];
        for (int i = 0; i < lineArray.Length - 3; i++)
        {
            csv[i] = lineArray[i + 3].Split(',');
        }
        return csv;
    }

    public static T[] ConvertToArray<T>(string value)
    {
        string[] temp = value.Split(';');
        int arrayLength = 0;

        for (int cnt = 0; cnt < temp.Length; cnt++)
        {
            if (string.IsNullOrEmpty(temp[cnt]))
            {
                continue;
            }
            arrayLength++;
        }

        T[] array = new T[arrayLength];
        int pointer = 0;
        for (int cnt = 0; cnt < temp.Length; cnt++)
        {
            if (string.IsNullOrEmpty(temp[cnt]))
                continue;
            array[pointer] = (T)Convert.ChangeType(temp[cnt], typeof(T));
            pointer++;
        }

        return array;

    }

    public static T[][] ConvertToMultiArray<T>(string value)
    {
        string[] temp = value.Split(';');
        int arrayLength1 = 0;
        int arrayLength2 = 0;

        for (int cnt = 0; cnt < temp.Length; cnt++)
        {
            if (string.IsNullOrEmpty(temp[cnt]))
            {
                continue;
            }
            arrayLength1++;
        }

        string[] temp2 = temp[0].Split('/');
        for (int cnt2 = 0; cnt2 < temp2.Length; cnt2++)
        {
            if (string.IsNullOrEmpty(temp2[cnt2]))
            {
                continue;
            }
            arrayLength2++;
        }

        T[][] array = new T[arrayLength1][];

        for (int cnt = 0; cnt < temp.Length; cnt++)
        {
            if (string.IsNullOrEmpty(temp[cnt]))
                continue;

            string[] tempSub = temp[cnt].Split('/');
            array[cnt] = new T[tempSub.Length];

            for (int cnt2 = 0; cnt2 < tempSub.Length; cnt2++)
            {
                array[cnt][cnt2] = (T)Convert.ChangeType(tempSub[cnt2], typeof(T));
            }
        }

        return array;
    }

}
