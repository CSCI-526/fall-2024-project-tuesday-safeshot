using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BulletCollision : MonoBehaviour
{
    public GameObject losingText;
    public static bool gameOver = false;
    public static bool isGameOver()
    {
        return gameOver;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an NPC
        if (other.CompareTag("NPC"))
        {
            Debug.Log("Bullet collided with NPC");
            if (!gameOver)
            {
                gameOver = true;
                Instantiate(losingText, new Vector3(0, 0, 0), Quaternion.identity);
            }
            // change the color of the NPC to grey
            other.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (other.CompareTag("BreakableWall"))
        {
            Debug.Log("Bullet collided with Breakable Wall");
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("Bullet collided with " + other.name);
        }

        // Destroy the bullet after it hits anything
        if (other.name != "PlayerObject")
        {
            Destroy(gameObject);
        }
    }
}
