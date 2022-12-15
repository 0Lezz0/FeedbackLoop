using UnityEngine;

public class ParticleEffectPool
{
    private static ParticleEffectPool instance;
    private BasicPooling particlePool;
    private GameObject originalSpawn;

    private ParticleEffectPool()
    {
    }

    public static ParticleEffectPool GetInstance()
    {
        if (instance == null)
        {
            instance = new ParticleEffectPool();
        }

        return instance;
    }

    public void InitializePool(GameObject particlePrefab, int amount, GameObject spawn)
    {

        particlePool = new BasicPooling(particlePrefab, spawn, amount);
        originalSpawn = spawn;
    }

    public GameObject GetParticle()
    {
        return particlePool.GetObject();
    }

    public void ReturnParticleToPool(GameObject particle)
    {
        particle.transform.position = originalSpawn.transform.position;
        particle.SetActive(false);
        particlePool.AddElemenToPool(particle);
    }
}
