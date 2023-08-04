using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool isHovered;

    public delegate void MouseEnterAction();
    public event MouseEnterAction onMouseEnterAction;

    public delegate void MouseExitAction();
    public event MouseExitAction onMouseExitAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(item);
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        if (onMouseEnterAction != null)
        {
            onMouseEnterAction();
        }
    }

    private void OnMouseExit()
    {
        isHovered = false;
        if (onMouseExitAction != null)
        {
            onMouseExitAction();
        }
    }

    public bool IsHovered()
    {
        return isHovered;
    }
}
