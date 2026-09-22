using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text killsText;

    void Update()
    {
        if (playerHealth) hpText.text = playerHealth.hp.ToString();
        killsText.text = GameStats.Kills.ToString();
    }
}