using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Character1,Character2,Character3,Character4
}
public class DataMgr : MonoBehaviour
{
    public static DataMgr instance;
    private void Awake()
    {
        if(instance == null) instance = this;
        else if(instance !=null)return;
        DontDestroyOnLoad(gameObject);
    }

    public Character currentCharacter;
}
