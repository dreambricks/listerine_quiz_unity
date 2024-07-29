using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RefreshmentSelection : MonoBehaviour
{
    [SerializeField] private GameObject resposta;
    [SerializeField] private GameObject cta;


    public ToggleGroup refreshmentToggleGroup;
    public Toggle intenseToggle;
    public Toggle mildToggle;
    public Button nextButton;

    public float timeLeft;
    public float totalTime;

    void Start()
    {
        mildToggle.isOn = false;
        intenseToggle.isOn = false;
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void OnEnable()
    {
        timeLeft = totalTime;
    }

    private void Update()
    {
        Chronometer();
    }

    void OnNextButtonClicked()
    {
        Toggle selectedToggle = refreshmentToggleGroup.ActiveToggles().FirstOrDefault();

        if (selectedToggle != null)
        {
            string selectedOption = selectedToggle.GetComponentInChildren<Text>().text;

            
            PlayerPrefs.SetString("SelectedRefreshment", selectedOption);

            resposta.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Nenhuma opção de refrescância selecionada.");
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
        dataLog.status = StatusEnum.PAROU_EM_REFRESCANCIA.ToString();
        dataLog.additional = "vazio";
        LogUtil.SaveLog(dataLog);
    }
}
