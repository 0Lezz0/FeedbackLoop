using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string PLAYER_TAG = "Player";
    public static string ENVIORMENT_TAG = "Enviorment";

    [SerializeField]
    private static GameObject player;

    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Transform GetPlayerPosition()
    {
        return player? player.transform : null;
    }
}
