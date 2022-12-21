using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEventManager;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private  GameObject _interactPanel, _pauseMenu, _gameOverScreen;

    [SerializeField]
    private Image _playerHealthBar;

    public  GameObject InteractPanel { get => _interactPanel; set => _interactPanel = value; }
    public Image PlayerHealthBar { get => _playerHealthBar; set => _playerHealthBar = value; }
    public GameObject PauseMenu { get => _pauseMenu; set => _pauseMenu = value; }
    public GameObject GameOverScreen { get => _gameOverScreen; set => _gameOverScreen = value; }

    private readonly int maxPlayerHealth = 500; //This is a placeHolder until i have a better way to get the real max health
    private float maxBarSize;
    private MechaControls controlSystem;
    private MechaControls.UIActions UIActions;

    void OnEnable()
    {
        PlayerEventManager.OnPlayerDamage += UpdatePlayerLifeBar;
        PlayerEventManager.OnPlayerHeal += UpdatePlayerLifeBar;
        PlayerEventManager.OnPlayerDeath += ShowGameOverScreen;
    }

    void OnDisable()
    {
        PlayerEventManager.OnPlayerDamage -= UpdatePlayerLifeBar;
        PlayerEventManager.OnPlayerHeal -= UpdatePlayerLifeBar;
        PlayerEventManager.OnPlayerDeath -= ShowGameOverScreen;
    }

    // Start is called before the first frame update
    void Start()
    {
        controlSystem = new MechaControls();
        UIActions = controlSystem.UI;
        UIActions.Enable();
        maxBarSize = PlayerHealthBar.preferredWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIActions.PauseMenu.WasPerformedThisFrame())
        {
            TogglePauseMenu();
        }
    }

    public void ActivateInteractPanel()
    {
        InteractPanel.SetActive(true);
    }
    public void DeactivateInteractPanel()
    {
        InteractPanel.SetActive(false);
    }

    public void UpdatePlayerLifeBar(int valueApplied)
    {
        if(valueApplied> 0)
        {
            float ammountToRemove = (100 * valueApplied) / maxPlayerHealth;
            //PlayerHealthBar.fillAmount = valueApplied / ;
        }
    }

    public void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
}
