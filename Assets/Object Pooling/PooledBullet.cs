using UnityEngine;
using UnityEngine.Pool;

public class PooledBullet : MonoBehaviour
{
    private ObjectPool<PooledBullet> _poolReference;
    [SerializeField] private float lifetime = 2f;

    public void SetPool(ObjectPool<PooledBullet> pool)
    {
        _poolReference = pool;
    }

    private void OnEnable()
    {
        // Start a countdown to return to the pool when activated
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void ReturnToPool()
    {
        // Instead of Destroy(gameObject), release it back to the pool
        _poolReference.Release(this);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 10, 0), 5f * Time.deltaTime);
    }
}