using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public Renderer m_Renderer;
    private Texture2D m_Texture;

    public string filename;
    public int color_version = 0;
    public int max_size = 1000;
    public int speed = 1000;

    void Start()
    {
        m_Texture = new Texture2D(max_size, max_size, TextureFormat.ARGB32, false);
        for (int i = 0; i < max_size; i++)
        {
            for (int j = 0; j < max_size; j++)
            {
                m_Texture.SetPixel(i,j,Color.white);
            }
        }
        m_Texture.Apply();
        m_Renderer.material.mainTexture = m_Texture;
        m_Texture.filterMode = FilterMode.Point;
        
        StartCoroutine(DataHandler.readFile(filename, color_version, max_size, m_Texture, speed));
    }
}
