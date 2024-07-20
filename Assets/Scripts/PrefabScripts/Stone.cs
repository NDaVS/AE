using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Stone : Modification
{
    [SerializeField]
    MineMan update1;

    [SerializeField]
    MineBase update1_1;
    protected string type = "stone";
    MineMan man;
    MineBase mineBase;

    private void Start()
    {
        man = new MineMan(); 
        mineBase = new MineBase();
        res1Cost = 5;
        res2Cost = 5;
    }

    public override void Upgrade()
    {
        if (this.level == 0)
        {
            isModified = true;
            this.level = 1;

            // Instantiate and set the parent before setting local position
            man = Instantiate(update1, Vector3.zero, this.rotation);
            man.transform.parent = transform;
            man.transform.localPosition = Vector3.zero;
            man.transform.localScale = Vector3.one;
            man.transform.DOPunchScale(Vector3.one * 1.1f, 0.1f, 3).SetEase(Ease.InCirc).Play();

            mineBase = Instantiate(update1_1, Vector3.zero, this.rotation);
            mineBase.transform.parent = transform;
            mineBase.transform.localPosition = Vector3.zero;
            mineBase.transform.localScale = Vector3.one;
            mineBase.transform.DOPunchScale(Vector3.one * 1.1f, 0.1f, 3).SetEase(Ease.InCirc).Play();

            res2Income += 2;

            Debug.Log(this.position);
            Debug.Log(man.transform.position);
        }
    }

    public string GetModificationType()
    {
        return type;
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
        income.Add(res2Income);
        return income;
    }


}
