using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> _units;
    public BaseHero SelectedHero;
    public BaseEnemy SelectedEnemy;


    void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeroes()
    {
        Debug.Log("Spawnheroes enter");
        //deber�a leer info de una pantalla de selecci�n de personaje
        var heroCount = 1;

        for(int i = 0; i< heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);

            //enemies spawn on the left, heroes spawn on the right(here goes more precise "spawn units" logic
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            randomSpawnTile.SetUnit(spawnedHero); 
            SelectedHero = spawnedHero;
        }
        PlayerPrefs.SetInt("Rango", 2);
         GridManager.Instance.UNShowwalkableTiles();
        GridManager.Instance.WalkableTiles();
        GridManager.Instance.ShowwalkableTiles();
        SelectedHero = null;

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        //deber�a leer info de una pantalla de selecci�n de personaje
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            //enemies spawn on the left, heroes spawn on the right(here goes more precise "spawn units" logic
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
            SelectedEnemy = spawnedEnemy;
        }
        PlayerPrefs.SetInt("NME_Rango", 2);
        GridManager.Instance.UNShow_NME_walkableTiles();
        GridManager.Instance.EnemyWalkableTiles();
        GridManager.Instance.NME_ShowwalkableTiles();
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        //cogemos todos los miembros de una facci�n, los mezclamos y cogemos el primero de la lista, su prefab ahora es T
        return (T) _units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

     public void SetSelectedEnemy(BaseEnemy enemy)
    {
        SelectedEnemy = enemy;
    }
}

