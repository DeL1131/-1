using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private float _maxLifeTime = 6;
    private float _lifeTime;

    private Renderer _renderer;
    private HashSet<GameObject> _collidedWalls;

    private void Start()
    {       
        _collidedWalls = new HashSet<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _))
        {
            if (_collidedWalls.Contains(collision.gameObject))
            {
                return;
            }

            ChangeColor();
            _collidedWalls.Add(collision.gameObject);
            _lifeTime = Random.Range(0, _maxLifeTime);
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void ChangeColor()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
}