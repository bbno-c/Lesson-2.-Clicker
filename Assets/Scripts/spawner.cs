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
    public static float delta;
    public float show_del;
    public float speed;
    public int temp;
    public bool flag;

    void Start()
    {
        speed = 3;
    }

    private void Update()
    {
        delta += Time.deltaTime;
        show_del = delta;
        if (delta > speed)
        {
            Vector3 screen_point = Camera.main.ScreenToWorldPoint(
                new Vector2(Random.Range(3f, Camera.main.pixelWidth-3), Random.Range(3f, Camera.main.pixelHeight-3)));
            Instantiate(Cube, screen_point + new Vector3(0, 0, 8), Quaternion.identity);
            delta = 0;

            if (temp != Destroyed / 10)
            {
                temp = Destroyed / 10;
                flag = true;
            }
            if (flag)
            {
                speed -= 0.05f;
                flag = false;
            }                

            amount = GameObject.FindGameObjectsWithTag("Cube_entity");
            if (amount.Length > 10)
            {
                foreach (GameObject cube_entity in amount)
                {
                    GameObject.Destroy(cube_entity);
                }
                Destroyed = 0;
                speed = 3;
            }
        }

        DisplayScore.GetComponent<Text>().text = "Score: " + Destroyed;
    }
}
