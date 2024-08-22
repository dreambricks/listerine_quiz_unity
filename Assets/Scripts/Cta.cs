using System.Collections;
using UnityEngine;

public class Cta : MonoBehaviour
{
    [SerializeField] private GameObject atributos;
    [SerializeField] private ArduinoCommunication arduinoCommunication;
    public string[] colors;
    private Coroutine sendColorsCoroutine;


    private void Start()
    {
        colors = new string[]{
            "255,255,255",
            "190,25,255",
            "255,70,150",
            "131,255,50",
            "102,200,200",
            "0,171,255",
            "229,60,90",
            "255,60,90",
            "140,255,120",
            "70,255,90"
        };
    }

    private void OnEnable()
    {
        sendColorsCoroutine = StartCoroutine(SendColorsToArduino());
    }

    private IEnumerator SendColorsToArduino()
    {
        int colorCount = colors.Length;
        for (int i = 0; i < colorCount; i++)
        {
            string startColor = colors[i];
            string endColor = colors[(i + 1) % colorCount];

        
            Color startColorObj = ParseColor(startColor);
            Color endColorObj = ParseColor(endColor);

   
            yield return StartCoroutine(TransitionColors(startColorObj, endColorObj, 20, 0.08f)); 
           
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(SendColorsToArduino());
    }

    private IEnumerator TransitionColors(Color startColor, Color endColor, int steps, float stepDuration)
    {
        for (int i = 0; i <= steps; i++)
        {

            float t = i / (float)steps;
            Color intermediateColor = Color.Lerp(startColor, endColor, t);

            arduinoCommunication.SendMessageToArduino(ColorToString(intermediateColor));

            yield return new WaitForSeconds(stepDuration);
        }
    }

    private Color ParseColor(string colorString)
    {
        string[] rgb = colorString.Split(',');
        float r = float.Parse(rgb[0]) / 255f;
        float g = float.Parse(rgb[1]) / 255f;
        float b = float.Parse(rgb[2]) / 255f;
        return new Color(r, g, b);
    }

    private string ColorToString(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);
        return $"{r},{g},{b}";
    }

    private void OnMouseDown()
    {
        atributos.SetActive(true);
        arduinoCommunication.SendMessageToArduino("0,0,0");
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (sendColorsCoroutine != null)
        {
            StopCoroutine(sendColorsCoroutine);
            sendColorsCoroutine = null;
        }
    }

}
