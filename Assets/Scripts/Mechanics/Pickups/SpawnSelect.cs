using UnityEngine;

public class SpawnSelect : MonoBehaviour
{
    [SerializeField] protected SimplePickUps coinPrefab;
    [SerializeField] protected SimplePickUps mushroomPrefab;
    [SerializeField] protected SimplePickUps yoshiCoinPrefab;
    protected static MonoBehaviour chosenItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MonoBehaviour[] items = { coinPrefab, mushroomPrefab, yoshiCoinPrefab };
        int randomItem = Random.Range(0, items.Length);

        chosenItem = items[randomItem];
    }

}
