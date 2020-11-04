using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinBox : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image frameImage = null;
    [SerializeField] Image coverImage = null;

    private Color _selectedColor = Color.white;
    private Color _defaultColor = Color.white;
    public void Init(Color selectedColor, Color defaultColor, Sprite frameSprite, Sprite coverSprite)
    {    
        _selectedColor = selectedColor;
        _defaultColor = defaultColor;

        Deactivate();

        SetFrameImage(frameSprite);
        SetCoverImage(coverSprite);        
    }

    public void SetFrameImage(Sprite sprite)
    {
        frameImage.sprite = sprite;
    }

    public void SetCoverImage(Sprite sprite)
    {
        coverImage.enabled = sprite;
    }

    public void Activate()
    {
        frameImage.enabled = true;
        coverImage.enabled = false;

        GetComponent<Image>().enabled = false;
    }

    public void Deactivate()
    {
        Deselect();

        frameImage.enabled = false;
        coverImage.enabled = true;

        GetComponent<Image>().enabled = true;
    }

    public void Select()
    {
        GetComponent<Image>().color = _selectedColor;
    }
    
    public void Deselect()
    {
        GetComponent<Image>().color = _defaultColor;
    }
}
