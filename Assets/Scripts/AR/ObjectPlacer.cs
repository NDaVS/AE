using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    public programmManager PM;

    [SerializeField]
    public Modification Tree;

    [SerializeField]
    public Modification Stone;

    [SerializeField]
    public Tile tile;

    [SerializeField]
    public HumanBuilding HomeBuilding;

    [SerializeField]
    public MainGame mainGame;

    private bool isModified = false;

    private List<Modification> modifications = new List<Modification>();

    private HumanBuilding home;

    [SerializeField]
    private Mankind player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.IsPlaced() && !isModified)
        {
            
            mainGame.RemoveStartUI();
            List<GameObject> towers = PM.GetTowers();
            List<float> xs = new List<float>();
            List<float> zs = new List<float>();

            xs.Add(towers[0].transform.position.x);
            xs.Add(towers[1].transform.position.x);
            xs.Sort();

            zs.Add(towers[0].transform.position.z);
            zs.Add(towers[1].transform.position.z);
            zs.Sort();

            float y = towers[0].transform.position.y;

            PlaceHome(xs, zs, y);
            PlaceResourses(xs, zs, y);
            isModified = true;
        }        
    }

    private void PlaceHome(List<float> xs, List<float> zs, float y)
    {

        home = Instantiate(HomeBuilding, new Vector3((xs[0] + xs[1])/2, y, (zs[0] + zs[1]) / 2), Quaternion.identity);
        var vartile = Instantiate(tile, new Vector3((xs[0] + xs[1]) / 2, y, (zs[0] + zs[1]) / 2), Quaternion.identity);
        vartile.transform.SetParent(home.transform);

    }

    private void PlaceResourses(List<float> xs, List<float> zs, float y) {
        Vector3 center = new Vector3((xs[0] + xs[1]) / 2, y, (zs[0] + zs[1]) / 2); 
        for(int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                var tree = Instantiate(Tree, new Vector3((center.x + xs[i]) / 2, y, (center.z + zs[j]) / 2), Quaternion.identity);
                var vartile = Instantiate(tile, new Vector3((center.x + xs[i]) / 2, y, (center.z + zs[j]) / 2), Quaternion.identity);
                vartile.transform.SetParent(tree.transform);
                modifications.Add(tree);

            }
        }

        Vector3 position = home.transform.position;
        int lu_tree = 0;
        for (int i = 1; i < 3 ; i++)
        {
            Vector3 position1 = (modifications[lu_tree].transform.position + modifications[lu_tree+i].transform.position) / 2;
            Debug.Log(modifications[i].transform.position);
            var stone = Instantiate(Stone, position1, Quaternion.identity);
            var vartile1 = Instantiate(tile, position1, Quaternion.identity);
            vartile1.transform.SetParent(stone.transform);
            modifications.Add(stone);
        }
        lu_tree = 3;
        for (int i = 1; i < 3; i++)
        {
            Vector3 position1 = (modifications[lu_tree].transform.position + modifications[lu_tree - i].transform.position) / 2;
            Debug.Log(modifications[i].transform.position);
            var stone = Instantiate(Stone, position1, Quaternion.identity);
            var vartile1 = Instantiate(tile, position1, Quaternion.identity);
            vartile1.transform.SetParent(stone.transform);
            modifications.Add(stone);
        }

        player.SetMods(modifications);

    }
}
