using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{

    public string TileName;

    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] public GameObject _onRange;
    [SerializeField] public GameObject _on_NME_Range;
    [SerializeField] public bool _isWalkable; //needs range logic
    [SerializeField] public bool _NME_isWalkable;
    [SerializeField] public bool _isSpawnable;
    //have is walkable and is spawnable variant

    public BaseUnit OccupiedUnit;
    public GameObject MovedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null; 

    public virtual void Init(int x, int y) 
    {

    }
    void OnMouseEnter()
    {
        if (MenuManager.Instance.GetComponent<PauseGame>().GameIsPaused == false)
        {
            _highlight.SetActive(true);
           // MenuManager.Instance.ShowTileInfo(this);
        }
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
      //  MenuManager.Instance.ShowTileInfo(null);
    }

    //falta onmouse down events, moure, diparar etc...  
    private void OnMouseDown()
    {
        if (MenuManager.Instance.GetComponent<PauseGame>().GameIsPaused == false)
        {
            //es el player turn
            if (GameManager.Instance.GameState != GameState.HeroesTurn) return;

            //HealthManager.Instance.GetComponent<HealthManager>().NME_UI.SetActive(false);

            //GridManager.Instance.UNShowwalkableTiles();
            //GridManager.Instance.UNShow_NME_walkableTiles();
            
            
            //si hay algo en la casilla
            if (OccupiedUnit != null)
            {   
               
                //si es un heroe lo que hemos seleccionado 
                if (OccupiedUnit.Faction == Faction.Hero)
                {
                    //lo assignamos a SelectedHero
                    UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                    GridManager.Instance.ShowwalkableTiles();
                }
                //si es un enemigo
                else
                {
                    //show UI
                    HealthManager.Instance.GetComponent<HealthManager>().NME_UI.SetActive(true);
                    GridManager.Instance.NME_ShowwalkableTiles();
                  


                    //si ya habiamos seleccionado un heroe 
                    if (UnitManager.Instance.SelectedHero != null)
                    {
                        var enemy = (BaseEnemy)OccupiedUnit;
                        if(enemy.OccupiedTile.GetComponent<Tile>()._isWalkable)
                        {
                        //aqui va la logica de ensenyar el rango y las acciones que puede hacer sobre el enemigo (ejemplo)
                        //UnitManager.Instance.SelectedHero.Attack;

                        //bajar vida
                        if(PlayerPrefs.GetInt("P_A_") > 0)
                        {
                        int hp;
                        hp = PlayerPrefs.GetInt("Enemy_health_");
                        hp--;
                        PlayerPrefs.SetInt("Enemy_health_", hp);

                        

                        if(hp == 0)
                        {
                            Destroy(enemy.gameObject);
                        }

                        HealthManager.Instance.RestarPuntos();
                        }

                        //deseleccionar el hero
                        //UnitManager.Instance.SetSelectedHero(null);
                        }
                    }
                }
            }
            //si no hay nada en la casilla
            else
            {
                //i ya habiamos seleccionado un hero
                if (UnitManager.Instance.SelectedHero != null)
                {
                    if (HealthManager.Instance.GetComponent<HealthManager>()._puntos > 0)
                    {
                        if (Walkable) 
                        {
                            HealthManager.Instance.RestarPuntos();
                            //move to selected 
                            SetUnit(UnitManager.Instance.SelectedHero);

                            GridManager.Instance.UNShowwalkableTiles();
                            GridManager.Instance.WalkableTiles();
                            
                            GridManager.Instance.ShowwalkableTiles();

                            //deseleccionar el hero
                            //UnitManager.Instance.SetSelectedHero(null);
                        }
                        /*else
                        {
                             //deseleccionar el hero
                        UnitManager.Instance.SetSelectedHero(null);
                        }
                        */
                    }
                    /*
                    else
                    {
                        //deseleccionar el hero
                        UnitManager.Instance.SetSelectedHero(null);
                    }
                        */
                }
            }

        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;

        //place Hero
             //OccupiedUnit = unit;
             //StartCoroutine(Move());
        unit.transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    /*
    IEnumerator Move()
    {
        //Get faction and get tag
        string faction = OccupiedUnit.Faction.ToString();
        MovedUnit = GameObject.FindGameObjectWithTag(faction);
        MovedUnit.transform.right = transform.position - MovedUnit.transform.position;

        while(MovedUnit.transform.position != transform.position)
        {
        MovedUnit.transform.Translate(MovedUnit.transform.right *2 *Time.deltaTime);
        }
        yield return null;
    }
    */
    
    

}
