using UnityEngine;

public class ItemSpawn : SpawnSelect
{

    [SerializeField] protected Transform spawnPoint;
    //[SerializeField] private Pickup lifePrefab;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Instantiate(chosenItem, spawnPoint.position, Quaternion.identity);
    }
}
