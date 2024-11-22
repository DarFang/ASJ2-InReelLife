using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishToSpriteMap : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    public static FishToSpriteMap Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    public Sprite ReturnSprite(String FishValue)
    {
        return sprite;
    }
}
