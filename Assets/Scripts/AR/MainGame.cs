using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    private GameObject SetUI;
    [SerializeField]
    private GameObject MainUI;

    [SerializeField]
    private ARInputManager inputManager;

    private Modification SelectedObject;

    void Start()
    {
        MainUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveStartUI()
    {
        SetUI.SetActive(false);
        MainUI.SetActive(true);
    }

    public Modification GetObject1(Vector2 tochPosition)
    {
        Debug.Log("AM IN");
        Modification newSelectedObject = inputManager.GetObjectByTouch(tochPosition);
        Debug.Log(newSelectedObject);
        if (newSelectedObject != null && SelectedObject != null)
        {
            SelectedObject.transform.localScale = newSelectedObject.transform.localScale;
            SelectedObject = newSelectedObject;
            SelectedObject.transform.localScale = new Vector3(1, 1, 1) / 3;
        }
        else if (newSelectedObject != null && SelectedObject == null)
        {
            SelectedObject = newSelectedObject;
            SelectedObject.transform.localScale = new Vector3(1, 1, 1) / 3;
        }
        else if (newSelectedObject == null)
        {
            if (SelectedObject)
            {
                Debug.Log("empty your mined");
                SelectedObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                SelectedObject = null;
            }
            
        }
        Debug.Log(SelectedObject);
        Debug.Log(newSelectedObject);
        return SelectedObject;
    }

    public void UpgradeModification()
    {
        if (SelectedObject != null)
        {
            SelectedObject.Upgrade();
        }
    }
}
