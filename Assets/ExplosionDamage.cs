using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplosionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    static bool gameFail = false;
    public GameObject losingText;

    void Start()
    {
        gameFail = false;
    }

    public static bool isGameOver()
    {
        return gameFail;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Explosion collided with " + other.gameObject.tag);
        if (other.gameObject.tag == "NPC")
        {
            losingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Over\nYou exploded a friend :(";
            losingText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(400, losingText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
            losingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            Instantiate(losingText, new Vector3(0, 800, 0), Quaternion.identity);
            gameFail = true;
        }
    }
}
