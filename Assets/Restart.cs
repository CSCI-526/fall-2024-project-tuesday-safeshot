using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Restarting game");

        GameObject bullet = GameObject.Find("Bullet(Clone)");

        BulletCollision.gameOver = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
