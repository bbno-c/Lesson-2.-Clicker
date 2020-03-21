using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    [SerializeField] private GameProxy GameProxy;
    private float _delta;

    private void OnMouseDown()
    {
        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        GameProxy.AddScore();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        _delta += Time.deltaTime;

        if (_delta > 1)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

            _delta = 0;

            if (gameObject.transform.localScale.x > 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
