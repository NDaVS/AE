using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
using System;
using System.Collections.Generic;


public class MarketScript : MonoBehaviour
{
    [SerializeField]
    private Mankind player;

    [SerializeField]
    private GameObject Market;

    [SerializeField]
    private GameObject SellUI;

    [SerializeField]
    private GameObject BuyUI;

    [SerializeField]
    private GameObject UpgradeUI;

    [SerializeField]
    private TextMeshProUGUI SellWood;

    [SerializeField]
    private TextMeshProUGUI SellStone;

    [SerializeField]
    private TextMeshProUGUI SellGold;

    [SerializeField]
    private TextMeshProUGUI BuyWood;

    [SerializeField]
    private TextMeshProUGUI BuyStone;

    [SerializeField]
    private TextMeshProUGUI BuyGold;

    [SerializeField]
    private TextMeshProUGUI UpgradeWood;

    [SerializeField]
    private TextMeshProUGUI UpgradeStone;

    [SerializeField]
    private TextMeshProUGUI UpgradeGold;

    public int UpdateWood;
    public int UpdateStone;
    public int UpdateGold;

    private int wood;
    private int stone;
    private int gold;
    private int soldWood;
    private int soldStone;
    static double totalWoodExchanged = 0;
    static double totalStoneExchanged = 0;
    static double baseFactor = 0.005;


    public void SetMarket()
    {
        Market.SetActive(true);
        BuyUI.SetActive(false);
        UpgradeUI.SetActive(false);
        SellUI.SetActive(false);
    }

    public void CloseMarket()
    {
        Market.SetActive(false);
    }

    public void SetSell()
    {

        ResZeros();

        SellWood.text = "" + 0;
        SellStone.text = "" + 0;
        SellGold.text = "" + 0;

        

        BuyUI.SetActive(false);
        UpgradeUI.SetActive(false);
        SellUI.SetActive(true);
        
    }

    public void SetBuy()
    {
        ResZeros();

        BuyWood.text = "" + 0;
        BuyStone.text = "" + 0;
        BuyGold.text = "" + 0;

        UpgradeUI.SetActive(false);
        SellUI.SetActive(false);
        BuyUI.SetActive(true);
    }

    public void SetUpgrade()
    {
        
        SellUI.SetActive(false);
        BuyUI.SetActive(false);
        UpgradeUI.SetActive(true);

        UpdateRequirements();
    }


    public void PlusSellWood()
    {


        wood+= 5;
        if (wood > player.GetResourses()[0])
        {
            wood = player.GetResourses()[0];
        }
        SellWood.text = "" + wood;
        GoldUpdate(SellGold, "+");
    }

    public void PlusSellStone()
    {

        stone += 5;
        if (stone > player.GetResourses()[1])
        {
            stone = player.GetResourses()[1];
        }

        SellStone.text = "" + stone;
        GoldUpdate(SellGold, "+");
    }

    public void MinusSellWood()
    {
        if (wood > 0)
        {
            wood -= 1;
        }
        

        SellWood.text = "" + wood;
        GoldUpdate(SellGold, "+");

    }

    public void MinusSellStone()
    {
        if (wood > 0)
        {
            stone -= 1;
        }
        

        SellStone.text = "" + stone;
        GoldUpdate(SellGold, "+");

    }

    public void PlusBuyWood()
    {
        if (GetPrices()[0] * (wood + 5) <= player.GetResourses()[2])
        {
            wood += 5;
        }

        BuyWood.text = "" + wood;
        GoldUpdate(BuyGold, "-");
    }

    public void PlusBuyStone()
    {
        if (GetPrices()[1] * (stone + 5) <= player.GetResourses()[2])
        {
            stone += 5;
        }

        BuyStone.text = "" + stone;
        GoldUpdate(BuyGold, "-");
    }

    public void MinusBuyWood()
    {
        if (wood > 1)
        {
            wood -= 1;
        }

        BuyWood.text = "" + wood;
        GoldUpdate(BuyGold, "-");

    }

    public void MinusBuyStone()
    {
        if (stone > 1)
        {
            stone -= 1;
        }
        
        BuyStone.text = "" + stone;
        GoldUpdate(BuyGold, "-");

    }

    public void ConfirmSell()
    {
        List<int> soldResourses= new List<int>();
        soldResourses.Add(-wood);
        soldResourses.Add(-stone);
        soldResourses.Add(gold);
        player.AddResourses(soldResourses);
        ResZeros();
        SellGold.text = "0";
    }

    public void ConfirmBuy()
    {
        List<int> soldResourses = new List<int>();
        soldResourses.Add(wood);
        soldResourses.Add(stone);
        soldResourses.Add(-gold);
        player.AddResourses(soldResourses);
        ResZeros();
        BuyGold.text = "0";
    }

    public void Upgrade(int sceneNumber)
    {
        if (UpdateGold >= player.GetPayCost())
        {
            player.addPay();
        }
    }


    private void Start()
    {
        Market.SetActive(false);
    }

    private void GoldUpdate(TextMeshProUGUI gold, string sign)
    {
        int addGold = (int) Math.Floor(
            stone * Math.Pow(1 - baseFactor, totalStoneExchanged) +
            wood * Math.Pow(1-baseFactor, totalWoodExchanged)
            ); 
        this.gold = addGold;
        gold.text = "" + player.GetResourses()[2]+sign + addGold;
    }
    


    private void ResZeros()
    {
        wood = 0;
        stone = 0;
        gold = 0;
    }

    private void UpdateRequirements()
    {
        List<int> playerResourses = player.GetResourses();
        UpgradeWood.text = "" + playerResourses[0] + "/" + UpdateWood;
        UpgradeStone.text = "" + playerResourses[1] + "/" + UpdateWood;
        UpgradeGold.text = "" + playerResourses[2] + "/" + UpdateGold;
    }
    private List<int> GetPrices()
    {
        List<int> prices = new List<int>();
        prices.Add(1);
        prices.Add(1);
        prices.Add(1);
        return (prices);
    }




}
