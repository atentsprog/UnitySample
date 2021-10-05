using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sample18_Anlytics
{ 
/// <summary>
/// https://docs.unity3d.com/Manual/UnityAnalyticsCustomEventScripting.html?_ga=2.162591996.1199620054.1633403722-1416507091.1623043000
/// </summary>
public class CustomEventSend : MonoBehaviour
{
    Button baseButton;
    void Start()
    {
        baseButton = GetComponentInChildren<Button>();

        AddButton("ReportSecretFound(1)", () => ReportSecretFound(1));
        AddButton("ReportSecretFound(2)", () => ReportSecretFound(2));
        AddButton("에러 만들기", () =>
        {
            int i = 0;
            print(1 / i);
        });
        AddButton("워닝 남기기", () => Debug.LogWarning("테스트 경고"));

        baseButton.gameObject.SetActive(false);
    }

    private void AddButton(string title, UnityAction fn)
    {
        var newButton = Instantiate(baseButton, baseButton.transform.parent);
        newButton.GetComponentInChildren<Text>().text = title;
        newButton.AddListener(this, fn);
    }

    public void ReportSecretFound(int secretID)
    {
        AnalyticsResult result = Analytics.CustomEvent("secret_found", new Dictionary<string, object>
        {
            { "secret_id", secretID },
            { "time_elapsed", Time.timeSinceLevelLoad },
            { "time_now", DateTime.Now}
        });

        print(result);
    }
}
}