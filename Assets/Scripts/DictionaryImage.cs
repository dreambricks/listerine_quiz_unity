using System.Collections.Generic;
using UnityEngine;


public class DictionaryImage : MonoBehaviour
{
    private Dictionary<string, Sprite> spriteDictionary;

    public List<Sprite> sprites;

    void Awake()
    {

        spriteDictionary = new Dictionary<string, Sprite>();


        foreach (Sprite sprite in sprites)
        {
            spriteDictionary.Add(sprite.name, sprite);
        }
    }


    public Sprite GetSpriteByName(string name)
    {

        if (spriteDictionary.TryGetValue(name, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            return null;
        }
    }
}
