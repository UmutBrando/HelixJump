using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPannal;
    public GameObject LevelWinPannal;

    public static int currentLevelIndex;
    public static int noOfPassingRings;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    public Image ProgressBar;

    public TextMeshProUGUI scoreText;
    public float score;


    public HelixManager helixManager;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }
    private void Start()
    {
        Time.timeScale = 1;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;
    }
    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPannal.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }



        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        
        
        //float progress = noOfPassingRings* 100 / FindObjectOfType<HelixManager>().noOfRings;
        float progress = Mathf.InverseLerp(0, helixManager.noOfRings, noOfPassingRings);
        ProgressBar.fillAmount = progress;

        if (levelWin)
        {
            Time.timeScale = 1;
            LevelWinPannal.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex",currentLevelIndex+1);
                SceneManager.LoadScene(0);
            }
        }
    }
    public void UpdateScore()
    {
        score += 2.5f;
        scoreText.text = "Score:" + score;
    }
}
