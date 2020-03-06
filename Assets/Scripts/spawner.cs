using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject Cube;
    public GameObject DisplayScore;
    public GameObject[] amount;
    public static int Destroyed;
    public float speed;
    public float delta;

    void Start()
    {
        speed = 1;
    }

    private void Update()
    {
        delta += Time.deltaTime;
        if (delta > speed / 1)
        {
            Vector3 screen_point = Camera.main.ScreenToWorldPoint(
                new Vector2(Random.Range(3f, Camera.main.pixelWidth-3), Random.Range(3f, Camera.main.pixelHeight-3)));
            Instantiate(Cube, screen_point + new Vector3(0, 0, 8), Quaternion.identity);
            delta = 0;

            if(Destroyed>10)
                speed -= 0.0001f;

            amount = GameObject.FindGameObjectsWithTag("Cube_entity");
            if (amount.Length > 10)
            {
                foreach (GameObject cube_entity in amount)
                {
                    GameObject.Destroy(cube_entity);
                }
                Destroyed = 0;
            }
        }

        DisplayScore.GetComponent<Text>().text = "Score: " + Destroyed;
    }
}
