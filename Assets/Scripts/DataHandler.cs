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

public class DataHandler : MonoBehaviour
{
    public struct Pixel
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

    public RawImage m_RawImage;
    private Texture2D m_Texture;

    public FileInfo f;
    private List<Pixel> pixels = new List<Pixel>();
    
    static private Color getColor(int num_color)
    {
        switch (num_color)
        {
            case 0:
                return new Color32(255,255,255,255);
                break;
            case 1:
                return new Color32(228,228,228,255);
                break;
            case 2:
                return new Color32(136,136,136,255);
                break;
            case 3:
                return new Color32(34,34,34,255);
                break;
            case 4:
                return new Color32(255,167,209,255);
                break;
            case 5:
                return new Color32(229,0,0,255);
                break;
            case 6:
                return new Color32(229,149,0,255);
                break;
            case 7:
                return new Color32(160,106,66,255);
                break;
            case 8:
                return new Color32(229,217,0,255);
                break; 
            case 9:
                return new Color32(148,224,68,255);
                break; 
            case 10:
                return new Color32(2,190,1,255);
                break; 
            case 11:
                return new Color32(0,229,240,255);
                break; 
            case 12:
                return new Color32(0,131,199,255);
                break; 
            case 13:
                return new Color32(0,0,234,255);
                break; 
            case 14:
                return new Color32(224,74,255,255);
                break; 
            case 15:
                return new Color32(130,0,255,255);
                break; 
            default:
                return new Color32(0, 0, 0,255);
                break;
        }
    }
    
    void readFile()
    {
        BinaryReader binReader = new BinaryReader(File.Open("Assets/data.bin", FileMode.Open));
        long length = (long)binReader.BaseStream.Length;
        while (binReader.BaseStream.Position != length)
        {
            long time = binReader.ReadInt64();  
            int x = binReader.ReadInt16();  
            int y = 1000-binReader.ReadInt16();  
            Color color = getColor(binReader.ReadInt16());
            pixels.Add(new Pixel(time,x,y,color));
        }
    }
    
    IEnumerator draw()
    {
        long i = 0;
        foreach (var pixel in pixels)
        {
            
            m_Texture.SetPixel(pixel.x,pixel.y,pixel.color);
            i++;
            if (i % 100 == 0)
            {
                m_Texture.Apply();
                yield return null;
            }
        }
    }

    async void Start()
    {
        m_Texture = new Texture2D(1000, 1000, TextureFormat.ARGB32, false);
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                m_Texture.SetPixel(i,j,Color.white);
            }
        }
        m_Texture.Apply();
        m_RawImage.texture = m_Texture;
        m_Texture.filterMode = FilterMode.Point;

        readFile();
        pixels = pixels.OrderBy(x => x.time).ToList();
        Debug.Log("done");

        StartCoroutine(draw());
    }
}
