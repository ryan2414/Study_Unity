using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    int _count = 0;
    public void OnButtonClicked()
    {
        _count++;
        _text.text = "점수 : " + _count;
    }
}
