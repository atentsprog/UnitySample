using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeTest : MonoBehaviour
{
    InputField inputField;
    Image coolTimeIconImage;
    Text remainTimeText1;
    Text remainTimeText2;
    Text remainTimeText3;

    void Start()
    {
        inputField = transform.Find("InputField").GetComponent<InputField>();
        if (string.IsNullOrEmpty(inputField.text))
            inputField.text = "3";

        coolTimeIconImage = transform.Find("SkillIcon/Image").GetComponent<Image>();
        remainTimeText1 = transform.Find("SkillIcon/Text1").GetComponent<Text>();
        remainTimeText2 = transform.Find("SkillIcon/Text2").GetComponent<Text>();
        remainTimeText3 = transform.Find("SkillIcon/Text3").GetComponent<Text>();

        transform.Find("Method1Button").GetComponent<Button>().AddListener(this, OnClickSkillButton);
    }

    Coroutine handle;
    private void OnClickSkillButton()
    {
        if (handle != null)
            StopCoroutine(handle);
        handle = StartCoroutine(StartCoolTimeCo());
    }


    private IEnumerator StartCoolTimeCo()
    {
        float coolTimeSeconds = float.Parse(inputField.text);
        float endTime;//쿨타임 종룟히간
            endTime = Time.time + coolTimeSeconds;

        while (endTime > Time.time)
        {
            float remainTime = endTime - Time.time;

            remainTimeText1.text = ((int)(remainTime + 1)).ToString();
            remainTimeText2.text = (remainTime + 1).ToString("0.0");
            remainTimeText3.text = remainTime.ToString("0.0");

            float remainPercent = remainTime / coolTimeSeconds;   // 1.0 -> 0.0
            coolTimeIconImage.fillAmount = 1 - remainPercent;       // 0.0 -> 1.0

            yield return null;
        }
    }
}
