using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public GameObject Player;
    public int health = 3;

    public Image[] hearts;

    public GameObject Enemy;
    public int Enemyhealth = 3;

    public int EnemyRango;
    public int EnemyPuntos;

    public GameObject heartContainer;
    public GameObject[] Enemyhearts;

    public GameObject NME_UI;
    public GameObject NME_PA;
    public GameObject NME_Rango;

    


    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Text Rango;
    public Text P_A_;

    public int _puntos;
    public int _rango;

    public GameObject ResumenDelDia;
    public Text ResumenDelDiaText;

    public bool Disparo;
    public bool moved;
    public bool Augmented;
    public int op;
    public bool PlayerTurn;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerPrefs.SetInt("Player_health", 3);
        PlayerPrefs.SetInt("P_A_", 1);
        //SumarPuntos();
        //Debug.Log(PlayerPrefs.GetInt("P_A_"));
        PlayerPrefs.SetInt("Rango", 2);
        
        Enemy = GameObject.FindGameObjectWithTag("Enemy");

        PlayerPrefs.SetInt("Enemy_health_", 3);
        PlayerPrefs.SetInt("NME_P_A_", 0);
        PlayerPrefs.SetInt("NME_Rango", 2);

        FindNMY_UI();
        //NME_UI.SetActive(false);

        ResumenDelDia.SetActive(false);
        ResumenDelDiaText.text = null;
        Disparo = false;
        moved = false;
        Augmented = false;
        
    }


    public void FindNMY_UI()
    {
        NME_UI =  Enemy.transform.GetChild(0).gameObject;

        heartContainer = NME_UI.transform.GetChild(0).gameObject;

        for (int i = 0; i <= 2; i++)
        {
            Enemyhearts[i] = heartContainer.transform.GetChild(i).gameObject;
        }

        NME_PA = NME_UI.transform.GetChild(1).transform.GetChild(0).gameObject; 
        NME_Rango = NME_UI.transform.GetChild(1).transform.GetChild(1).gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        Enemyhealth = PlayerPrefs.GetInt("Enemy_health_");
        health = PlayerPrefs.GetInt("Player_health");

        //Vida jugador
        foreach (Image img in hearts)
        {
            img.sprite =emptyHeart;
        }
        for (int i = 0; i<health; i++)
        {
            hearts[i].sprite = fullHeart;
        }


        //vida enemigo
        foreach (GameObject heart in Enemyhearts)
        {
            heart.GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        for (int i = 0; i < Enemyhealth; i++)
        {
            Enemyhearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
        }


        _rango = PlayerPrefs.GetInt("Rango");
        _puntos = PlayerPrefs.GetInt("P_A_");

        Rango.text = "Rango:" + _rango.ToString();
        P_A_.text = _puntos.ToString();

        EnemyPuntos = PlayerPrefs.GetInt("NME_P_A_");
        EnemyRango = PlayerPrefs.GetInt("NME_Rango");

        NME_PA.GetComponent<Text>().text = EnemyPuntos.ToString();
        NME_Rango.GetComponent<Text>().text = "Rango: " +EnemyRango.ToString();
       Debug.Log(EnemyPuntos);
    }


    public void SumarPuntos()
    {
        _puntos++;
        PlayerPrefs.SetInt("P_A_", _puntos);
    }

    public void RestarPuntos()
    {   
        if (_puntos > 0)
        {
            _puntos--;
            PlayerPrefs.SetInt("P_A_", _puntos);
        }
    }


    public void AugmentarRango()
    {
        if (_puntos > 0)
        {
            RestarPuntos();
            //_rango = PlayerPrefs.GetInt("Rango");
            _rango++;
            PlayerPrefs.SetInt("Rango", _rango);
            UnitManager.Instance.SetSelectedHero(Player.GetComponent<Hero1>());
            GridManager.Instance.WalkableTiles();
            GridManager.Instance.ShowwalkableTiles();
             //deseleccionar el hero
            UnitManager.Instance.SetSelectedHero(null);
        }
    }

    public void Sumar_NME_Puntos()
    {
        EnemyPuntos++;
        PlayerPrefs.SetInt("NME_P_A_", EnemyPuntos);
    }

    public void Restar_NME_Puntos()
    {   
        if (EnemyPuntos > 0)
        {
            EnemyPuntos--;
            PlayerPrefs.SetInt("NME_P_A_", EnemyPuntos);
        }
    }

       public void StartHeroTurn()
    {
        PlayerTurn = true;
         Debug.Log("start");
    }

     public void EndHeroTurn()
    {
        PlayerTurn = false;
        SumarPuntos();
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }

    public void StartEnemyTurn()
    {
        PlayerTurn = false;

        ResumenDelDia.SetActive(false);
        ResumenDelDiaText.text = null;
        Disparo = false;
        moved = false;
        Augmented = false;
        //GridManager.Instance.UNShow_NME_walkableTiles();
        Sumar_NME_Puntos();
        EnemyLogic();
    }


    public void EnemyLogic()
    {
         //mientras tengas puntos
        while(PlayerPrefs.GetInt("NME_P_A_") != 0 && !PlayerTurn)
        {
        //si el jugador esta en la area de el NME
            if(Player.GetComponent<Hero1>().OccupiedTile._NME_isWalkable)
            {
                //dispara
                Disparo = true;

                health = PlayerPrefs.GetInt("Player_health");
                health--;
                PlayerPrefs.SetInt("Player_health", health);
                Restar_NME_Puntos(); 
            }
            
            //si el jugador no esta en la area del NME
            else
            {
                //probabilidad de 1 entre 3
               op = UnityEngine.Random.Range(0, 3);
               if(op == 0)
               {
                ///move
                moved = true;

                Enemy.GetComponent<Enemy1>().CalculateNextStep();
                    GridManager.Instance.UNShow_NME_walkableTiles();
                    GridManager.Instance.EnemyWalkableTiles();
                    GridManager.Instance.NME_ShowwalkableTiles();
                    Restar_NME_Puntos();
               }
               else if( op == 1 && PlayerPrefs.GetInt("NME_Rango")<16)
               { 
                //+RANGO
                Augmented = true;

                EnemyRango = PlayerPrefs.GetInt("NME_Rango");
                EnemyRango++;
                PlayerPrefs.SetInt("NME_Rango",EnemyRango);
                    GridManager.Instance.UNShow_NME_walkableTiles();
                    GridManager.Instance.EnemyWalkableTiles();
                    GridManager.Instance.NME_ShowwalkableTiles();
                    Restar_NME_Puntos();
               }
               else if(op == 2)
               {
                PlayerTurn = true;
                //terminar turno
                //EndEnemyTurn();
               }
            }
            
        }
        PlayerTurn = true;
        //cuando no tengas puntos termina el turno
        EndEnemyTurn();
    }

    public void EndEnemyTurn()
    {
        PlayerTurn = true;

        if(Disparo)
        {
            int dmg = 3 - health;
            ResumenDelDiaText.text += " Disparado " +dmg + " veces";
        }
        if(moved)
        {
            ResumenDelDiaText.text += " Movido a " + Enemy.GetComponent<Enemy1>().OccupiedTile;
        }
        if(Augmented)
        {
            ResumenDelDiaText.text += " Rango augmentado a " +EnemyRango; 
        }

        ResumenDelDia.SetActive(true);
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

}
