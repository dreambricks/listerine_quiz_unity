using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour
{

    public List<string> resultList;
    public string results;
    public List<Sprite> spriteList;

    [SerializeField] private DictionaryImage dictionaryImage;
    [SerializeField] private ArduinoCommunication arduinoCommunication;
    [SerializeField] private ResultScreen resultScreen;

    private void OnEnable()
    {
        spriteList = new();

        results = PlayerPrefs.GetString("Results");

        resultList = new List<string>(results.Split(','));

        foreach (string s in resultList)
        {

      
                spriteList.Add(dictionaryImage.GetSpriteByName(resultScreen.imageDictionary[s]));
     
            
                Debug.Log($"Key '{s}' in imageDictionary.");
            
        }

        JoinImages();
        SendArduinomessage();

    }

    public void JoinImages()
    {

        if (spriteList == null || spriteList.Count == 0)
        {
            Debug.LogError("A lista de sprites está vazia ou não foi atribuída.");
            return;
        }

        Image[] allChildrenImages = GetComponentsInChildren<Image>(true);

  
        List<Image> childImages = new List<Image>();

        foreach (var img in allChildrenImages)
        {
            if (img.transform != transform) 
            {
                childImages.Add(img);
            }
        }

 
        for (int i = 0; i < childImages.Count; i++)
        {
    
            if (i < spriteList.Count)
            {
                childImages[i].sprite = spriteList[i];
            }
            else
            {
        
                Debug.LogWarning("Há mais filhos com Image do que sprites na lista.");
                break;
            }
        }
    }

    void SendArduinomessage()
    {
        if (resultScreen.colorDictionary.ContainsKey(resultList[0]))
        {
            arduinoCommunication.SendMessageToArduino(resultScreen.colorDictionary[resultList[0]]);
        }
        else
        {
            Debug.LogError($"Key '{resultList[0]}' not found in colorDictionary.");
        }
    }

    private void OnDisable()
    {
        arduinoCommunication.SendMessageToArduino("0,0,0");
    }


}
