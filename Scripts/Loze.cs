using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loze : MonoBehaviour
{
    [SerializeField] Text recordscore;
    private void Start()
    {
        int lastrunscore = PlayerPrefs.GetInt("ScoreMax");
        int recordrunscore = PlayerPrefs.GetInt("RecordScore");
        if(lastrunscore > recordrunscore)
        {
            recordrunscore = lastrunscore;
            PlayerPrefs.SetInt("RecordScore", recordrunscore);
            recordscore.text = recordrunscore.ToString();
        }
        else
        {
            recordscore.text = recordrunscore.ToString();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
