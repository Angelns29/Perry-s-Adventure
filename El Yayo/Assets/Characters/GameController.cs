using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool sword;
    public bool estus;
    public bool franchesco;
    public PlayerController playerController;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    public Vector3 GetPlayerPosition()
    {
        Debug.Log(playerController.transform.position);
        return playerController.transform.position;
    }
    public void SetObject(string name)
    {
        switch (name.ToUpper())
        {
            case "SWORD":
                sword = true; break;
            case "ESTUS":
                estus = true; break;
            case "FRANCHESCO":
                franchesco = true; break;
            default: break;
        }
    }
}
