using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string PLAYER_TAG = "Player";
    public static string ENVIORMENT_TAG = "Enviorment";
    public static string ENEMY_BULLET = "EnemyBullet";
    public static string ENEMY = "Enemy";

    public static int MAX_BULLETS_ON_SCREEN = 1000;
    public static int MAX_PARTICLE_HIT_EFFECT_ON_SCREEN = 100;

    [SerializeField]
    private static MechaStatus player;

    [SerializeField]
    private GameObject _spawnPoint, _bulletPrefab, _particleHitPrefab;

    public GameObject SpawnPoint { get => _spawnPoint; set => _spawnPoint = value; }
    public GameObject BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }
    public GameObject ParticleHitPrefab { get => _particleHitPrefab; set => _particleHitPrefab = value; }


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
            potentialPlayer.GetComponent<MechaWeaponController>().enabled = true;
        }

        BulletPool bulletPool = BulletPool.GetInstance();
        bulletPool.InitializePool(BulletPrefab, MAX_BULLETS_ON_SCREEN, gameObject);
        //ParticleEffectPool particlePool = ParticleEffectPool.GetInstance();
        //particlePool.InitializePool(ParticleHitPrefab, MAX_PARTICLE_HIT_EFFECT_ON_SCREEN, gameObject);
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
