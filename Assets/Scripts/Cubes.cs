using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    [SerializeField] private Spawner _spawnerScript;
    private float _delta;

    private void Start()
    {
        _spawnerScript = transform.GetComponentInParent<Spawner>();
    }

    private void OnMouseDown()
    {
        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        _spawnerScript.Destroyed++;
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
