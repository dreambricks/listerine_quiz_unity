using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cta : MonoBehaviour
{
    [SerializeField] private GameObject atributos;

    private void OnMouseDown()
    {
        atributos.SetActive(true);
        gameObject.SetActive(false);
    }
}
