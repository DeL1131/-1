using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private float _maxLifeTime = 6;
    private bool _isColideWall;

    private Renderer _renderer;

    public event Action<Cube> ReturningToPool;

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
            float lifeTime = UnityEngine.Random.Range(0, _maxLifeTime);
            StartCoroutine(DestroyDelayed(lifeTime));
        }
    }

    private void ChangeColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private IEnumerator DestroyDelayed(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        _renderer.material.color = Color.white;
        ReturningToPool?.Invoke(this);
    }
}