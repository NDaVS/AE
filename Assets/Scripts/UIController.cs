using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI textRes1;

    [SerializeField]
    TextMeshProUGUI textRes2;

    [SerializeField]
    TextMeshProUGUI textRes3;
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
        textRes1.text = "WOOD: " + res1;
    }

    public void UpdateUIRes2(int res2)
    {
        textRes2.text = "STONE: " + res2;
    }

    public void UpdateUIRes3(int res3)
    {
        textRes2.text = "GOLD: " + res3;
    }
}
