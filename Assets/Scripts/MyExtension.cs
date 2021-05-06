using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Ȯ���Լ� ����, �Լ� �̸��� �߿����� ����.
/// </summary>
static public class MyExtension
{
    static public string ToNumber(this int value)
    {
        return $"{value:N0}";
    }

    /// <summary>
    /// ��θ� �����ϴ� �Լ�
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    static public string GetPath(this Transform t)
    {
        // �θ� ������ �θ� ��ο� ��� �����ڸ� �ִ´�.
        StringBuilder sb = new StringBuilder();
        GetParentPath(t, sb);
        return sb.ToString();

        void GetParentPath(Transform tr, StringBuilder sb)
        {
            if (tr.parent != null)
            {
                GetParentPath(tr.parent, sb);

                sb.Append(tr.parent.name);
                sb.Append(System.IO.Path.DirectorySeparatorChar);
            }

            sb.Append(tr.name);
        }
    }
}