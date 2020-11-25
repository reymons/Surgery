using UnityEngine;
using UnityEngine.UI;

public class BasinFilling : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    [SerializeField] private Sprite _filledBasinNotSelected;
    [SerializeField] private Sprite _filledBasinSelected;

    [SerializeField] private Text _fillingText;

    [SerializeField] private GameObject _water;

    private void Start()
    {
        _water.SetActive(false);
    }

    private void Update()
    {
        if (Global.BasinIsInSink)
        {
            if (Global.WaterIsActive && !Global.BasinIsFilled)
            {
                _water.SetActive(true);
                Global.BasinIsFilled = true;
                _fillingText.text = "Вы налили воду в тазик!";
                Global.TextTimer = 3;
            }
        }
    }

    private void OnMouseDown()
    {
        if (_inventory.Items.Count != 0)
        {
            int index = _inventory.SelectedItemIndex;
            if (index != -1)
            {
                if (_inventory.Items[index].Name == "Basin" && !Global.BasinIsInSink)
                {
                    Global.SlotOfReturnable = index;
                    Global.PlayerGrabbed = true;
                    Global.BasinIsInSink = true;
                    
                    Vector3 thisPos = transform.position;
                    Vector3 newPos = new Vector3(thisPos.x + 1.57f, thisPos.y + 5.25f, thisPos.z);
                    Transform basin = _inventory.Items[index].Object.transform;
                    basin.position = newPos;

                    basin.gameObject.SetActive(true);
                    basin.gameObject.GetComponent<CustomTag>().Replace("", "Returnable");

                    _inventory.Items[index].SpriteNotSelected = _inventory.SpriteEmpty;
                    _inventory.Items[index].SpriteSelected = _inventory.SpriteEmpty;

                    _inventory.SetNewItemSprite();
                    _inventory.SelectedItemIndex = -1;

                    Global.GrabTimer = 1.5f;
                }
            }
        }
    }
}
