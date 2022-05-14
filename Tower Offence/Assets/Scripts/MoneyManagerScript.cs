using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagerScript : MonoBehaviour
{
    public static MoneyManagerScript Instance;
    public int MoneyCount;
    public Text MoneyText;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = MoneyCount.ToString();
    }
}
