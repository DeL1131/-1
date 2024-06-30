using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _spawnRadius;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.transform.position = Random.insideUnitSphere * _spawnRadius + _startPoint.transform.position;

        cube.gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetSphere), 0.0f, _repeatRate);
    }

    private void GetSphere()
    {
        _pool.Get();
    }

    private void ReturnToPool(Cube cube)
    {
        _pool.Release(cube);
    }
}