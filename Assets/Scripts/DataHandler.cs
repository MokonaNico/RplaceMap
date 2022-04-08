using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler
{
    public class Pixel
    {
        public Pixel(long time, int x, int y, Color color)
        {
            this.time = time;
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public long time { get; }
        public int x { get; }
        public int y { get; }
        public Color color { get; }
    }

    private static Color[] colorsv1 =
    {
        new Color32(255, 255, 255, 255),
        new Color32(228, 228, 228, 255),
        new Color32(136, 136, 136, 255),
        new Color32(34, 34, 34, 255),
        new Color32(255, 167, 209, 255),
        new Color32(229, 0, 0, 255),
        new Color32(229, 149, 0, 255),
        new Color32(160, 106, 66, 255),
        new Color32(229, 217, 0, 255),
        new Color32(148, 224, 68, 255),
        new Color32(2, 190, 1, 255),
        new Color32(0, 229, 240, 255),
        new Color32(0, 131, 199, 255),
        new Color32(0, 0, 234, 255),
        new Color32(224, 74, 255, 255),
        new Color32(130, 0, 255, 255),
        new Color32(0, 0, 0, 255)
    };
    
    static Color[] colorsv2 =
    {
        new Color32(0x00,0x00,0x00,0xFF),
        new Color32(0x00,0x75,0x6F,0xFF),
        new Color32(0x00,0x9E,0xAA,0xFF),
        new Color32(0x00,0xA3,0x68,0xFF),
        new Color32(0x00,0xCC,0x78,0xFF),
        new Color32(0x00,0xCC,0xC0,0xFF),
        new Color32(0x24,0x50,0xA4,0xFF),
        new Color32(0x36,0x90,0xEA,0xFF),
        new Color32(0x49,0x3A,0xC1,0xFF),
        new Color32(0x51,0x52,0x52,0xFF),
        new Color32(0x51,0xE9,0xF4,0xFF),
        new Color32(0x6A,0x5C,0xFF,0xFF),
        new Color32(0x6D,0x00,0x1A,0xFF),
        new Color32(0x6D,0x48,0x2F,0xFF),
        new Color32(0x7E,0xED,0x56,0xFF),
        new Color32(0x81,0x1E,0x9F,0xFF),
        new Color32(0x89,0x8D,0x90,0xFF),
        new Color32(0x94,0xB3,0xFF,0xFF),
        new Color32(0x9C,0x69,0x26,0xFF),
        new Color32(0xB4,0x4A,0xC0,0xFF),
        new Color32(0xBE,0x00,0x39,0xFF),
        new Color32(0xD4,0xD7,0xD9,0xFF),
        new Color32(0xDE,0x10,0x7F,0xFF),
        new Color32(0xE4,0xAB,0xFF,0xFF),
        new Color32(0xFF,0x38,0x81,0xFF),
        new Color32(0xFF,0x45,0x00,0xFF),
        new Color32(0xFF,0x99,0xAA,0xFF),
        new Color32(0xFF,0xA8,0x00,0xFF),
        new Color32(0xFF,0xB4,0x70,0xFF),
        new Color32(0xFF,0xD6,0x35,0xFF),
        new Color32(0xFF,0xF8,0xB8,0xFF),
        new Color32(0xFF,0xFF,0xFF,0xFF)
    };

    static private Color getColor(int num_color, int colorVersion)
    {
        if (colorVersion == 0)
        {
            return colorsv1[num_color];
        }
        else if (colorVersion == 1)
        {
            return colorsv2[num_color];
        }
        return new Color32(0, 0, 0,255);
    }
    
    public static IEnumerator readFile(string filename, int colorVersion, int max_size, Texture2D m_Texture, int speed)
    {
        BinaryReader binReader = new BinaryReader(File.Open("Assets/"+filename, FileMode.Open));
        long length = (long)binReader.BaseStream.Length;
        long i = 0;
        while (binReader.BaseStream.Position != length)
        {
            int x = max_size - binReader.ReadInt16();  
            int y = binReader.ReadInt16();  
            Color color = getColor(binReader.ReadByte(), colorVersion);

            m_Texture.SetPixel(x,y,color);
            i++;
            if (i % speed == 0)
            {
                m_Texture.Apply();
                yield return null;
            }
        }
        m_Texture.Apply();
    }
}
