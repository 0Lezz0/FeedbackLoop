using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private  GameObject _interactPanel;

    [SerializeField]
    private Image _playerHealthBar;

    public  GameObject InteractPanel { get => _interactPanel; set => _interactPanel = value; }
    public Image PlayerHealthBar { get => _playerHealthBar; set => _playerHealthBar = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateInteractPanel()
    {
        InteractPanel.SetActive(true);
    }
    public void DeactivateInteractPanel()
    {
        InteractPanel.SetActive(false);
    }
}
