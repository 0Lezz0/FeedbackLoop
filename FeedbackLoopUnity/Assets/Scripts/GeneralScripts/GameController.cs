using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string PLAYER_TAG = "Player";
    public static string ENVIORMENT_TAG = "Enviorment";

    public static int MAX_BULLETS_ON_SCREEN = 1000;

    [SerializeField]
    private static MechaStatus player;

    [SerializeField]
    private GameObject _spawnPoint, _bulletPrefab;

    public GameObject SpawnPoint { get => _spawnPoint; set => _spawnPoint = value; }
    public GameObject BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }



    // Start is called before the first frame update
    void Start()
    {
        GameObject potentialPlayer = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        if(potentialPlayer.TryGetComponent(out player))
        {
            Debug.Log("Valid Player found");
            potentialPlayer.transform.SetPositionAndRotation(SpawnPoint.transform.position, SpawnPoint.transform.localRotation);
            potentialPlayer.GetComponent<MechaCollisions>().enabled = true;
            potentialPlayer.GetComponent<MechaMovement>().enabled = true;
        }

        BulletPool bulletPool = BulletPool.GetInstance();
        bulletPool.InitializePool(BulletPrefab, MAX_BULLETS_ON_SCREEN, gameObject);
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
