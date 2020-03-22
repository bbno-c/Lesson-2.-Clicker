using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    [SerializeField] private GameProxy GameProxy;
    public static System.Action<GameObject> CubeGrown;
    public Vector3 StartScale;
    private float _delta;

    private void Start()
    {
        StartScale = gameObject.transform.localScale;
    }

    private void OnMouseDown()
    {
        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        GameProxy.AddScore();
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
                CubeGrown?.Invoke(gameObject);
            }
        }
    }
}
