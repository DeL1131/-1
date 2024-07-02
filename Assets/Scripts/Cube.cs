using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private float _maxLifeTime = 6;
    private float _lifeTime;
    private bool _isColideWall;

    public event Action<Cube> ReturnToPool;
    private Spawner _spawner;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _isColideWall = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _) && _isColideWall == false)
        {
            _isColideWall = true;
            ChangeColor();
            _lifeTime = UnityEngine.Random.Range(0, _maxLifeTime);
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        _renderer.material.color = Color.white;
        ReturnToPool?.Invoke(this);
    }
     
    private void ChangeColor()
    {
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
}