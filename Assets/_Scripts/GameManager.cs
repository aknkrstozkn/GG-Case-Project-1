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
    
    [Header("Other")]
    [SerializeField] const int TravelCount = 9;
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

    void SetCurrentSkinBox(SkinBox skinBox)
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
        else if(inactiveSkinBoxes.Count == 0)
        {
            // End
            enabled = false;
        }        
    }

    public void ChooseSkinbox()
    {
        _elapsedTime += Time.deltaTime;

        var isFinal = _traveledCount == TravelCount || inactiveSkinBoxes.Count == 0;
        var inTime = _elapsedTime >= _traveledCount / 10f;

        if (!isFinal && inTime) 
        {
            // Reset time
            _elapsedTime = 0f;

            ++_traveledCount;
            SelectSkinBox();
        } 
        else if(isFinal && inTime)
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

    void SelectSkinBox()
    {       
        var index = Random.Range(0, inactiveSkinBoxes.Count);
        if(_currentSkinBox != null)
        {
            inactiveSkinBoxes.Add(_currentSkinBox);
        }
        
        SetCurrentSkinBox(inactiveSkinBoxes[index]);
        inactiveSkinBoxes.RemoveAt(index);       
    }
}
