using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private GameObject cta;

    public Text resultText;
    public Button returnCta;

    void Start()
    {
        returnCta.onClick.AddListener(OnNextButtonClicked);
    }

    private void OnEnable()
    {

        string selectedAttributes = PlayerPrefs.GetString("SelectedAttributes");
        string selectedRefreshment = PlayerPrefs.GetString("SelectedRefreshment");

        string result1 = DetermineResult1(selectedAttributes);
        string result2 = DetermineResult2(result1, selectedRefreshment);

        resultText.text = $"Resultado 1: {result1}\nResultado 2: {result2}";
    }

    string DetermineResult1(string attributes)
    {
        HashSet<string> attributeSet = new HashSet<string>(attributes.Split(','));

        bool hasGeralSW = attributeSet.Contains("Combater o mau h�lito") ||
                          attributeSet.Contains("Limpar toda a �rea da boca") ||
                          attributeSet.Contains("At� 24 horas de prote��o") ||
                          attributeSet.Contains("Eliminar at� 99.9% dos germes") ||
                          attributeSet.Contains("Prevenir a placa bacteriana") ||
                          attributeSet.Contains("Gengivas saud�veis");

        bool hasAnticaries = attributeSet.Contains("Prote��o antic�ries") ||
                             attributeSet.Contains("Fortalecer os dentes");

        bool hasAntitartaro = attributeSet.Contains("Prevenir o t�rtaro");

        bool hasWhitening = attributeSet.Contains("Clarear os dentes") ||
                            attributeSet.Contains("Remover manchas");

        if (hasGeralSW && hasAnticaries && hasAntitartaro && hasWhitening)
            return "Cuidado Total + Whitening";
        if (hasGeralSW && hasAnticaries && hasWhitening)
            return "Antic�ries + Whitening";
        if (hasGeralSW && hasAnticaries && hasAntitartaro)
            return "Cuidado Total";
        if (hasGeralSW && hasAnticaries)
            return "Antic�ries";
        if (hasGeralSW && hasAntitartaro && hasWhitening)
            return "Antit�rtaro + Whitening";
        if (hasGeralSW && hasAntitartaro)
            return "Antit�rtaro";
        if (hasGeralSW && hasWhitening)
            return "Cool Mint / Melancia + Whitening";
        if (hasGeralSW)
            return "Cool Mint / Melancia";

        if (hasAnticaries && hasAntitartaro && hasWhitening)
            return "Cuidado Total + Whitening";
        if (hasAnticaries && hasWhitening)
            return "Antic�ries + Whitening";
        if (hasAnticaries && hasAntitartaro)
            return "Cuidado Total";
        if (hasAnticaries)
            return "Antic�ries";
        if (hasAntitartaro && hasWhitening)
            return "Antit�rtaro + Whitening";
        if (hasAntitartaro)
            return "Antit�rtaro";
        if (hasWhitening)
            return "Whitening";

        return "Nenhum resultado encontrado";
    }

    string DetermineResult2(string result1, string refreshment)
    {
        if (result1 == "Cool Mint / Melancia")
        {
            if (refreshment == "Refresc�ncia intensa")
                return "Cool Mint";
            if (refreshment == "Refresc�ncia suave")
                return "Cool Mint s/ �lcool, Melancia & Hortel�";
        }
        if (result1 == "Antit�rtaro")
        {
            if (refreshment == "Refresc�ncia intensa")
                return "Antit�rtaro";
            if (refreshment == "Refresc�ncia suave")
                return "Antit�rtaro s/ �lcool";
        }
        if (result1 == "Cuidado Total")
        {
            if (refreshment == "Refresc�ncia intensa")
                return "Cuidado Total";
            if (refreshment == "Refresc�ncia suave")
                return "Cuidado Total s/ �lcool";
        }

        return result1;
    }

    void OnNextButtonClicked()
    {
        ResetAllPlayerPrefs();

        cta.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Todos os PlayerPrefs foram resetados.");
    }
}
