using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{

    private Dictionary<Vector3, Tile> spawnableObjects = new Dictionary<Vector3, Tile>();
    private Dictionary<Vector3, Modification> modificationObjects = new Dictionary<Vector3, Modification>();
    [SerializeField]
    private string mode;


    System.Random rand = new System.Random();
    [SerializeField]
    private RandomCellModificator[] cellModificators;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject mouseIndicator;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Grass grass;

    [SerializeField]
    private Tree tree;

    [SerializeField]
    private Stone stone;

    [SerializeField]
    private HumanMainBuildBase mainBuildBase;

    [SerializeField]
    private float gridSize = 1; // размер €чейки сетки

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Vector3 gridOffset = Vector3.one / 2f; // размер €чейки сетки

    [SerializeField]
    private float probability = 0.3f;

    private IEnumerator Start()
    {
        mode = "sleep";
        //заполнение базовыми плитками
        for (int i = -2; i < 2; i++)
        {
            for (int j =17; j < 20; j++)
            {
                var spawnPosition = getGridPos(new Vector3(j, 0, i)) + gridOffset;
                SpawnBase(spawnPosition);
                yield return new WaitForSeconds(.05f);
            }
        }
        int z = rand.Next(-2, 2);
        int x = rand.Next(18, 20);
        SpawnModification(new Vector3(x, 0, z) + gridOffset, mainBuildBase);

        //строим города
        for (int i = -2; i < 2; i++)
        {
            for (int j = 17; j < 20; j++)
            {
                var spawnPosition = getGridPos(new Vector3(j, 0, i)) + gridOffset;
                ModificationSpawn(spawnPosition);
                yield return new WaitForSeconds(.05f);
            }
        }
    }

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3 cellPosition = getGridPos(mousePosition) + gridOffset;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if ((Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0)  && Input.GetKey(KeyCode.LeftControl))) && mode == "create" && !spawnableObjects.ContainsKey(cellPosition))
            {
                SpawnBase(cellPosition);
                ModificationSpawn(cellPosition);
            }
            else if (Input.GetMouseButtonDown(0) && mode == "build" && modificationObjects.TryGetValue(cellPosition, out var modification))
            {
                Debug.Log(modification.GetModificationType());
                if (player.IsReadyToUpdate(modification.getCost()))
                {
                    modification.Upgrade();
                    player.PayingForBuilding(modification.getCost());
                }


            }

            if (Input.GetMouseButtonDown(1) && mode == "delete" && spawnableObjects.TryGetValue(cellPosition, out var tileToDel))
            {
                spawnableObjects.Remove(cellPosition);
                player.DeleteTile(cellPosition);
                tileToDel.transform.DOScale(0, 0.3f).SetEase(Ease.InCirc).OnComplete(() => Destroy(tileToDel.gameObject)).Play();
                if (modificationObjects.TryGetValue(cellPosition, out var modificationToDel))
                {
                    modificationToDel.transform.DOScale(0, 0.5f).SetEase(Ease.InCirc).OnComplete(() => Destroy(modificationToDel)).Play();
                    modificationObjects.Remove(cellPosition);
                    player.DeleteModification(cellPosition);
                }
            }
        }
        

        // ќбновление позиции указател€ мыши
        mouseIndicator.transform.position = cellPosition;
        
        
    }

    private Vector3 getGridPos(Vector3 mousePosition) {
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        return grid.CellToWorld(gridPosition);
    }

    private void SpawnBase(Vector3 spawnPosition)
    {
        var spawnTile = Instantiate(tile, spawnPosition, Quaternion.identity);
        spawnTile.transform.DOPunchScale(Vector3.one * 1.1f, 0.1f, 3).SetEase(Ease.InCirc).Play();
        spawnableObjects.Add(spawnPosition, spawnTile);
        player.AddTile(spawnPosition, spawnTile);
    }
    private void SpawnModification(Vector3 spawnPosition, Modification modification)
    {
        Quaternion rotation = Quaternion.Euler(0f, Random.Range(0, 4) * 90f, 0f);
        var spawnTile = Instantiate(modification, spawnPosition, rotation);
        spawnTile.SetTransform(spawnPosition, rotation);
        spawnTile.transform.DOPunchScale(Vector3.one * 1.1f, 0.1f, 3).SetEase(Ease.InCirc).Play();
        modificationObjects.Add(spawnPosition, spawnTile);
        player.AddModification(spawnPosition, spawnTile);
        var baseTile = spawnableObjects[spawnPosition];
        baseTile.SetModification(spawnTile);
    }


    private void ModificationSpawn(Vector3 spawnPosition) 
    {
        if (!modificationObjects.ContainsKey(spawnPosition)){

            if (Mathf.PerlinNoise(spawnPosition.x + 1000, spawnPosition.z+1000) < probability)
            {
                float value = Mathf.PerlinNoise(spawnPosition.x, spawnPosition.z);
                var randomEx = RandomExtension.ProceedValue(value, cellModificators);
                SpawnModification(spawnPosition, cellModificators[randomEx].modification);
            }

        }

    }

    public void setModeDelete()
    {
        if (mode != "delete")
        {
            mode = "delete";
        }
        else
        {
            mode = "sleep";
        }
        Debug.Log(mode);
    }

    public void setModeBuild()
    {
        if (mode != "build")
        {
            mode = "build";
        }
        else
        {
            mode = "sleep";
        }
        Debug.Log(mode);
    }

    public void setModeCreatecell()
    {
        if (mode != "create")
        {
            mode = "create";
        }
        else
        {
            mode = "sleep";
        }
        Debug.Log(mode);
    }

    public void setModeSleep()
    {
        mode = "sleep";
    }

}
