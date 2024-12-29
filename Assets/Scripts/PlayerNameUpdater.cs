using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameUpdater : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    void Awake()
    {
        if(GameManager.Instance != null)
        playerName.text = GameManager.Instance.nameText;
    }

}
