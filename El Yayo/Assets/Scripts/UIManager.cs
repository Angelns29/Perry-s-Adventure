using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject startMenu;
    public GameObject hudMenu;
    public GameObject bonfireMenu;
    public List<Image> sprites = new List<Image>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        startMenu.SetActive(false);
        hudMenu.SetActive(true);
        Time.timeScale = 1f;    
    }
    public void ReturnToGame()
    {
        bonfireMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void SetObjectToInventory(string newObjectName)
    {
        foreach (var image in sprites)
        {
            if (image.sprite.name == "mask")
            {
                Debug.Log(newObjectName);
                image.sprite = Resources.Load<Sprite>("Images/" + newObjectName);
                return;
            } 
        }
    }
}
