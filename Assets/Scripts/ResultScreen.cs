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

        bool hasGeralSW = attributeSet.Contains("Combater o mau hálito") ||
                          attributeSet.Contains("Limpar toda a área da boca") ||
                          attributeSet.Contains("Até 24 horas de proteção") ||
                          attributeSet.Contains("Eliminar até 99.9% dos germes") ||
                          attributeSet.Contains("Prevenir a placa bacteriana") ||
                          attributeSet.Contains("Gengivas saudáveis");

        bool hasAnticaries = attributeSet.Contains("Proteção anticáries") ||
                             attributeSet.Contains("Fortalecer os dentes");

        bool hasAntitartaro = attributeSet.Contains("Prevenir o tártaro");

        bool hasWhitening = attributeSet.Contains("Clarear os dentes") ||
                            attributeSet.Contains("Remover manchas");

        if (hasGeralSW && hasAnticaries && hasAntitartaro && hasWhitening)
            return "Cuidado Total + Whitening";
        if (hasGeralSW && hasAnticaries && hasWhitening)
            return "Anticáries + Whitening";
        if (hasGeralSW && hasAnticaries && hasAntitartaro)
            return "Cuidado Total";
        if (hasGeralSW && hasAnticaries)
            return "Anticáries";
        if (hasGeralSW && hasAntitartaro && hasWhitening)
            return "Antitártaro + Whitening";
        if (hasGeralSW && hasAntitartaro)
            return "Antitártaro";
        if (hasGeralSW && hasWhitening)
            return "Cool Mint / Melancia + Whitening";
        if (hasGeralSW)
            return "Cool Mint / Melancia";

        if (hasAnticaries && hasAntitartaro && hasWhitening)
            return "Cuidado Total + Whitening";
        if (hasAnticaries && hasWhitening)
            return "Anticáries + Whitening";
        if (hasAnticaries && hasAntitartaro)
            return "Cuidado Total";
        if (hasAnticaries)
            return "Anticáries";
        if (hasAntitartaro && hasWhitening)
            return "Antitártaro + Whitening";
        if (hasAntitartaro)
            return "Antitártaro";
        if (hasWhitening)
            return "Whitening";

        return "Nenhum resultado encontrado";
    }

    string DetermineResult2(string result1, string refreshment)
    {
        if (result1 == "Cool Mint / Melancia")
        {
            if (refreshment == "Refrescância intensa")
                return "Cool Mint";
            if (refreshment == "Refrescância suave")
                return "Cool Mint s/ álcool, Melancia & Hortelã";
        }
        if (result1 == "Antitártaro")
        {
            if (refreshment == "Refrescância intensa")
                return "Antitártaro";
            if (refreshment == "Refrescância suave")
                return "Antitártaro s/ álcool";
        }
        if (result1 == "Cuidado Total")
        {
            if (refreshment == "Refrescância intensa")
                return "Cuidado Total";
            if (refreshment == "Refrescância suave")
                return "Cuidado Total s/ álcool";
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
