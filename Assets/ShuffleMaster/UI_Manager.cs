using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : Singleton<UI_Manager>
{
    [SerializeField] private GameObject playMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject tapToStartMenu;
    [SerializeField] private Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWin()
    {
        playMenu.SetActive(false);
        loseMenu.SetActive(false);
        winMenu.SetActive(true);
    }

    private void OnLose()
    {
        playMenu.SetActive(false);
        loseMenu.SetActive(true);
        winMenu.SetActive(false);
    }

    private void OnPlay()
    {
        playMenu.SetActive(true);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        tapToStartMenu.SetActive(false);
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score:" + score;
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += OnPlay;
        GameManager.OnGameLose += OnLose;
        GameManager.OnGameWin += OnWin;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnPlay;
        GameManager.OnGameLose -= OnLose;
        GameManager.OnGameWin -= OnWin;
    }



}
