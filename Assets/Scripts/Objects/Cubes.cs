using System.Collections;
using UnityEngine;
using System;

namespace Objects
{
    public class Cubes : MonoBehaviour
    {
        public event Action<GameObject> CubeGrownEvent;
        public event Action CubeClickedEvent;

        private ParticleSystem _effect;
        private BoxCollider _boxCollider;
        private MeshRenderer _meshRenderer;

        private Vector3 _startScale;
        private float _delta;

        private void Start()
        {
            _startScale = gameObject.transform.localScale;
        }

        private void Awake()
        {
            gameObject.transform.localScale = _startScale;
        }

        private void OnMouseDown()
        {
            gameObject.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
            CubeClickedEvent?.Invoke();
        }

        private void Update()
        {
            _delta += Time.deltaTime;
            if (_delta > 1)
            {
                gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

                _delta = 0;
            }
        }

        private void FixedUpdate()
        {
            if (gameObject.transform.localScale.x > 3)
            {
                StartCoroutine(CubeDestroyCoroutine());
            }
        }

        private IEnumerator CubeDestroyCoroutine()
        {
            _boxCollider.enabled = false;
            _meshRenderer.enabled = false;
            _effect.Play();
            Debug.Log(_effect.main.startLifetime.constantMin);
            yield return new WaitForSeconds(_effect.main.startLifetime.constantMin);
            _boxCollider.enabled = true;
            _meshRenderer.enabled = true;
            gameObject.transform.localScale = _startScale;
            CubeGrownEvent?.Invoke(gameObject);
        }
    }
}