using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
        
    }
    

    public Vector3 GetSelectedMapPosition()
    {
        Vector2 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);
        //mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        //Gizmos.DrawRay(ray);
        Physics.Raycast(ray, out hit, 1000, placementLayermask);
        //Debug.Log(hit.point);
        if (Physics.Raycast(ray, out hit, 1000, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;

    }
}
