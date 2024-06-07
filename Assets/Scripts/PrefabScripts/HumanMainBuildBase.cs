using System.Collections.Generic;
using UnityEngine;

public class HumanMainBuildBase : Modification
{
    public HumanBuilding[] objects;
    private System.Collections.Generic.List<HumanBuilding> levels = new System.Collections.Generic.List<HumanBuilding>(); // Инициализация списка
    int current_level = 0;

    private void Start()
    {
        res1Cost = 5;
        res2Cost = 5;
        for (int i = 0; i < objects.Length; i++)
        {
            // Получение компонента HumanBuilding
            var tile = objects[i].GetComponent<HumanBuilding>();
            Debug.Log(tile.name);

            // Создание копии объекта
            var spawnTile = Instantiate(objects[i], gameObject.transform.position, Quaternion.identity);
            spawnTile.gameObject.SetActive(false);
            spawnTile.transform.parent = transform;

            // Добавление в список уровней
            levels.Add(spawnTile);
        }
        if (levels.Count > 0)
        {
            levels[0].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("No levels available to activate.");
        }
    }

    public override void Upgrade()
    {
        if (current_level < levels.Count - 1)
        {
            current_level++;
            SwitchObjects(current_level);
        }
        else
        {
            Debug.LogWarning("Already at the highest level.");
        }
    }

    private void SwitchObjects(int lvl)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].gameObject.SetActive(i == lvl);
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
        income.Add(res2Income);
        return income;
    }
}
