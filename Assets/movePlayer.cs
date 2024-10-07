using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class movePlayer : MonoBehaviour
{
    public Camera camera;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    private int bulletCount = 0;

    public TextMeshProUGUI bulletCountText;

    public int bulletLimit = 10;

    public Transform obj;

    public Transform gun;

    public GameObject losingText;
    public float force = 5f;

    Vector2 playerPos;
    Vector2 mousePos;
    float mousePosY, mousePosX;

    private Rigidbody2D rb;
    private Vector3 direction;

    private bool gameOver = false;

    private float offsetDistance = 0f;

    private PauseMenuController pauseMenuController;
    void Awake()
    {
        InitializePauseMenuController();
    }

    void InitializePauseMenuController()
    {
        pauseMenuController = FindObjectOfType<PauseMenuController>();
        if (pauseMenuController == null)
        {
            Debug.LogError("PauseMenuController not found in the scene!");
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        // set bullet collision gameOver to false
        BulletCollision.setGameOver(false);

        rb = GetComponent<Rigidbody2D>();
        bulletCountText.text = "Bullets: " + (bulletLimit - bulletCount) + "/" + bulletLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        if (BulletCollision.isGameOver())
        {
            gameOver = true;
        }
        if (ExitDoor.isGameOver())
        {
            gameOver = true;
        }

        // Debug.Log("Bullet count: " + bulletCount);
        // Debug.Log("Velocity: " + rb.velocity.magnitude);
        // as the velocity may be a very small number when standing on a dynamic rigidbody, set a low threshold to detect if the player is standing still
        // may need to fix this in the future
        if (bulletCount >= bulletLimit && rb.velocity.magnitude < 0.00001f)
        {
            Debug.Log("Game Over as you have no bullets left");
            gameOver = true;
            Instantiate(losingText, new Vector3(0, 800, 0), Quaternion.identity);
        }
        
        // If paused, do not allow shooting
        if (pauseMenuController != null && pauseMenuController.IsPaused())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletCount < bulletLimit)
            {
                playerPos = obj.transform.position;//gets player position
                mousePos = Input.mousePosition;//gets mouse postion
                mousePos = camera.ScreenToWorldPoint(mousePos);
                mousePosX = mousePos.x - playerPos.x;//gets the distance between object and mouse position for x
                mousePosY = mousePos.y - playerPos.y;//gets the distance between object and mouse position for y  if you want this.
                float dirX = mousePosX / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));
                float dirY = mousePosY / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));

                Vector2 accelerationForce = new Vector2(-dirX, -dirY) * force;
                rb.AddForce(accelerationForce, ForceMode2D.Impulse);


                ShootBullet();
                bulletCountText.text = "Bullets: " + (bulletLimit - bulletCount) + "/" + bulletLimit;
            }

        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        gun.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void ShootBullet()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're in 2D

        // Get the player's position (where the bullet will spawn)
        Vector3 playerPosition = transform.position;

        // Calculate the directionion
        Vector2 direction = (mousePosition - playerPosition).normalized;
        Vector3 direction3 = new Vector3(direction.x, direction.y, 0);

        Vector3 bulletSpawnPosition = playerPosition + direction3 * offsetDistance;


        // Instantiate the bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        // Pass the direction to the player's controller
        // if (playerController != null)
        // {
        //     playerController.ReceiveDirection(direction);
        // }

        // Increment the bullet count; maybe we should do this by calling a function in the game manager
        bulletCount++;
        Debug.Log("Bullet count: " + bulletCount);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2f);
    }

}
