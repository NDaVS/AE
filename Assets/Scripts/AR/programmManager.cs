using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class programmManager : MonoBehaviour
{
    public GameObject PlaneMarkerPrefab;
    public GameObject Plane;

    private bool isPlanePlaced = false;

    [SerializeField]
    private ARInputManager arinputManager;

    private ARRaycastManager raycastManager;

    [SerializeField] private Camera ARCamera;

    private GameObject selectedObject;

    private Vector2 TouchPosition;

    [SerializeField]
    private LayerMask placementLayermask;


    private Vector2 initialTouchPosition;
    private Vector3 initialObjectPosition;

    private bool isMoving = false;

    private float initialDistance;
    private Vector3 initialScale;
    private bool isScaling = false;


    public float rotationSpeed = 100f; // Скорость вращения
    private bool isRotatingClockwise = false;
    private bool isRotatingCounterClockwise = false;





    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        PlaneMarkerPrefab.SetActive(false);
        Debug.Log(raycastManager);
    }

    
    void Update()
    {
        //List<ARRaycastHit> hits = new List<ARRaycastHit>();
        //raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        //Debug.Log(hits.Count);
        

        PlaneMarkerPrefab.SetActive(true);
        Vector3 hitPosition = arinputManager.GetSelectedMapPosition();
        PlaneMarkerPrefab.transform.position = hitPosition;
        if (!isPlanePlaced && Input.GetMouseButtonDown(0))
        {
            isPlanePlaced = true;
            SpawnPlane(hitPosition);
        }
        selectedObject = GameObject.FindGameObjectWithTag("plain");
        MoveObject();

        if (selectedObject != null)
        {
            if (isRotatingClockwise)
            {
                selectedObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
            }

            if (isRotatingCounterClockwise)
            {
                selectedObject.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
            }
        }
    }

    private void SpawnPlane(Vector3 pos)
    {
        var spawnTile = Instantiate(Plane, pos + new Vector3(0, 0.01f, 0), Quaternion.identity);
        //spawnTile.transform.Rotate(-90, 0, 0);
    }


    void MoveObject()
    {
        selectedObject = GameObject.FindGameObjectWithTag("plain");

        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1Pos = touch1.position;
            Vector2 touch2Pos = touch2.position;

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch1Pos, touch2Pos);
                initialScale = selectedObject.transform.localScale;
                isScaling = true;
            }

            if (isScaling && (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved))
            {
                float currentDistance = Vector2.Distance(touch1Pos, touch2Pos);
                float scaleFactor = currentDistance / initialDistance;

                selectedObject.transform.localScale = initialScale * scaleFactor;
            }

            if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Canceled)
            {
                isScaling = false;
            }
        }
            // Ensure touch input is detected
        if (Input.touchCount == 1)
        {
            // Get the first touch input
            Touch touch = Input.GetTouch(0);
            Vector2 currentTouchPosition = touch.position;

            // Check if touch has just begun
            if (touch.phase == TouchPhase.Began)
            {

                initialTouchPosition = currentTouchPosition;
                initialObjectPosition = selectedObject.transform.position;
                isMoving = true;
            }


            if (isMoving && touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = currentTouchPosition - initialTouchPosition;

                // Преобразование дельты перемещения из экранных координат в мировые
                Vector3 screenDelta = new Vector3(touchDelta.x, touchDelta.y, 0);
                Vector3 worldDelta = ARCamera.ScreenToWorldPoint(screenDelta + new Vector3(0, 0, ARCamera.nearClipPlane)) - ARCamera.ScreenToWorldPoint(new Vector3(0, 0, ARCamera.nearClipPlane));
                Debug.Log(screenDelta);
                // Обновление позиции объекта с учётом дельты
                //selectedObject.transform.position = initialObjectPosition + new Vector3(screenDelta.x, 0, screenDelta.y) * 0.005f;
                selectedObject.transform.position = arinputManager.GetTouchMapPosition(touch.position);
            }

            
        }
    }

    public void StartRotateClockwise()
    {
        isRotatingClockwise = true;
    }

    public void StopRotateClockwise()
    {
        isRotatingClockwise = false;
    }

    public void StartRotateCounterClockwise()
    {
        isRotatingCounterClockwise = true;
    }

    public void StopRotateCounterClockwise()
    {
        isRotatingCounterClockwise = false;
    }
}
