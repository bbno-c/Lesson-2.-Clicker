using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCoordinates : MonoBehaviour
{
    public GameObject Cube;

    private void Update()
    {
        var p = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse_click_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//-Vector3.one
            Instantiate(Cube, mouse_click_position + new Vector3(0,0,8), Quaternion.identity);
        }
    }
}
