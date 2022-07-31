using UnityEngine;

public struct TileCreator 
{    
    public static TileScript[] CreateTileMap(TileMapCreateParameters parameters, Transform transform)
    {
        TileScript[] temp = new TileScript[parameters.TileCount.x * parameters.TileCount.y];

        float middle = parameters.FillCenter ? (parameters.TileCount.x - 1 + parameters.TileCount.y - 1) / 2f : 0;

        for (int i = 0; i < parameters.TileCount.x; i++)
        {
            for (int j = 0; j < parameters.TileCount.y; j++)
            {
                Vector3 pos = transform.localPosition;
                pos += new Vector3((i - j) * parameters.Offset.x, 
                    (middle - (i + j)) * parameters.Offset.y, 
                    1f - (i + j) / (float)(parameters.TileCount.x + parameters.TileCount.y));

                TileScript tileScript = GameObject.Instantiate(parameters.TilePrefab, pos, Quaternion.identity, transform);
                tileScript.Initialize((i * parameters.TileCount.y) + j);

                temp[(i * parameters.TileCount.y) + j] = tileScript;                
            }
        }
        return temp;
    }
}
