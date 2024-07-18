using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AR : MonoBehaviour
{
    [SerializeField]
    public GameObject PlaneMarkerPrefab;

    private ARRaycastManager raycastManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        PlaneMarkerPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        Debug.Log(hits);
        if (hits.Count > 0)
        {
            PlaneMarkerPrefab.transform.position = hits[0].pose.position;
            PlaneMarkerPrefab.SetActive(true);
        }
    }

}
