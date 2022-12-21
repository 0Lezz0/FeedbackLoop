using UnityEngine;
using UnityEngine.SceneManagement;
using Config;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private static int _currentLoop;

    [SerializeField]
    private static MechaStatus player;

    [SerializeField]
    private GameObject _spawnPoint, _bulletPrefab, _particleHitPrefab;

    public GameObject SpawnPoint { get => _spawnPoint; set => _spawnPoint = value; }
    public GameObject BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }
    public GameObject ParticleHitPrefab { get => _particleHitPrefab; set => _particleHitPrefab = value; }
    public static int CurrentLoop { get => _currentLoop; }

    void OnEnable()
    {
        PlayerEventManager.OnPlayerDeath += OnPlayerDeath;
    }

    void OnDisable()
    {
        PlayerEventManager.OnPlayerDeath -= OnPlayerDeath;
    }

    void Start()
    {
        GameObject potentialPlayer = GameObject.FindGameObjectWithTag(ConstantsAndFixedValues.PLAYER_TAG);
        if(potentialPlayer.TryGetComponent(out player))
        {
            potentialPlayer.transform.SetPositionAndRotation(SpawnPoint.transform.position, SpawnPoint.transform.localRotation);
            potentialPlayer.GetComponent<MechaCollisions>().enabled = true;
            potentialPlayer.GetComponent<MechaMovement>().enabled = true;
            potentialPlayer.GetComponent<MechaWeaponController>().enabled = true;
            potentialPlayer.GetComponent<HealthSystem>().enabled = true;
        }

        BulletPool bulletPool = BulletPool.GetInstance();
        bulletPool.InitializePool(BulletPrefab, ConstantsAndFixedValues.DEFAUTL_MAX_BULLETS_ON_SCREEN, gameObject);
        //ParticleEffectPool particlePool = ParticleEffectPool.GetInstance();
        //particlePool.InitializePool(ParticleHitPrefab, MAX_PARTICLE_HIT_EFFECT_ON_SCREEN, gameObject);

        //Loads the current loop from file; if No file is present, tries to create a new one.
        if(CurrentLoop == 0)
        {
            LoopConfig loopStoredConfig = FileController.LoadData();
            if(loopStoredConfig == null)
            {
                FileController.SaveCurrentLoop(new LoopConfig(CurrentLoop));
            }
            else
            {
                _currentLoop = (int)loopStoredConfig?.currentLoop;
            }
        }
    }

    public static Transform GetPlayerPosition()
    {
        return player? player.GetComponentInParent<Transform>(): null;
    }

    public static void OnPlayerDeath()
    {
        Debug.Log("On no, you died :(! That's great, because you are awful >:|");
        //Reset the active level, while the 'game over' splash UI is active... find a way to keep the UI up?
    }

    public static void NextLoop()
    {
        //increases the current loop
        _currentLoop++;
        //saves the new loop on a file
        FileController.SaveCurrentLoop(new LoopConfig(CurrentLoop));

        //re-launches the game with the updated loop
        SceneManager.LoadScene((int)Scenes.DessertCanyon);
    }

    public static void ResetLoop()
    {
        FileController.SaveCurrentLoop(new LoopConfig(0));
    }
}
