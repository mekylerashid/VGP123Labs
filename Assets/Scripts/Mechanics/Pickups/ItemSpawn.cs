using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;
    //[SerializeField] private Pickup lifePrefab;
    [SerializeField] private SimplePickUps coinPrefab;
    [SerializeField] private SimplePickUps mushroomPrefab;
    [SerializeField] private SimplePickUps yoshiCoinPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        MonoBehaviour[] items = { coinPrefab, mushroomPrefab, yoshiCoinPrefab };
        int randomItem = Random.Range(0, items.Length);

        MonoBehaviour chosenItem = items[randomItem];

        Instantiate(chosenItem, spawnPoint.position, Quaternion.identity);
    }
}
