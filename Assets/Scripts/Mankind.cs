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
        if (mods.Count > 0)
        {
            foreach (var item in mods)
            {
                if (item.IsModified())
                {
                    List<int> income = item.getIncome();
                    res1 += income[0];
                    res2 += income[1];
                }
            }

            UIController.Instance.UpdateUIRes1(res1);
            UIController.Instance.UpdateUIRes2(res2);
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
    }

    public void SetMods(List<Modification> modifications)
    {
        this.mods = modifications;
    }


}
