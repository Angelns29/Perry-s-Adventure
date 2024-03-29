using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] GameObject objectInCharacter;
    [SerializeField] GameObject collectibleObject;


    public void OnTriggerEnter(Collider col)
    {
        objectInCharacter.SetActive(true);
        collectibleObject.SetActive(false);
        GameController.instance.SetObject(collectibleObject.name);
        SoundManagerScript.instance.PlaySFX(SoundManagerScript.instance.item);
        UIManager.instance.SetObjectToInventory(collectibleObject.name);
    }
}
