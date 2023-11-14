using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : UI_Base
{

    enum Buttons
    {
        PointButton,
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    // 리플렉션
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        Get<TextMeshProUGUI>((int)Texts.ScoreText).text = "Bind Test!";
    }

    int _count = 0;
    
    public void OnButtonClicked()
    {
        _count++;
    }
}
