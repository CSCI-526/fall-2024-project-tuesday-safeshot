using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public int maxX;
    public int maxY;

    public int offsetY = 0;
    public int offsetX = 0;

    private Camera cam;
    // Update is called once per frame

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("No Camera component found on this GameObject!");
        }
    }

    void Update()
    {
        // move the camera in a boxed region from the center of the screen
        float camX = math.abs(player.transform.position.x) / (player.transform.position.x) * math.min(maxX, math.abs(player.transform.position.x)) + offsetX;
        float camY = math.abs(player.transform.position.y) / (player.transform.position.y) * math.min(maxY, math.abs(player.transform.position.y)) + offsetY;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < 5 && Input.GetKey(KeyCode.M))
        {
            camX = 0;
            camY = 0;
        } else
        {
            
            if (Input.GetKey(KeyCode.M)) {
                
                if (currentSceneIndex == 5) {
                    cam.orthographicSize = 20;
                    camY = 14;
                }
                if (currentSceneIndex == 6)
                {
                    cam.orthographicSize = 32;
                    camX = 44;
                }
            }
            if (Input.GetKeyUp(KeyCode.M))
            {
                cam.orthographicSize = 5;
            }
        }
        
        transform.position = new Vector3(camX, camY, transform.position.z);
        
    }
}
