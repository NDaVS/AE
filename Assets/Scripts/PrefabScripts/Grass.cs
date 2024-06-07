using System.Collections.Generic;
using UnityEngine;

public class Grass : Modification
{
    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }

    public override List<int> getCost()
    {
        List<int> cost = new List<int>();
        cost.Add(0);
        cost.Add(0);
        return cost;

    }

    public override List<int> getIncome()
    {
        List<int> income = new List<int>();
        income.Add(0);
        income.Add(0);
        return income;
    }
}
