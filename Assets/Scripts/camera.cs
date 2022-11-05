using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    public Camera main_camera;
    public float pixelToUnits = 40f;
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, transform.position.z);       // присваиваем позиции камеры позицию объекта, не считая позиции по Z - она остается такой же как и была (для сохранения слоев)
    }
    void Update()
    {
        if (player != null)
        {
            float player_x = player.transform.position.x;
            float player_y = player.transform.position.y + 1.2f;

            float rounded_x = RoundToNearestPixel(player_x);
            float rounded_y = RoundToNearestPixel(player_y);

            Vector3 new_pos = new Vector3(rounded_x, rounded_y, -10.0f); // this is 2d, so my camera is that far from the screen.
            main_camera.transform.position = new_pos;
        }
    }
    public float RoundToNearestPixel(float unityUnits)
    {
        float valueInPixels = unityUnits * pixelToUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
        return roundedUnityUnits;
    }

}
