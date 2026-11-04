using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    [SerializeField]
    private Transform Target;

    private float angle = 0;
    private Vector2 direction;
    private float sign = 1;
    private float offset = 0;



    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        direction = Target.position - transform.position;

        sign = (Target.position.x >= transform.position.x) ? 1 : -1;

        offset = (sign >= 0) ? 0 : 360;

        angle = Vector2.Angle(Vector2.up, direction) *sign + offset;
        //Debug.Log(angle);
    }

    public void CalculateNextStep()
    {
        OccupiedTile.OccupiedUnit = null;

        if (angle == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, -1);
        }
        else if(angle>0 && angle<90)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y + 1, -1);
        }
        else if (angle == 90)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, -1);
        }
        else if (angle > 90 && angle < 180)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, -1);
        }
        else if (angle == 180)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, -1);
        }
        else if (angle > 180 && angle < 270)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y - 1, -1);
        }
        else if (angle == 270)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y , -1);
        }
        else if (angle > 270 && angle < 359)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y + 1, -1);
        }
        
        OccupiedTile = GridManager.Instance.GetTileAtPosition(new Vector2  (transform.position.x, transform.position.y));
        OccupiedTile.OccupiedUnit = this;
    }
}
