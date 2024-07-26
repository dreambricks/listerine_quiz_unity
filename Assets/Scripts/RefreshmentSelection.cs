using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RefreshmentSelection : MonoBehaviour
{
    [SerializeField] private GameObject resposta;

    public ToggleGroup refreshmentToggleGroup;
    public Toggle intenseToggle;
    public Toggle mildToggle;
    public Button nextButton;

    void Start()
    {
        mildToggle.isOn = false;
        intenseToggle.isOn = false;
        nextButton.onClick.AddListener(OnNextButtonClicked);
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
}
