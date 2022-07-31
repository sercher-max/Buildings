using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    protected Image _image;
    protected IButtonListener _menu;
    protected int _index;

    public virtual void Initialize(int index, Sprite icon, IButtonListener menu)
    {
        _index = index;
        _menu = menu;

        _image = GetComponent<Image>();
        _image.sprite = icon;

        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _menu.OnButtonClick(_index);
    }
}