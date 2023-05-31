using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text coin;
    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        coin.text = coins.ToString();
    }
    public void Playing()
    {
        SceneManager.LoadScene(0);
    }
}
