using UnityEngine;


[CreateAssetMenu(fileName = "New item object", menuName = "My/Inventory System/Items")]
[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;
    public bool stackable; // Ознака того, чи може предмет стакатись
    public int maxStack; // Максимальна кількість предметів в стаку
    public int currentStack; // Поточна кількість предметів в стаку
}

//public sealed class ItemData
//{
//    public string UniqueName;
//    public int StackCount;
//}
