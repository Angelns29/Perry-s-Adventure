using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    
    public void SaveData()
    {
        Vector3 _position = GameController.instance.GetPlayerPosition();
        PlayerData player = new PlayerData(_position, GameController.instance.sword, GameController.instance.estus,GameController.instance.franchesco);
        Game _game = new Game(player);

        try
        {
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/data.json");
            string json = JsonUtility.ToJson(_game);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public Game LoadData() 
    {
        Game _game = new Game();
        try
        {
            StreamReader sr = File.OpenText(Application.persistentDataPath + "/data,json");
            if (sr != null)
            {
                string content = sr.ReadToEnd();
                sr.Close();
                _game = JsonUtility.FromJson<Game>(content);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return _game;
    }

    // Classes del JSON
    [Serializable]
    public class Game
    {
        public PlayerData _player;
        public EnemyData[] _enemies;

        public Game() { }
        public Game(PlayerData data)
        {
            _player = data;
        }
    }
    [Serializable]
    public class PlayerData
    {
        public float posX;
        public float posY;
        public float posZ;
        public bool swordEquipped = false;
        public bool estusEquipped = false;
        public bool franchescoEquipped = false;

        public PlayerData(Vector3 pos, bool swordbool, bool estusbool,bool franchescobool)
        {
            this.posX = pos.x;
            this.posY = pos.y;
            this.posZ = pos.z;
            this.swordEquipped = swordbool;
            this.estusEquipped = estusbool;
            this.franchescoEquipped= franchescobool;
        }
    }
    [Serializable]
    public class EnemyData
    {
        public int posX;
        public int posY;
        public int posZ;
        public int hp;

    }
}
