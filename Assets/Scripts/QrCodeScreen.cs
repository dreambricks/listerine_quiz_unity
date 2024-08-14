using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QrCodeScreen : MonoBehaviour
{
    [SerializeField] private GameObject cta;

    public float time;

    public Button nextBtn;

    private void Start()
    {
        nextBtn.onClick.AddListener(() => GoToCTA());
    }

    private void OnEnable()
    {
        Invoke(nameof(GoToCTA), time);
    }

    void GoToCTA()
    {
        cta.SetActive(true);
        gameObject.SetActive(false);
    }
}
