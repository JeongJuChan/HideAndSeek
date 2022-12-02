using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RouletteSlot : MonoBehaviour
{
    [SerializeField] Image _image;


    void Start()
    {
        SetThumbNail();
    }

    void SetThumbNail()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Objects/BoxCollider/Box_Frisbee");
        Texture2D thumbNail = AssetPreview.GetAssetPreview(go);
        Rect rect = new Rect(0, 0, thumbNail.width, thumbNail.height);
        _image.sprite = Sprite.Create(thumbNail, rect, new Vector2(0.5f, 0.5f));
    }

    void Update()
    {
        
    }
}
