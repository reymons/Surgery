using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite SpriteNotSelected { get; set; }

    public Sprite SpriteSelected { get; set; }

    public string Name { get; set; }

    public bool IsSelected { get; set; }

    public Item(Sprite notSelected, Sprite selected, string name, GameObject obj = null, bool isSelected = false)
    {
        SpriteNotSelected = notSelected;
        SpriteSelected = selected;
        Name = name;
        IsSelected = isSelected;
        Object = obj;
    }

    public bool IsBlocked { get; set; }

    public GameObject Object { get; set; }
}
