using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string PLAYER_TAG = "Player";
    public static string ENVIORMENT_TAG = "Enviorment";

    [SerializeField]
    private static MechaStatus player;




    // Start is called before the first frame update
    void Start()
    {
        GameObject potentialPlayer = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        if(potentialPlayer.TryGetComponent(out player))
        {
            Debug.Log("Valid Player found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Transform GetPlayerPosition()
    {
        return player? player.GetComponentInParent<Transform>(): null;
    }
}
