using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeSelection : MonoBehaviour
{
    public List<Toggle> attributeToggles;
    public Button nextButton;

    private List<string> selectedAttributes = new List<string>();

    void Start()
    {
        Debug.Log("Hello, Unity Console!");
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void OnNextButtonClicked()
    {
        selectedAttributes.Clear();
        foreach (var toggle in attributeToggles)
        {
            if (toggle.isOn)
            {
                selectedAttributes.Add(toggle.GetComponentInChildren<Text>().text);
            }
        }
  
        // Armazena os atributos selecionados para uso na próxima tela
        PlayerPrefs.SetString("SelectedAttributes", string.Join(",", selectedAttributes));
        Debug.Log(PlayerPrefs.GetString("SelectedAttributes"));

        // Verifique se deve pular a tela 3
        if (ShouldSkipRefreshScreen(selectedAttributes))
        {
            Debug.Log("Tela4");
        }
        else
        {
            Debug.Log("Tela3");
        }
    }

    bool ShouldSkipRefreshScreen(List<string> attributes)
    {
        return attributes.Contains("Proteção anticáries") || attributes.Contains("Clarear os dentes");
    }
}
