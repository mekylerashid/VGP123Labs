using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public void SpawnItem(GameObject itemPrefab) => Instantiate(itemPrefab, transform.position, Quaternion.identity);
}
