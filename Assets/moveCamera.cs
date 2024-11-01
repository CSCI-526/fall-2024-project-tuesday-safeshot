using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        // move the camera in a boxed region from the center of the screen
        float camX = math.abs(player.transform.position.x) / (player.transform.position.x) * math.min(1, math.abs(player.transform.position.x));
        float camY = math.abs(player.transform.position.y) / (player.transform.position.y) * math.min(1, math.abs(player.transform.position.y));
        transform.position = new Vector3(camX, camY, transform.position.z);
    }
}
