using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _spawnRadius;

    private bool _isWork = true;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActivateOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity);
    }

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_repeatRate);

        while (_isWork)
        {
            yield return waitForSeconds;
            _pool.Get();
        }
    }

    private void ActivateOnGet(Cube cube)
    {
        cube.ReturningToPool += ReturnToPool;
        cube.gameObject.transform.position = Random.insideUnitSphere * _spawnRadius + transform.position;

        cube.gameObject.SetActive(true);
    }

    private void ReturnToPool(Cube cube)
    {
        cube.ReturningToPool -= ReturnToPool;
        _pool.Release(cube);
    }
}