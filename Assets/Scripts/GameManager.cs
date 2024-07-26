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

    void Start()
    {
        cta.SetActive(true);
        atributos.SetActive(false);
        refrescancia.SetActive(false);
        resposta.SetActive(false);
        legal.SetActive(false);
    }

}
