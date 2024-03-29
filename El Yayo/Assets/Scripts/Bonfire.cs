using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour, ICollectable
{
    [SerializeField] GameObject bonfireMenu;

    public void OnTriggerEnter(Collider col)
    {
        Time.timeScale = 0;
        bonfireMenu.SetActive(true);
    }
}
