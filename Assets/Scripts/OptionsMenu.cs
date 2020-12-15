using System;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;

    public Action OnOpenMenu;
    public Action OnCloseMenu;
    public Action OnRestartClick;

    void Start()
    {        
        menuPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        OnOpenMenu?.Invoke();
        menuPanel.SetActive(true);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void CloseMenuButton()
    {
        menuPanel.SetActive(false);
        OnCloseMenu?.Invoke();
    }

    public void RestarGameButton()
    {
        OnRestartClick?.Invoke();
        menuPanel.SetActive(false);
    }
}
