using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI textRes1;

    [SerializeField]
    TextMeshProUGUI textRes2;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<UIController>();
            return _instance;
        }
    }
    private static UIController _instance;
    public void UpdateUIRes1 (int res1)
    {
        textRes1.text = "res1: " + res1;
    }

    public void UpdateUIRes2(int res2)
    {
        textRes2.text = "res2: " + res2;
    }
}
