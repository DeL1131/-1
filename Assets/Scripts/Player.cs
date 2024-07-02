using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{

    public event Action<string> Touch;

    private void OnCollisionEnter(Collision collision)
    {
        Touch?.Invoke("Стук");
    }
}
