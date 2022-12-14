using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private static BulletPool instance;
    private BasicPooling bulletPool;

    void OnEnable()
    {
        //EventManager.OnEnemyDeath += StartReturnEnemyToPool;
    }

    void OnDisable()
    {
        //EventManager.OnEnemyDeath -= StartReturnEnemyToPool;
    }

    private BulletPool()
    {
        //EventManager.OnEnemyDeath += StartReturnEnemyToPool;
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
    }


    public GameObject GetBullet()
    {
        return bulletPool.GetObject();
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bulletPool.AddElemenToPool(bullet);
    }

    public void StartReturnBulletToPool(GameObject bullet)
    {
        if (bullet != null)
        {
            ReturnBulletToPool(bullet);
        }
    }
}


