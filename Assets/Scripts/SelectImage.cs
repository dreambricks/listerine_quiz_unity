using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour
{

    public string results;
    public List<string> resultList;
    public List<Sprite> spriteList;

    [SerializeField] private DictionaryImage dictionaryImage;
    [SerializeField] private ArduinoCommunication arduinoCommunication;
    [SerializeField] private ResultScreen resultScreen;

    private void OnEnable()
    {
        spriteList = new();

        results = PlayerPrefs.GetString("Results");

        resultList = new List<string>(results.Split(','));

        
        if (resultList.Contains("whitening"))
        {
            
            resultList.Remove("whitening");
            resultList.Insert(0, "whitening");
        }

        foreach (string s in resultList)
        {
            spriteList.Add(dictionaryImage.GetSpriteByName(resultScreen.imageDictionary[s]));

            Debug.Log($"Key '{s}' in imageDictionary.");
        }

        MoveWhiteningSpriteToFront();

        JoinImages();
        SendArduinomessage();
    }

    private void MoveWhiteningSpriteToFront()
    {
        Sprite whiteningSprite = spriteList.FirstOrDefault(s => s.name == "whitening");

        if (whiteningSprite != null)
        {
            spriteList.Remove(whiteningSprite);

            spriteList.Insert(0, whiteningSprite);
        }
    }


    public void JoinImages()
    {

        if (spriteList == null || spriteList.Count == 0)
        {
            Debug.Log("A lista de sprites está vazia ou não foi atribuída.");
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
        
               
                break;
            }
        }
    }

    void SendArduinomessage()
    {
        if (resultScreen.colorDictionary.ContainsKey(resultList[0]))
        {
            arduinoCommunication.SendMessageToArduino(resultScreen.colorDictionary[resultList[0]]);
            Debug.Log($"Key '{resultList[0]}' not found in colorDictionary.");

        }
        else
        {
            Debug.Log($"Key '{resultList[0]}' not found in colorDictionary.");
        }
    }

    private void OnDisable()
    {
        arduinoCommunication.SendMessageToArduino("0,0,0");
    }


}
