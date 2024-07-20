using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject gridPrefab;
    public int rows = 40;
    public int columns = 40;

    private GameObject grid;
    private GameObject selectedObject;
    public GameObject example;

    public void InitializeGrid()
    {
        selectedObject = GameObject.FindGameObjectWithTag("plain");
        // Если сетка уже существует, уничтожаем ее
        if (grid != null)
        {
            Destroy(grid);
        }

        // Создаем новый объект сетки
        grid = Instantiate(gridPrefab, selectedObject.transform.position, Quaternion.identity);
        grid.transform.localScale = selectedObject.transform.localScale;
        grid.transform.SetParent(selectedObject.transform);
        grid.transform.position = selectedObject.transform.position;
        grid.transform.rotation = selectedObject.transform.rotation;

        SpawnObjectInCell(1, 1, example);

        
    }

    public void SpawnObjectInCell(int row, int column, GameObject objectPrefab)
    {
        if (row < 0 || row >= rows || column < 0 || column >= columns)
        {
            Debug.LogError("Cell coordinates are out of bounds!");
            return;
        }

        Vector3 spawnPosition = new Vector3(column * 1, 0, row * 1);
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        spawnedObject.transform.SetParent(grid.transform);
    }
}
