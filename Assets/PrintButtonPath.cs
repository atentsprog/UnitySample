using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Sample20_3D_UI
{
    public class PrintButtonPath : MonoBehaviour
    {

        void Start()
        {
            var buttons = GetComponentsInChildren<Button>();
            foreach (var item in buttons)
            {
                item.onClick.AddListener(() =>
                {
                    print(item.transform.GetPath());
                    item.transform.DOPunchScale(Vector3.one * 0.9f, 0.4f);
                });
            }

            //GetComponentsInChildren<Button>().ForEach(item =>
            //{
            //    item.onClick.AddListener(() =>
            //    {
            //        print(item.transform.GetPath());
            //        item.transform.DOPunchScale(Vector3.one * 0.9f, 0.4f);
            //    });
            //});
        }
    }

    //static public class Ext
    //{
    //    public static void ForEach<T>(this IEnumerable<T> list, System.Action<T> action)
    //    {
    //        foreach (var item in list)
    //            action(item);
    //    }
    //}
}