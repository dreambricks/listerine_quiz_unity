using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttributeSelection : MonoBehaviour
{
    [SerializeField] private GameObject refrescancia;
    [SerializeField] private GameObject resposta;
    [SerializeField] private GameObject cta;

    public List<Toggle> attributeToggles;
    public Button nextButton;
    public Text warningText;

    private List<string> selectedAttributes = new List<string>();

    private List<string> anticariesAttributes = new List<string> { "Proteção anticáries", "Fortalecer os dentes" };
    private List<string> whiteningAttributes = new List<string> { "Clarear os dentes", "Remover manchas" };

    public float timeLeft;
    public float totalTime;

    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void Update()
    {
        Chronometer();
    }


    private void OnEnable()
    {
        timeLeft = totalTime;

        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }

        foreach (var toggle in attributeToggles)
        {
            toggle.isOn = false;
        }
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

        if (selectedAttributes.Count == 0)
        {
            if (warningText != null)
            {
                warningText.gameObject.SetActive(true);
                warningText.text = "Por favor, selecione pelo menos um atributo.";
                StartCoroutine(HideWarningTextAfterDelay(3f));
            }
            return;
        }


        PlayerPrefs.SetString("SelectedAttributes", string.Join(",", selectedAttributes));

        
        if (ShouldSkipRefreshScreen(selectedAttributes))
        {
            resposta.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            refrescancia.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    bool ShouldSkipRefreshScreen(List<string> attributes)
    {
        bool isAnticariesOnly = attributes.Count > 0 && attributes.TrueForAll(attr => anticariesAttributes.Contains(attr));
        bool isWhiteningOnly = attributes.Count > 0 && attributes.TrueForAll(attr => whiteningAttributes.Contains(attr));
        return isAnticariesOnly || isWhiteningOnly;
    }

    private IEnumerator HideWarningTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }
    }

    private void Chronometer()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            SaveLog();
            cta.gameObject.SetActive(true);
            gameObject.SetActive(false);

        }
    }

    void SaveLog()
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.PAROU_EM_ATRIBUTOS.ToString();
        dataLog.additional = "vazio";
        LogUtil.SaveLog(dataLog);
    }
}
