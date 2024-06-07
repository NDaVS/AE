using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    int lvl = 0; //4 переменные под каждый уровень
    Modification modification = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  SetModification(Modification modification)
    {
        this.modification = modification;
    }
}
