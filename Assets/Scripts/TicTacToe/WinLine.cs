using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLine : MonoBehaviour
{
    public RectTransform line;

    public float offset = 150f;

    public void Show(int index)
    {
        gameObject.SetActive(true);

        line.localRotation = Quaternion.identity;

        switch (index)
        {
            case 0: line.localPosition = new Vector3(0, offset, 0); break;
            case 1: line.localPosition = Vector3.zero; break;
            case 2: line.localPosition = new Vector3(0, -offset, 0); break;

            case 3: line.localRotation = Quaternion.Euler(0, 0, 90); line.localPosition = new Vector3(-offset, 0, 0); break;
            case 4: line.localRotation = Quaternion.Euler(0, 0, 90); line.localPosition = Vector3.zero; break;
            case 5: line.localRotation = Quaternion.Euler(0, 0, 90); line.localPosition = new Vector3(offset, 0, 0); break;

            // диагонали
            case 6: line.localRotation = Quaternion.Euler(0, 0, 45); line.localPosition = Vector3.zero; break;
            case 7: line.localRotation = Quaternion.Euler(0, 0, -45); line.localPosition = Vector3.zero; break;

        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
