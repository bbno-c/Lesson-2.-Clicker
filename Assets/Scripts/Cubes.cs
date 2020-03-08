using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    [SerializeField] private spawner spawnerScript;
    public float delta;

    private void Start()
    {
        spawnerScript = transform.GetComponentInParent<spawner>();
    }

    private void OnMouseDown()
    {
        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        spawnerScript.Destroyed++;
    }

    private void Update()
    {
        delta += Time.deltaTime;
        if (delta > 1)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            delta = 0;
            if (gameObject.transform.localScale.x > 3)
            {
                Destroy(gameObject);
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube_entity")
        {
            //Debug.Log(collision.gameObject.name);
            //Debug.Log(gameObject.name);
            //gameObject.transform.localScale += new Vector3(1,1,1);
            Destroy(collision.gameObject);
        }
    }*/

}
