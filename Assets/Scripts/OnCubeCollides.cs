using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCubeCollides : MonoBehaviour
{
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
        Destroy(gameObject);
        spawner.Destroyed++;
    }
}
