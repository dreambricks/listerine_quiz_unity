using UnityEngine;
using UnityEngine.UI;

public class RefreshmentSelection : MonoBehaviour
{
    public Toggle intenseToggle;
    public Toggle mildToggle;
    public Button nextButton;

    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void OnNextButtonClicked()
    {
        string refreshmentType = intenseToggle.isOn ? "Refresc�ncia intensa" : "Refresc�ncia suave";
        PlayerPrefs.SetString("SelectedRefreshment", refreshmentType);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tela4");
    }
}
