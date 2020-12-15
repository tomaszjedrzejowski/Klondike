using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    private Text displayText;

    void Start()
    {
        displayText = GetComponent<Text>();
        displayText.text = "00";
    }

    public void UpdateDisplay(string _text)
    {
        displayText.text = _text;
    }
}
