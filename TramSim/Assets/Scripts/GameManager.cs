using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _coins { get; private set; }
    public static GameManager _instance { get; private set; }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddCoins(int coins)
    {
        _coins += coins;
    }

    public void RemoveCoins(int coins)
    {
        _coins -= coins;
    }
}
