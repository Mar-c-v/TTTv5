using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedHeroObject; //_tileObject, _tileUnitObject ;
    public GameObject RangeButton;
    public bool RangeIsFull;
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {   
        //RangeButton.SetActive(false);
        RangeButton.SetActive(true);
         RangeIsFull = false;

        /*  _tileObject.SetActive(false);
          _tileUnitObject.SetActive(false);
        */
    }
   
    public void Update()
    {
         //EL RANGE BUTTON SOLO APARECE SI TENEMOS EL HERO SELECCIONADO
        if(_selectedHeroObject == null)
        {
            _selectedHeroObject = GameObject.FindGameObjectWithTag("Player_highlight");
            _selectedHeroObject.SetActive(false);
        }
        
        if(PlayerPrefs.GetInt("Rango") == 16 )
        {
            RangeIsFull = true;
        }
        if(RangeIsFull)
        {
            RangeButton.SetActive(false);
        }
    }

/*
    public void ShowTileInfo(Tile tile)
    {
        if (tile == null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        _tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileObject.SetActive(true);

        if(tile.OccupiedUnit)
        {
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _tileUnitObject.SetActive(true);

        }
    }
*/
    
    public void ShowSelectedHero(BaseHero hero)
    {
        if(hero == null)
        {
        _selectedHeroObject.SetActive(false);
           //RangeButton.SetActive(false);
            return;
        }
        else
        {
        _selectedHeroObject.SetActive(true);
            //RangeButton.SetActive(true);
        }
    }
    
   
}
