using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    [Header("SkinBox")]
    [SerializeField] SkinBox[] skinBoxes = null;
    [SerializeField] Color selectedColor = Color.white;
    [SerializeField] Color defaultColor = Color.white;

    [Header("Sprites")]
    [SerializeField] Sprite coverSprite = null;
    [SerializeField] Sprite[] frameSprites = null;
    
    [SerializeField] const int TravelCount = 9;
    [SerializeField] float StartTravelTime = 0.2f;
    private float _elapsedTime = 0f;
    private int _traveledCount = 0;
    private bool _travel = false;
    private SkinBox _currentSkinBox;
    private List<SkinBox> inactiveSkinBoxes;

    private void Start() 
    {   
        inactiveSkinBoxes = new List<SkinBox>(skinBoxes);
        _currentSkinBox = null;

        SetupSkinBoxes();
    }

    void SetupSkinBoxes()
    {
        foreach (var skinBox in skinBoxes)
        {            
            var frameSprite = GetRandomSprite();
            skinBox.Init(selectedColor, defaultColor, frameSprite, coverSprite);
        }    
    }

    Sprite GetRandomSprite()
    {
        var index = Random.Range(0, frameSprites.Length);
        return frameSprites[index];
    }

    void SelectSkinBox(SkinBox skinBox)
    {
        if(_currentSkinBox != null)
        {
            _currentSkinBox.Deselect();
        }        

        _currentSkinBox = skinBox;

        _currentSkinBox.Select();
    }

    private void Update() 
    {
        if(_travel)
        {
            ChooseSkinbox();
        }

        if(inactiveSkinBoxes.Count == 0 && !_travel)
        {
            enabled = false;
        }        
    }

    public void ChooseSkinbox()
    {
        _elapsedTime += Time.deltaTime;

        var isFinal = _traveledCount == TravelCount || inactiveSkinBoxes.Count == 0;
        var select = _elapsedTime >= _traveledCount / 10f;

        if (!isFinal && select) 
        {
            _elapsedTime = 0f;
            ++_traveledCount;
            SetCurrentSkinBox();
        } 
        else if(isFinal && select)
        {
            _elapsedTime = 0f;
            _currentSkinBox.Activate();
            _currentSkinBox = null;
            _traveledCount = 0;
            _travel = false;
        }                      
    }

    public void StartSelection()
    {
        _travel = true;
        _traveledCount = 0;
    }

    void SetCurrentSkinBox()
    {       
        var index = Random.Range(0, inactiveSkinBoxes.Count);
        if(_currentSkinBox != null)
        {
            inactiveSkinBoxes.Add(_currentSkinBox);
        }        
        SelectSkinBox(inactiveSkinBoxes[index]);

        inactiveSkinBoxes.RemoveAt(index);       
    }
}
