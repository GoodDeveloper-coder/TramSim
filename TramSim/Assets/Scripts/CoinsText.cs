using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    private TMP_Text _coinsText;

    void Start()
    {
        _coinsText = GetComponent<TMP_Text>();    
    }

    void Update()
    {
        _coinsText.text = "Coins: " + GameManager._instance._coins;
    }
}
