using UnityEngine;

public struct CircleLayout
{ 
    public static Vector3[] GetCirclePositions(int count, float radius, float modRadius)
    {
        Vector3[] temp = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            Vector2 vec = new Vector3();

            vec.x = (radius + (modRadius * count - 1)) * Mathf.Sin(i * (Mathf.PI / (count / 2f)));
            vec.y = (radius + (modRadius * count - 1)) * Mathf.Cos(i * (Mathf.PI / (count / 2f)));

            temp[i] = vec;
        }
        return temp;
    }
}
