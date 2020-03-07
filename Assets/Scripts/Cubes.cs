using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    public bool flag;
    public int temp;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube_entity")
        {
            //Debug.Log(collision.gameObject.name);
            //Debug.Log(gameObject.name);
            //gameObject.transform.localScale += new Vector3(1,1,1);
            Destroy(collision.gameObject);
        }
    }

    private void OnMouseDown()
    {
        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        spawner.Destroyed++;
    }

    private void Start()
    {
        flag = false;
    }

    private void Update()
    {
        if (temp != (int)spawner.delta)
        {
            temp = (int)spawner.delta;
            flag = true;
        }
        if (flag)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            flag = false;
        }
        if (gameObject.transform.localScale.x > 5)
        {
            Destroy(gameObject);
        }
    }
}
