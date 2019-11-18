using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationMap : MonoBehaviour
{
    private int beach_height = 5;
    private int beach_width = 9;
    private int c = 0;

    public GameObject p_tile;
    public Color c1;
    public Color c2;

    void Start()
    {
        GenerateBeach();
    }

   
    void Update()
    {
        
    }

    private void GenerateBeach()
    {
        for (int i = 0; i < beach_height; i++)
        {
            for (int j = 0; j < beach_width; j++)
            {
                GameObject tile = Instantiate(p_tile, new Vector2((float)j - beach_width/2, (float)i - beach_height/2), Quaternion.identity, transform);
                if (c % 2 == 0)
                {
                    tile.GetComponent<SpriteRenderer>().color = c1;
                }
                else
                {
                    tile.GetComponent<SpriteRenderer>().color = c2;
                }
                c++;
            }
        }
    }


}
