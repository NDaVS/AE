using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class Mankind : Player
{
    [SerializeField]
    TextMeshProUGUI textRes1;

    [SerializeField]
    TextMeshProUGUI textRes2;

    

    private List<Modification> mods;
    private HumanMainBuild1 home;

    private int numberOfPays = 0;

    private float woodFactor = 1;
    private float stoneFactor = 1;

    public void SetOn()
    {
        Observer.OnTimedEvent += HandleEvent;
    }

    private void OnDisable()
    {
        Observer.OnTimedEvent -= HandleEvent;

    }

    private void HandleEvent() 
    {
        if (mods.Count > 0)// рассчёт прироста ресурсов
        {
            foreach (var item in mods)
            {
                if (item.IsModified())
                {
                    List<int> income = item.getIncome();
                    res1 += (int) (income[0] * this.woodFactor);
                    res2 += (int) (income[1] * this.stoneFactor);
                }
            }

            UIController.Instance.UpdateUIRes1(res1);
            UIController.Instance.UpdateUIRes2(res2);
            UIController.Instance.UpdateUIRes3(money);
        }
        
        
    }
    public override bool IsReadyToUpdate(List<int> cost)
    {
        if (res1 - cost[0] < 0 || res2 - cost[1] < 0)
        {
            return false;
        }
        return true;
    }

    public override void PayingForBuilding(List<int> cost)
    {
        res1 -= cost[0];
        res2 -= cost[1];
        UIController.Instance.UpdateUIRes1(res1);
        UIController.Instance.UpdateUIRes2(res2);
        UIController.Instance.UpdateUIRes3(money);
    }

    public void SetMods(List<Modification> modifications)
    {
        this.mods = modifications;
    }

    public List<int> GetResourses()
    {
        List<int> ress = new List<int>();
        ress.Add(res1);
        ress.Add(res2);
        ress.Add(money);
        return ress;
    }

    public float GetPayCost()
    {

        return this.money * 1.5f * numberOfPays;
    }

    public void AddResourses(List<int> resourses)
    {
        res1 += resourses[0];
        res2 += resourses[1];
        money += resourses[2];
        UIController.Instance.UpdateUIRes1(res1);
        UIController.Instance.UpdateUIRes2(res2);
        UIController.Instance.UpdateUIRes3(money);
    }

    public void addPay() {
        this.numberOfPays += 1;
    }


}
