using UnityEngine;
using UnityEngine.Pool; // Required for the built-in system

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private PooledBullet bulletPrefab;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 50;
    private int spawnTimer = 0;

    private ObjectPool<PooledBullet> _pool;

    private void Awake()
    {
        // Initialize the pool with its configuration requirements
        _pool = new ObjectPool<PooledBullet>(
            createFunc: CreateBullet,          // How to create a new item if the pool is empty
            actionOnGet: OnGetBullet,          // What to do when an item is retrieved
            actionOnRelease: OnReleaseBullet,  // What to do when an item is returned
            actionOnDestroy: OnDestroyBullet,  // What to do if max size is exceeded
            collectionCheck: true,             // Throws an error if you return an item already in the pool
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    // 1. Create a new instance
    private PooledBullet CreateBullet()
    {
        PooledBullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(_pool); // Give the bullet a reference to return itself
        return bullet;
    }

    // 2. Activate the object when taken from the pool
    private void OnGetBullet(PooledBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    // 3. Deactivate the object when returned to the pool
    private void OnReleaseBullet(PooledBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // 4. Clean up memory if the pool is overstuffed
    private void OnDestroyBullet(PooledBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Update()
    {
        spawnTimer++;
        if (spawnTimer >= 50)
        {
            PooledBullet bullet = _pool.Get();
            bullet.transform.position = transform.position;
            spawnTimer = 0;
        }
    }
}