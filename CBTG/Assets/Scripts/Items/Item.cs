using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public ItemData data;
    public SpriteRenderer sr;
    public void Dropped()
    {
        sr.sprite = data.visual;
    }
    public void PickUp()
    {
        data.PickedUp();
        gameObject.SetActive(false);
    }

    public void ReturnToPool()
    {
        //You think I had time to make a pooling system?
    }
}
