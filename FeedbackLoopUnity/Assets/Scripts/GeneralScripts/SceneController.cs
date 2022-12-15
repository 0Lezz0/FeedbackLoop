using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Scenes _sceneTransitionTo;
    public Scenes SceneTransitionTo { get => _sceneTransitionTo; set => _sceneTransitionTo = value; }

    private GameObject UIPanel;
    private MechaControls controlSystem;
    private MechaControls.MechaActionsActions mechaActions;

    private bool canChangeScene;

    void Start()
    {
        UIPanel = GameObject.FindGameObjectWithTag(GameController.MAIN_UI);
        controlSystem = new MechaControls();
        mechaActions = controlSystem.MechaActions;
        mechaActions.Enable();

        canChangeScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canChangeScene && mechaActions.Interact.triggered)
        {
            //Scene transition to SceneTransitionTo
            Debug.Log("Traveling to " + SceneTransitionTo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameController.PLAYER_TAG))
        {
            canChangeScene = true;
            UIPanel.GetComponent<UIController>().ActivateInteractPanel();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameController.PLAYER_TAG))
        {
            canChangeScene = false;
            UIPanel.GetComponent<UIController>().DeactivateInteractPanel();
        }
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene((int)SceneTransitionTo);
    }


}

public enum Scenes
{
    DessertCanyon,
    ReactorCore
}
