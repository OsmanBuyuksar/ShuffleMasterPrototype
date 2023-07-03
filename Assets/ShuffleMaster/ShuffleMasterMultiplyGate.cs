using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleMasterMultiplyGate : MonoBehaviour, I_ShuffleMasterGate
{
    [SerializeField] private int scaleValue = 1;
    [SerializeField] private Text scaleText;
    // Start is called before the first frame update
    void Start()
    {
        scaleText.text = "X" + scaleValue;
    }
    public int ScaleValue(int count)
    {
        return (scaleValue - 1) * count;
    }

}
