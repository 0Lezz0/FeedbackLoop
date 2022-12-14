using UnityEngine;

public class BulletPool
{
    private static BulletPool instance;
    private BasicPooling bulletPool;
    private GameObject originalBulletPrefab, originalSpawn;
  
    private BulletPool()
    {
    }

    public static BulletPool GetInstance()
    {
        if (instance == null)
        {
            instance = new BulletPool();
        }

        return instance;
    }

    public void InitializePool(GameObject bulletPrefab, int amount, GameObject spawn)
    {

        bulletPool = new BasicPooling(bulletPrefab, spawn, amount);
        originalBulletPrefab = bulletPrefab;
        originalSpawn = spawn;
    }

    public GameObject GetBullet()
    {
        return bulletPool.GetObject();
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.transform.position = originalSpawn.transform.position;
        bullet.transform.localScale = originalBulletPrefab.transform.localScale;
        bullet.SetActive(false);
        bulletPool.AddElemenToPool(bullet);
    }
}


