using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Spawner _startPoint;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _spawnRadius;

    private bool _isWork = true;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.ReturnToPool += ReturnToPool;
        cube.gameObject.transform.position = Random.insideUnitSphere * _spawnRadius + _startPoint.transform.position;

        cube.gameObject.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(GetSphere());
    }

    private IEnumerator GetSphere()
    {
        while (_isWork)
        {
            yield return new WaitForSeconds(_repeatRate);
            _pool.Get();
        }
    }

    private void ReturnToPool(Cube cube)
    {
        cube.ReturnToPool -= ReturnToPool;
        _pool.Release(cube);
    }
}