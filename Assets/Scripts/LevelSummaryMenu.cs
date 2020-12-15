using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSummaryMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] private Text messageText; 
    [SerializeField] private Text timeText;
    [SerializeField] private Text movesText;
    [SerializeField] private Text scoreText;
    private string winMessage = "Congratulations! You Win";
    private string concideMessage = "Don't Give Up!";

    public Action OnNewGame;

    void Start()
    {
        menuPanel.SetActive(false);
    }        

    public void LevelSummary(bool _win)
    {
        menuPanel.SetActive(true);
        if (_win) messageText.text = winMessage;
        else messageText.text = concideMessage;
        //timeText.text = _time;
        //movesText.text = _moves;
        //scoreText.text = _score;
    }

    private void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

    public void NewLevelClick()
    {
        OnNewGame?.Invoke();
        CloseMenu();
    }
}
