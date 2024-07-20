using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class programmManager : MonoBehaviour
{
    public GameObject Tower;
    public GameObject Plane;

    private bool isPlaced = false;

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



    private bool mode = true;



    List<GameObject> towers = new List<GameObject>();

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        
    }

    
    void Update()
    {
        if(Input.touchCount > 0 && !isPlaced)
        {
            Debug.Log("Touchme");
            Touch touch1 = Input.GetTouch(0);
            Vector2 touchPos = touch1.position;
            if (mode)
            {
                Debug.Log("first");
                if(towers.Count == 0)
                {
                    var spawnTile = Instantiate(Tower, arinputManager.GetTouchMapPosition(touchPos), Quaternion.identity);
                    towers.Add(spawnTile);
                }
                if (towers.Count > 2)
                {
                    towers[0].transform.position = arinputManager.GetTouchMapPosition(touchPos);

                    Vector3 position3 = towers[0].transform.position;
                    position3.x = towers[1].transform.position.x;
                    towers[2].transform.position = position3;
                    

                    Vector3 position4 = towers[1].transform.position;
                    position4.x = towers[0].transform.position.x;
                    towers[3].transform.position = position4;

                }
                
            }
            else
            {
                Debug.Log("Second");
                if (towers.Count == 1)
                {
                    var spawnTile = Instantiate(Tower, arinputManager.GetTouchMapPosition(touchPos), Quaternion.identity);
                    towers.Add(spawnTile);

                    Vector3 position3 = spawnTile.transform.position;
                    position3.x = towers[1].transform.position.x;
                    var spawnTile3 = Instantiate(Tower, position3, Quaternion.identity);
                    towers.Add(spawnTile3);

                    Vector3 position4 = spawnTile.transform.position;
                    position4.x = towers[0].transform.position.x;
                    var spawnTile4 = Instantiate(Tower, position3, Quaternion.identity);
                    towers.Add(spawnTile4);
                }
                else
                {
                    towers[1].transform.position = arinputManager.GetTouchMapPosition(touchPos);

                    Vector3 position3 = towers[0].transform.position;
                    position3.x = towers[1].transform.position.x;
                    towers[2].transform.position = position3;


                    Vector3 position4 = towers[1].transform.position;
                    position4.x = towers[0].transform.position.x;
                    towers[3].transform.position = position4;
                }
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



    public void SetFirst()
    {
        mode = true;
    }

    public void SetSecond()
    {
        mode = false;
    }

    public void Setplaced()
    {
        if (towers.Count == 4)
        {
            isPlaced = true;
        }
        
    }
    public List<GameObject> GetTowers()
    {
        return towers;
    }

    public bool IsPlaced()
    {
        
        return isPlaced;
    }
}
