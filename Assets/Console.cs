using TMPro;
using UnityEngine;

public class Console : MonoBehaviour {
    public TextMeshProUGUI output;
    public void SendLine(string main)
    {
        output.text += main + "\n";
    }
}