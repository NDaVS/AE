using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Modification
{
    [SerializeField]
    TimberMan update1;

    [SerializeField]
    TimberWoods update1_1;
    protected string type = "wood";

    private void Start()
    {
        res1Cost = 5;
        res2Cost = 5;
    }

    public override void Upgrade()
    {
        if (this.level == 0)
        {
            isModified = true;
            this.level = 1;
            var man = Instantiate(update1, this.position, this.rotation);
            man.transform.parent = transform;
            var timberWoods = Instantiate(update1_1, this.position, this.rotation);
            timberWoods.transform.parent = transform;
            man.transform.DOPunchScale(Vector3.one * 1.1f, 0.1f, 3).SetEase(Ease.InCirc).Play();
            timberWoods.transform.DOPunchScale(Vector3.one * 1.1f, 0.1f, 3).SetEase(Ease.InCirc).Play();
            res1Income += 2;
        }
    }
    public override List<int> getCost()
    {
        List<int> cost = new List<int>();
        cost.Add(res1Cost);
        cost.Add(res2Cost);
        return cost;
        
    }
    public override List<int> getIncome()
    {
        List<int> income = new List<int>();
        income.Add(res1Income);
        income.Add(res1Income);
        return income;
    }

    public string GetModificationType()
    {
        return type;
    }
}
