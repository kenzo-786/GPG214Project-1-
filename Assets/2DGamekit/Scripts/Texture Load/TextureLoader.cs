using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextureLoader : MonoBehaviour
{
    public string fileName = "Texture/sky.jpeg";
    public string spriteName = "Sprite/Dropship.png";
    public string folderPath = Application.streamingAssetsPath;
    string combinedFilePathLocation;
    string combineDropShipPath;

    public SpriteRenderer dropShip;

    public GameObject dropShipGameObject;

    private void Start()
    {
        combinedFilePathLocation = Path.Combine(folderPath, fileName);
        combineDropShipPath = Path.Combine(folderPath, spriteName);
        LoadTexture();
        LoadSprite();
    }

    void LoadTexture()
    {
        if (File.Exists(combinedFilePathLocation))
        {
            byte[] imageBytes = File.ReadAllBytes(combinedFilePathLocation);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            GetComponent<Renderer>().material.mainTexture = texture;
        }
    }
    
    void LoadSprite()
    {
        if (File.Exists(combineDropShipPath))
        {
            byte[] spriteBytes = File.ReadAllBytes(combineDropShipPath);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(spriteBytes);

            dropShip.sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
