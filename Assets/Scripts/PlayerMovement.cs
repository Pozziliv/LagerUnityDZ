using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IEventable
{
    [SerializeField] private float _sensivity = 5f;
    [SerializeField] private float _maxHeadY = 40f;
    [SerializeField] private float _minHeadY = -40f;

    [SerializeField] private KeyCode _runButton = KeyCode.LeftShift;
    [SerializeField] private float _stamina = 7f;
    [SerializeField] private bool _isRun = false;
    [SerializeField] private float _runSpeed = 2.5f;
    [SerializeField] private float _goSpeed = 1.5f;
    private float _speed = 1.5f;

    [SerializeField] private Image _staminaImage;
    private float _staminaMultyPlier;
    private float _staminaTimer = 0f;

    private Vector3 _direction;
    private float _horizontal, _vertical;
    private Rigidbody _rb;
    private float _rotationY;
    private Transform _camera;

    private bool _isEnd = false;

    public delegate void GetCollectable(Collectables collectable);
    public static event GetCollectable? Geting;
    public delegate void OnCaught();
    public static OnCaught? Caught;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _camera = Camera.main.transform;

        _speed = _goSpeed;

        _staminaMultyPlier = 1 / _stamina;
    }

    private void Update()
    {
        if (!_isEnd)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
        }

        _direction = new Vector3(_horizontal, 0, _vertical).normalized;
        _direction = _camera.TransformDirection(_direction);
        _direction = new Vector3(_direction.x, 0, _direction.z);

        if (Input.GetKeyDown(_runButton) && _staminaTimer < _stamina)
        {
            _speed = _runSpeed;
            _isRun = true;
        }
        if (Input.GetKeyUp(_runButton))
        {
            _speed = _goSpeed;
            _isRun = false;
        }

        RunChecker();
    }

    private void RunChecker()
    {
        if (_isRun)
        {
            _staminaTimer += Time.deltaTime;
            _staminaImage.fillAmount = 1 - _staminaMultyPlier * _staminaTimer;
            if (_staminaTimer >= _stamina)
            {
                _speed = _goSpeed;
                _isRun = false;
                _staminaImage.fillAmount = 0;
            }
        }
        else if (_staminaTimer > 0)
        {
            _staminaTimer -= Time.deltaTime;
            _staminaImage.fillAmount = 1 - _staminaMultyPlier * _staminaTimer;
            if (_staminaTimer <= 0)
            {
                _staminaTimer = 0;
                _staminaImage.fillAmount = 1;
            }
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;

        if (!_isEnd)
        {
            float rotationX = _camera.localEulerAngles.y + Input.GetAxis("Mouse X") * _sensivity;
            _rotationY += Input.GetAxis("Mouse Y") * _sensivity;
            _rotationY = Mathf.Clamp(_rotationY, _minHeadY, _maxHeadY);
            _camera.localEulerAngles = new Vector3(-_rotationY, rotationX, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Roman>(out Roman roma))
        {
            Caught.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.TryGetComponent<Collectables>(out Collectables collectables))
        {
            Geting.Invoke(collectables);
        }
    }

    private void OnCollectionGet(Collectables collectable)
    {
        Destroy(collectable.gameObject);
    }

    private void End()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isEnd = true;
    }

    public void OnDisable()
    {
        Geting -= OnCollectionGet;
        Caught -= End;
        VideoCollecting.OnAllVideo -= End;
    }

    public void OnEnable()
    {
        Geting += OnCollectionGet;
        Caught += End;
        VideoCollecting.OnAllVideo += End;
    }
}
