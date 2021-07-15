using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeTest : MonoBehaviour
{
    InputField inputField;
    Image dashCoolTimeImg;
    Text DashRemindTimeTxt;

    void Start()
    {
        inputField = transform.Find("InputField").GetComponent<InputField>();
        if (string.IsNullOrEmpty(inputField.text))
            inputField.text = "3";

        dashCoolTimeImg = transform.Find("SkillIcon/Image").GetComponent<Image>();
        DashRemindTimeTxt = transform.Find("SkillIcon/Text").GetComponent<Text>();

        transform.Find("Method1Button").GetComponent<Button>().AddListener(this, OnClickSkillButton1);
        transform.Find("Method2Button").GetComponent<Button>().AddListener(this, OnClickSkillButton2);
    }

    enum MethodType
    {
        방법1,
        방법2
    }
    private void OnClickSkillButton2()
    {
        StartCoroutine(DashCoolTimeTxt(MethodType.방법2));
    }

    private void OnClickSkillButton1()
    {
        StartCoroutine(DashCoolTimeTxt(MethodType.방법1));
    }
    private IEnumerator DashCoolTimeTxt(MethodType methodType)
    {
        float dashCooltxt = float.Parse(inputField.text);
        float endTime = Time.time + dashCooltxt;
        while (endTime > Time.time)
        {
            DashRemindTimeTxt.text = ((int)(endTime - Time.time) + 1).ToString();

            if(methodType == MethodType.방법1)
                dashCoolTimeImg.fillAmount = 1 - (endTime - Time.time) / dashCooltxt;


            if (methodType == MethodType.방법2)
                dashCoolTimeImg.fillAmount = Mathf.Lerp(1, 0, (endTime - Time.time) / dashCooltxt);

            yield return null;
        }

    }
}
