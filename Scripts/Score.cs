using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public Text ScoreText;
    public int Multiply = 25;
    private int score;
    

    private void FixedUpdate()
    {
        score += Multiply / 25;
        ScoreText.text = score.ToString();
    }
}
