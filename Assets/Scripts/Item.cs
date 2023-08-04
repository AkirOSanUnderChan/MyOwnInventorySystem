using UnityEngine;


[CreateAssetMenu(fileName = "New item object", menuName = "My/Inventory System/Items")]
[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;
    public bool stackable; // ������ ����, �� ���� ������� ���������
    public int maxStack; // ����������� ������� �������� � �����
    public int currentStack; // ������� ������� �������� � �����
}

//public sealed class ItemData
//{
//    public string UniqueName;
//    public int StackCount;
//}
