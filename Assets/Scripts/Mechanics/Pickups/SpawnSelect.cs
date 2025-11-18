using UnityEngine;

public class SpawnSelect : MonoBehaviour
{
    [SerializeField] protected GameObject coinPrefab;
    [SerializeField] protected GameObject mushroomPrefab;
    [SerializeField] protected GameObject yoshiCoinPrefab;


    [SerializeField] protected ItemSpawn[] spawners;
    protected GameObject chosenItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] items = { coinPrefab, mushroomPrefab, yoshiCoinPrefab };
        int randomItem = Random.Range(0, items.Length);

        chosenItem = items[randomItem];

        foreach (ItemSpawn spawner in spawners)
        {
            spawner.SpawnItem(chosenItem);
        }
    }

}
