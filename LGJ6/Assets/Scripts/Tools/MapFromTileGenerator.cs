using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapFromTileGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        string mapa = "";

        // Normalna wersja
        //mapa += "{";
        //for (int i = 0; i < 24; i++)
        //{
        //    mapa += "{";
        //    for (int j = 0; j < 24; j++)
        //    {
        //        mapa += ReturnValuer(j, -i); mapa += ",";
        //    }
        //    mapa += "},";
        //}
        //mapa += "},";

        // Wersja lustrzania
        mapa += "{";
        for (int i = 23; i > -1; i--)
        {
            mapa += "{";
            for (int j = 23; j > -1; j--)
            {
                mapa += ReturnValuer(j, -i); mapa += ",";
            }
            mapa += "},";
        }
        mapa += "},";
        Debug.Log(mapa);
    }

    int ReturnValuer(int i,int j)
    {
        Sprite sprite = tilemap.GetSprite(new Vector3Int(i, j, 0));
        for (int k = 0; k < MapBuilder.Instance.bioms[0].sprites.Length; k++)
        {
            
            if (MapBuilder.Instance.bioms[0].sprites[k].GetComponentInChildren<SpriteRenderer>().sprite == sprite) return k;
        }
        Debug.Log(sprite.name);
        return 5;
    }
}
