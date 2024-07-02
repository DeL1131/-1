using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Touch += ConsoleMessage;

    }

    private void OnDisable()
    {
        _player.Touch -= ConsoleMessage;
              

    }
    private void ConsoleMessage(string str)
    {
        Debug.Log(str);
    }


}
