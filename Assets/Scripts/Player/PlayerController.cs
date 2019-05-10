using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Events

        public static Action<float> UpdateEnergyEvent = delegate(float currentEnergy) { };
        public static Action PlayerCanMove = delegate { };

        #endregion

        #region public var

        public Sprite jumpSprite;
        public float maxSpeed = 10f;
        public float energyToJump = 10f;
        public float speed = 10f;

        #endregion

        #region private var

        private float _energy = 100f;
        private bool _canMove;
        private bool _canJump;
        private Rigidbody2D _rigidbody2D;
        private Sprite _defaultSprite;
        private SpriteRenderer _spriteRenderer;

        #endregion

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _defaultSprite = _spriteRenderer.sprite;

            _canMove = false;
            _canJump = true;
        }

        private void Start()
        {
            UpdateEnergy(100);
        }

        private void Update()
        {
            if (_rigidbody2D.velocity.magnitude > maxSpeed)
            {
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * maxSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Space) && _energy >= energyToJump && _canJump &&
                !GameManager.Instance.isTutorialEnabled)
            {
                _canJump = false;
                UpdateEnergy(_energy - energyToJump);
                _rigidbody2D.velocity = Vector2.zero;
                _spriteRenderer.sprite = jumpSprite;

                StartCoroutine(ChangeSprite());
            }
        }

        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(.5f);
            _spriteRenderer.sprite = _defaultSprite;
            _canJump = true;
        }

        private void FixedUpdate()
        {
            if (_canMove)
            {
                Move();
            }
        }

        private void Move()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(moveHorizontal, 0);

            _rigidbody2D.AddForce(movement * speed);
        }

        private void UpdateEnergy(float energy)
        {
            _energy = energy;

            UpdateEnergyEvent(_energy);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Helper.TAG_ENTRANCE))
            {
                _canMove = true;

                PlayerCanMove();
            }
        }
    }
}