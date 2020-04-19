using System.Collections;
using UnityEngine;
using System;

namespace Objects
{
    public class Cubes : MonoBehaviour
    {
        public event Action<GameObject> CubeGrownEvent;
        public event Action CubeClickedEvent;

        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private Vector3 _startScale;
        private float _delta;
        private bool cubeGrown;

        private void Start()
        {
            _startScale = gameObject.transform.localScale;
            cubeGrown = false;
        }

        private void OnDisable()
        {
            _boxCollider.enabled = true;
            _meshRenderer.enabled = true;
            gameObject.transform.localScale = _startScale;
            cubeGrown = false;
            CubeGrownEvent?.Invoke(gameObject);
        }

        private void OnMouseDown()
        {
            gameObject.transform.localScale += new Vector3(0.8f, 0.8f, 0.8f);
            CubeClickedEvent?.Invoke();
        }

        private void Update()
        {
            _delta += Time.deltaTime;
            
        }

        private void FixedUpdate()
        {
            if (!cubeGrown)
            {
                if (_delta > 1)
                {
                    gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

                    _delta = 0;
                }
                if (gameObject.transform.localScale.x > 3)
                {
                    cubeGrown = true;
                    StartCoroutine(CubeDestroyCoroutine());
                }
            }
        }

        private IEnumerator CubeDestroyCoroutine()
        {
            _boxCollider.enabled = false;
            _meshRenderer.enabled = false;
            _effect.Play();
            Debug.Log(_effect.main.startLifetime.constantMin);
            yield return new WaitForSeconds(_effect.main.startLifetime.constantMin);
            
            gameObject.SetActive(false);
        }
    }
}