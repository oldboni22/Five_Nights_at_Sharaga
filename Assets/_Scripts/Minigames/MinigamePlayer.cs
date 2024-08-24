using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace minigames.jumper
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MinigamePlayer : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jump;

        private float _x;
        private Rigidbody2D _rb;

        [SerializeField] private Collider2D _col;
        [SerializeField] private InputAction _action;

        public void SetSprite(Sprite sprite)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
            GetComponent<SpriteRenderer>().size = Vector2.one * .7f;
        }
        private void OnEnable()
        {
            _action.Enable();
        }
        private void OnDisable()
        {
            _action.Disable();
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            _x = _action.ReadValue<float>();
        }
        private void FixedUpdate()
        {
            _rb.position += Vector2.right * _x * _speed * Time.fixedDeltaTime;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _rb.velocity = Vector2.zero;
            _rb.AddForce(_jump * Vector2.up);
            _col.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _col.enabled = true;
        }
    }
}