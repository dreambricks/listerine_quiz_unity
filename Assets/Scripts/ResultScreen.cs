using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private GameObject qr;

    [SerializeField] private GameObject panel1item;
    [SerializeField] private GameObject panel2items;
    [SerializeField] private GameObject panel3items;

    public Dictionary<string, string> imageDictionary;

    public Dictionary<string, string> colorDictionary;

    public Button returnCta;

    public HashSet<string> results;

    public string resultTotal;

    public float timeLeft;
    public float totalTime;

    private ColorParameters colorParameter;


    void Awake()
    {
        returnCta.onClick.AddListener(OnNextButtonClicked);
        colorParameter = SaveManager.LoadFromJsonFile<ColorParameters>("ColorParameters.json");

    }

    private void OnEnable()
    {

        imageDictionary = new Dictionary<string, string>
        {
            { "Whitening", "whitening" },
            { "Cuidado Total", "cuidado_total_intense" },
            { "Cuidado Total s/ �lcool", "cuidado_total_suave" },
            { "Antic�ries", "anticaries_suave" },
            { "Antit�rtaro s/ �lcool", "antitartaro_suave" },
            { "Antit�rtaro", "antitartaro_intense" },
            { "Melancia & Hortel�", "melancia" },
            { "Melancia", "melancia" },
            { "Cool Mint s/ �lcool", "cool_mint_suave" },
            { "Cool Mint", "cool_mint_intense" }
        };


        colorDictionary = new Dictionary<string, string>
        {
            { "Whitening", colorParameter.Whitening },
            { "Cuidado Total", colorParameter.CuidadoTotal },
            { "Cuidado Total s/ �lcool", colorParameter.CuidadoTotalSemAlcool },
            { "Antic�ries", colorParameter.Anticaries },
            { "Antit�rtaro s/ �lcool", colorParameter.AntitartaroSemAlcool },
            { "Antit�rtaro", colorParameter.Antitartaro },
            { "Melancia & Hortel�", colorParameter.Melancia },
            { "Melancia", colorParameter.Melancia },
            { "Cool Mint s/ �lcool", colorParameter.CoolMintSemAlcool },
            { "Cool Mint", colorParameter.CoolMint }
        };


        timeLeft = totalTime;


        string selectedAttributes = PlayerPrefs.GetString("SelectedAttributes");
        string selectedRefreshment = PlayerPrefs.GetString("SelectedRefreshment");

        string result1 = DetermineResult1(selectedAttributes);
        string result2 = DetermineResult2(result1, selectedRefreshment);

        resultTotal =  JoinResults(result1, result2);
        AddResultsToHash(resultTotal);

        DisplayPanel();

        Debug.Log("RESULTADOS: " + PlayerPrefs.GetString("Results"));

    }

    private void Update()
    {
        Chronometer();
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
            return "Cuidado Total,Whitening";
        if (hasGeralSW && hasAnticaries && hasWhitening)
            return "Antic�ries,Whitening";
        if (hasGeralSW && hasAnticaries && hasAntitartaro)
            return "Cuidado Total";
        if (hasGeralSW && hasAnticaries)
            return "Antic�ries";
        if (hasGeralSW && hasAntitartaro && hasWhitening)
            return "Antit�rtaro,Whitening";
        if (hasGeralSW && hasAntitartaro)
            return "Antit�rtaro";
        if (hasGeralSW && hasWhitening)
            return "Cool Mint,Melancia,Whitening";
        if (hasGeralSW)
            return "Cool Mint,Melancia";

        if (hasAnticaries && hasAntitartaro && hasWhitening)
            return "Cuidado Total,Whitening";
        if (hasAnticaries && hasWhitening)
            return "Antic�ries,Whitening";
        if (hasAnticaries && hasAntitartaro)
            return "Cuidado Total";
        if (hasAnticaries)
            return "Antic�ries";
        if (hasAntitartaro && hasWhitening)
            return "Antit�rtaro,Whitening";
        if (hasAntitartaro)
            return "Antit�rtaro";
        if (hasWhitening)
            return "Whitening";

        return "Nenhum resultado encontrado";
    }

    string DetermineResult2(string result1, string refreshment)
    {
        if (result1 == "Cool Mint,Melancia")
        {
            if (refreshment == "Refresc�ncia intensa")
                return "Cool Mint";
            if (refreshment == "Refresc�ncia suave")
                return "Cool Mint s/ �lcool,Melancia";
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
        SaveLog();
        ResetAllPlayerPrefs();
        qr.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Todos os PlayerPrefs foram resetados.");
    }

    public string JoinResults(string result1,string result2)
    {
        return result1 + "," + result2;
    }

    public void AddResultsToHash(string resultTotal)
    {
        results = new HashSet<string>(resultTotal.Split(','));
    }



    private void Chronometer()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            SaveLog();
            ResetAllPlayerPrefs();
            qr.gameObject.SetActive(true);
            gameObject.SetActive(false);

        }
    }

    void SaveLog()
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.ACAO_CONCLUIDA.ToString();
        foreach (var result in results)
        {
           dataLog.additional += result + "|";
        }
        dataLog.additional = dataLog.additional.Remove(dataLog.additional.Length - 1);
        LogUtil.SaveLog(dataLog);
    }

    void DisplayPanel()
    {
        panel1item.gameObject.SetActive(false);
        panel2items.gameObject.SetActive(false);
        panel3items.gameObject.SetActive(false);

        PlayerPrefs.SetString("Results", string.Join(",", results));

        switch (results.Count)
        {
            case 1:
                panel1item.gameObject.SetActive(true);
                panel2items.gameObject.SetActive(false);
                panel3items.gameObject.SetActive(false);
                break;
            case 2:
                panel1item.gameObject.SetActive(false);
                panel2items.gameObject.SetActive(true);
                panel3items.gameObject.SetActive(false);
                break;
          
            default:
                panel1item.gameObject.SetActive(false);
                panel2items.gameObject.SetActive(false);
                panel3items.gameObject.SetActive(true);
                break;
        }

    }

    private void OnDisable()
    {
        panel1item.gameObject.SetActive(false);
        panel2items.gameObject.SetActive(false);
        panel3items.gameObject.SetActive(false);
    }

}
