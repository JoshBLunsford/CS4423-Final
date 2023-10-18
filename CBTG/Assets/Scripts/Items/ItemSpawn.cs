using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject genericItem;
    public ItemData[] possibleItems;
    void Start()
    {
        SpawnItem();
    }

    public void SpawnItem(ItemData data = null)
    {
        data ??= possibleItems[Random.Range(0, possibleItems.Length)];
        Item newItem = Instantiate(genericItem.gameObject).GetComponent<Item>();
        newItem.data = data;
        newItem.Dropped();
        newItem.transform.position = transform.position;
        newItem.gameObject.SetActive(true);
    }
}
