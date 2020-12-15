using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    [SerializeField] private CroupierInspector inspector;
    [SerializeField] private CroupierDealer dealer;
    [SerializeField] private Timer timer;
    [SerializeField] private LevelSummaryMenu gameSummaryMenu;
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private TextDisplay scoreDisplay;
    [SerializeField] private TextDisplay timerDisplay;
    [SerializeField] private TextDisplay moveCounterDisplay;

    private void Start()
    {       
        inspector.OnGameOver += HandleGameOver;
        inspector.OnMoveCounterChange += moveCounterDisplay.UpdateDisplay;
        timer.OnTimeUpdate += timerDisplay.UpdateDisplay;            
        gameSummaryMenu.OnNewGame += inspector.SetUpNewGame;
        optionsMenu.OnRestartClick += inspector.SetUpNewGame;
    }

    private void OnDisable()
    {
        inspector.OnGameOver -= HandleGameOver;
        inspector.OnMoveCounterChange -= moveCounterDisplay.UpdateDisplay;
        timer.OnTimeUpdate -= timerDisplay.UpdateDisplay;
        gameSummaryMenu.OnNewGame -= inspector.SetUpNewGame;
        optionsMenu.OnRestartClick -= inspector.SetUpNewGame;
    }

    private void HandleGameOver(bool _isWin)
    {

        gameSummaryMenu.LevelSummary(_isWin);
    }
}
