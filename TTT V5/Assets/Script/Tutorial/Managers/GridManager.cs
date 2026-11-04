using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _grassTile, _mountainTile;
    [SerializeField] private Transform _cam;

    public Tile[] tilecount;
    private int i;
    protected Dictionary<Vector2, Tile> _tiles;
    
    public void Start()
    {
        i = 0;

    }
   


    void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //logica spawn tormenta
                //var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;

                var spawnedTile = Instantiate(_grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

               
                spawnedTile.Init(x,y);

                _tiles[new Vector2(x, y)] = spawnedTile;

            }
        }

        CountTiles();

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);

    }

    public void CountTiles()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                string name;
                name = $"Tile {x} {y}";
                GameObject countedTile;
                countedTile = GameObject.Find(name);
                tilecount[i] = countedTile.GetComponent<Tile>();
                i++;

            }
        }
    }
 
    public void WalkableTiles()
    {
        //LOGICA RANGO
        //para cada tile generada y guardada en Gridmanager
        foreach (Tile tile in tilecount)
        {
            tile._isWalkable = false;
            for (int i = 0; i <= PlayerPrefs.GetInt("Rango"); i++)
            {
                if (tile.transform.position.x == UnitManager.Instance.SelectedHero.OccupiedTile.transform.position.x || tile.transform.position.x == UnitManager.Instance.SelectedHero.OccupiedTile.transform.position.x + i || tile.transform.position.x == UnitManager.Instance.SelectedHero.OccupiedTile.transform.position.x - i)
                {
                    for (int j = 0; j <= PlayerPrefs.GetInt("Rango"); j++)
                    {
                        if (tile.transform.position.y == UnitManager.Instance.SelectedHero.OccupiedTile.transform.position.y || tile.transform.position.y == UnitManager.Instance.SelectedHero.OccupiedTile.transform.position.y + j || tile.transform.position.y == UnitManager.Instance.SelectedHero.OccupiedTile.transform.position.y - j)
                        {
                            tile._isWalkable = true;
                        }
                    }
                }
            }
        }

        Debug.Log("Walkable Done");
    }

    public void ShowwalkableTiles()
    {
        foreach (Tile tile in tilecount)
        {
            if(tile.GetComponent<Tile>()._isWalkable)
            {
                tile.GetComponent<Tile>()._onRange.SetActive(true);
            }
        }
        Debug.Log("Show Walkable Done");
    }
     public void UNShowwalkableTiles()
    {
        foreach (Tile tile in tilecount)
        {
            if(tile.GetComponent<Tile>()._isWalkable)
            {
                tile.GetComponent<Tile>()._onRange.SetActive(false);
            }
        }
    }

    public void EnemyWalkableTiles()
    {
        Debug.Log(PlayerPrefs.GetInt("NME_Rango"));
        //LOGICA RANGO
        //para cada tile generada y guardada en Gridmanager
        foreach (Tile tile in tilecount)
        {
            tile._NME_isWalkable = false;
            for (int i = 0; i <= PlayerPrefs.GetInt("NME_Rango"); i++)
            {
                if (tile.transform.position.x == UnitManager.Instance.SelectedEnemy.OccupiedTile.transform.position.x || tile.transform.position.x == UnitManager.Instance.SelectedEnemy.OccupiedTile.transform.position.x + i || tile.transform.position.x == UnitManager.Instance.SelectedEnemy.OccupiedTile.transform.position.x - i)
                {
                    for (int j = 0; j <= PlayerPrefs.GetInt("NME_Rango"); j++)
                    {
                        if (tile.transform.position.y == UnitManager.Instance.SelectedEnemy.OccupiedTile.transform.position.y || tile.transform.position.y == UnitManager.Instance.SelectedEnemy.OccupiedTile.transform.position.y + j || tile.transform.position.y == UnitManager.Instance.SelectedEnemy.OccupiedTile.transform.position.y - j)
                        {
                            tile._NME_isWalkable = true;
                        }
                    }
                }
            }
        }
        Debug.Log("Walkable ENEMY done");
    }

    public void NME_ShowwalkableTiles()
    {
        foreach (Tile tile in tilecount)
        {
            if(tile.GetComponent<Tile>()._NME_isWalkable)
            {
                tile.GetComponent<Tile>()._on_NME_Range.SetActive(true);
            }
        }
    }

    public void UNShow_NME_walkableTiles()
    {
        foreach (Tile tile in tilecount)
        {
            if(tile.GetComponent<Tile>()._NME_isWalkable)
            {
                tile.GetComponent<Tile>()._on_NME_Range.SetActive(false);
            }
        }
    }

    public Tile GetHeroSpawnTile()
    {
        //agafem totes les tiles de la mitat dreta del tauler, les mesclem i seleccionem nomes la primera, t es ara el valor de la tile
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value._isSpawnable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile() //falta logica de rango
    {
        //agafem totes les tiles de la mitat dreta del tauler, les mesclem i seleccionem nomes la primera, t es ara el valor de la tile
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value._isSpawnable && t.Value._isWalkable == false ).OrderBy(t => Random.value).First().Value;
    }

    //function to grab the tile position
    public Tile GetTileAtPosition(Vector2 pos)
    {
        //if the tile is available
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
