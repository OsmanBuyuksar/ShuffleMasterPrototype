using UnityEngine.UI;
using UnityEngine;

public class MinusGate : MonoBehaviour, IGate
{
    [SerializeField] private int scaleValue;
    [SerializeField] private Text scaleText;
    public void Start()
    {
        scaleText.text = "" + scaleValue;
    }
    public int ScaleValue()
    {
        return scaleValue;
    }
}
