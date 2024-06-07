using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected int money = 100;
    protected int res1 = 10;
    protected int res2 = 10;
    protected int epoch = 1; // 1 - язычество, 2 -каменный век, 3 - капитализм
    protected Dictionary<Vector3, Tile> spawnableObjects = new Dictionary<Vector3, Tile>();
    protected Dictionary<Vector3, Modification> modificationObjects = new Dictionary<Vector3, Modification>();


    public abstract void PayingForBuilding(List<int> cost);
    public abstract bool IsReadyToUpdate(List<int> cost);
    public void AddTile(Vector3 position, Tile tile)
    {
        spawnableObjects.Add(position, tile);
    }

    public void DeleteTile(Vector3 position)
    {
        spawnableObjects.Remove(position);
    }

    public void AddModification(Vector3 position, Modification modification)
    {
        modificationObjects.Add(position, modification);
    }

    public void DeleteModification(Vector3 position)
    {
        modificationObjects.Remove(position);
    }

    
    
}
