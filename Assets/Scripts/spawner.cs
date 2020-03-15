using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _displayScore;
    [SerializeField] private GameObject _instanceObj;
    [SerializeField] private GameObject[] _amount;
    [SerializeField] private int _destroyed;
    [SerializeField] private float _delta;
    [SerializeField] private float _speed;
    private int _temp;
    private bool _flag;

    public int Destroyed
    {
        get => _destroyed;
        set => _destroyed++;
    }

    void Start()
    {
        _speed = 2;
        _destroyed = 0;
    }

    private void Update()
    {
        _delta += Time.deltaTime;
        if (_delta > _speed)
        {
            Vector3 screen_point = Camera.main.ScreenToWorldPoint(
                new Vector2(
                    Random.Range(3f, Camera.main.pixelWidth-3),
                    Random.Range(3f, Camera.main.pixelHeight-3)
                    )
                );

            _instanceObj = Instantiate(_cubePrefab, screen_point + new Vector3(0, 0, 8), Quaternion.identity);
            _instanceObj.transform.parent = gameObject.transform;

            _delta = 0;

            if (_temp != _destroyed / 10)
            {
                _temp = _destroyed / 10;
                _flag = true;
            }
            if (_flag)
            {
                _speed -= 0.05f;
                _flag = false;
            }

            _amount = GameObject.FindGameObjectsWithTag("Cube_entity");
            if (_amount.Length > 10)
            {
                foreach (GameObject cube_entity in _amount)
                {
                    GameObject.Destroy(cube_entity);
                }
                _destroyed = 0;
                _speed = 2;
            }
        }

        _displayScore.GetComponent<Text>().text = "Score: " + _destroyed;
    }
}
