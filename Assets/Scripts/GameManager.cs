using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cta;
    [SerializeField] private GameObject atributos;
    [SerializeField] private GameObject refrescancia;
    [SerializeField] private GameObject resposta;
    [SerializeField] private GameObject legal;
    [SerializeField] private GameObject qr;

    void Start()
    {
        PlayerPrefs.DeleteAll();

        cta.SetActive(true);
        atributos.SetActive(false);
        refrescancia.SetActive(false);
        resposta.SetActive(false);
        legal.SetActive(false);
        qr.SetActive(false);
    }

}
