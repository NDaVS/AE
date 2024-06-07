using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modification : MonoBehaviour
{

    protected Vector3 position = Vector3.zero;
    protected Quaternion rotation = Quaternion.identity;
    protected int level = 0;
    protected int res1Cost = 0;
    protected int res2Cost = 0;
    protected int res1Income = 0;
    protected int res2Income= 0;
    protected bool isModified = false; 
    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
    public abstract void Upgrade();

    public string GetModificationType()
    {
        return "base";
    }

    public abstract List<int> getCost();
    public abstract List<int> getIncome();

    public bool IsModified()
    {
        return isModified;
    }


}
