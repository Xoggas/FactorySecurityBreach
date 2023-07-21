using UnityEngine;

namespace MelonJam4.Factory
{
    public sealed class Player : Robot
    {
        [Header("Player Properties")]
        [SerializeField]
        private float _movementSpeed = 5f;

        [SerializeField]
        private float _rotationSpeed = 5f;

        [SerializeField]
        private float _timeLimit = 2f;

        private float _compromisedTimer;
        private bool _isInStealthMode;
        private bool _isBeingCompromised;

        private void Update()
        {
            Transform();
            UpdateCompromisedTimer();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent<Policeman>(out _))
            {
                _isBeingCompromised = true;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.TryGetComponent<Policeman>(out _))
            {
                _isBeingCompromised = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent<Policeman>(out _))
            {
                _isBeingCompromised = false;
            }
        }

        private Vector3 GetInputVector()
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            return matrix.MultiplyPoint(input);
        }

        private void Transform()
        {
            var input = GetInputVector();

            if (input.magnitude <= 0.00001f)
            {
                return;
            }

            transform.localPosition += input * Time.deltaTime * _movementSpeed;

            var rotation = Quaternion.LookRotation(input, Vector3.up);

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotation, Time.deltaTime * _rotationSpeed);
        }

        private void UpdateCompromisedTimer()
        {
            if (_isInStealthMode)
            {
                return;
            }

            if (_isBeingCompromised)
            {
                _compromisedTimer += Time.deltaTime;
            }
            else if (_compromisedTimer > 0f)
            {
                _compromisedTimer -= Time.deltaTime;
            }
            else
            {
                _compromisedTimer = 0f;
            }

            if (_compromisedTimer >= _timeLimit)
            {
                
            }
        }
    }
}
