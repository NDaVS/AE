using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInputManager : MonoBehaviour
{
    private ARRaycastManager raycastManager;

    [SerializeField] private Camera ARCamera;

    private Vector3 lastPosition;

    


    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }


    public Vector3 GetSelectedMapPosition()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            lastPosition = hits[0].pose.position;
        }
        return lastPosition;

    }

    public Vector3 GetTouchMapPosition(Vector2 pos)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(pos, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            lastPosition = hits[0].pose.position;
        }

        return new Vector3(
            Mathf.Round(lastPosition.x * 1000f) / 1000f,
            Mathf.Round(lastPosition.y * 1000f) / 1000f,
            Mathf.Round(lastPosition.z * 1000f) / 1000f
        );
        

    }

    public Modification GetObjectByTouch(Vector2 tochPosition)
    {
        Ray ray = ARCamera.ScreenPointToRay(tochPosition);
        RaycastHit hitObject;
        Modification SelectedObject = null;
        if (Physics.Raycast(ray, out hitObject))
        {
            if (hitObject.collider.CompareTag("modification")){
                Debug.Log("AM IN modification");
                SelectedObject = hitObject.collider.GetComponent<Modification>();
            }
        }
        return SelectedObject;
    }
}
