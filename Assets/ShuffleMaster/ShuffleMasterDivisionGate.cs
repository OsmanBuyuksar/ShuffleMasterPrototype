using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleMasterDivisionGate : MonoBehaviour, I_ShuffleMasterGate
{
    [SerializeField] private int scaleValue = 1;
    [SerializeField] private Text scaleText;

    private int cardCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        scaleText.text = "/" + scaleValue;
    }
    public int ScaleValue(int count)
    {
        return -count / (scaleValue);
    }

}
