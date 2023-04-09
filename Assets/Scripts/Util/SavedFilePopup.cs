using System.IO;
using TMPro;
using UnityEngine;

public class SavedFilePopup : MonoBehaviour
{
    public TextMeshProUGUI[] texts;

    string subPath;


    private void OnEnable()
    {
        subPath = DataManager.Instance.path.Substring(0, DataManager.Instance.path.Length - 1);//뒤에 마지막 문자 자르기

        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(subPath + $"{i}"))
            {
                if (i == 0)
                {
                    DataManager.Instance.path = subPath + $"{i}";
                    DataManager.Instance.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {DataManager.Instance.dateTime0}";
                }

                if (i == 1)
                {
                    DataManager.Instance.path = subPath + $"{i}";
                    DataManager.Instance.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {DataManager.Instance.dateTime1}";
                }

                if (i == 2)
                {
                    DataManager.Instance.path = subPath + $"{i}";
                    DataManager.Instance.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {DataManager.Instance.dateTime2}";
                }
            }
            else
            {
                texts[i].text = TextUtil.GetText("game:start:empty");
            }
        }
    }

    private void Start()
    {
        subPath = DataManager.Instance.path.Substring(0, DataManager.Instance.path.Length - 1);//뒤에 마지막 문자 자르기

        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(subPath + $"{i}"))
            {
                if (i == 0)
                {
                    DataManager.Instance.path = subPath + $"{i}";
                    DataManager.Instance.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {DataManager.Instance.dateTime0}";
                }

                if (i == 1)
                {
                    DataManager.Instance.path = subPath + $"{i}";
                    DataManager.Instance.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {DataManager.Instance.dateTime1}";
                }

                if (i == 2)
                {
                    DataManager.Instance.path = subPath + $"{i}";
                    DataManager.Instance.LoadData();
                    texts[i].text = $"Slot {i + 1}\nSave date/time : {DataManager.Instance.dateTime2}";
                }
            }
            else
            {
                texts[i].text = TextUtil.GetText("game:start:empty");
            }
        }
    }
}
