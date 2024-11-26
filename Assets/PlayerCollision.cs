using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject losingText;
    private static bool gameOver = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "lava")
        {
            Debug.Log("Game Over");
            if (!gameOver)
            {
                gameOver = true;
                PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
                if (gameObject.scene.name == "Level5" || gameObject.scene.name == "Level4")
                { pauseMenuController.ShowGamePauseMenu(false, "Game Over!\nYou died by Lava!"); }
                else
                {
                    pauseMenuController.ShowGamePauseMenu(false, "Game Over!\nYou died by Spikes!");
                }
                pauseMenuController.ShowGamePauseMenu(false, "Game Over!\nYou died by Lava!");
                pauseMenuController.EndGame();
            }
        }
    }
}
