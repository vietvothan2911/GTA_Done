using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour, IDriverVehicles
{
    [Header("HelicopterInfo")]
    public GameObject helicopter;
    public PointVehicles pointVehicles;
    public VehicleSensor sensor;
    public VehiclesData heliData;
    public VehiclesData _vehiclesData { get; set; }
    public Transform _damepoint { get; set; }
    public GameObject _vehicles { get; set; }
    public VehicleSensor _sensor { get; set; }
    public Rigidbody _rb { get; set; }
    public List<Transform> _enterFormPos { get; set; }
    public GameObject _driver { get; set; }
    public Transform _driverSit { get; set; }
    public Transform _camtarget { get; set; }
    public Transform _exitForce { get; set; }
    public float _maxspeed { get; set; }
    public Animator animOpenDoor;
    public ShootingHelicopter shootingHelicopter;
    public BladeRotation bladeRotation;
    [Header("Helicopter Engine")]
    public float enginePower;
    public float EnginePower
    {
        get { return enginePower; }
        set
        {
            bladeRotation.BladeSpeed = value * 1000;
            enginePower = value;
        }
    }
    public float effectiveHeight;
    public float engineLift = 0.0075f;
    private Vector2 Movement = Vector2.zero;
    private Vector2 Tilting = Vector2.zero;
    public bool onSurface;
    [SerializeField] Transform surfaceCheck;
    [SerializeField] float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    void Awake()
    {
        _enterFormPos = pointVehicles.enterFormPos;
        _driverSit = pointVehicles.driverSit;
        _camtarget = pointVehicles.camtarget;
        _rb = GetComponent<Rigidbody>();
        _sensor = sensor;
        _vehicles = gameObject;
        _damepoint = pointVehicles.damePoint;
        _vehiclesData = heliData;
    }
    public void DriverVehicles(float acceleration, float vertical, float horizontal, float maxspeed)
    {
        CheckOnGround(vertical, horizontal, acceleration);
        //shootingHelicopter.ShootingBullet(helicopterControl.shootingBullet);
        //shootingHelicopter.ShootingRocket(helicopterControl.shootingRocket);
        HelicopterHover();
        HelicopterMovement();
        HelicopterTilting();
        Player.ins.animator.SetBool("OnGround", onSurface);
    }
    public void CheckOnGround(float vertical, float horizontal, float acceleration)
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        Movement.x = horizontal;
        Movement.y = vertical;
        if (!onSurface)
        {

            if (acceleration < 0)
            {

                EnginePower = Mathf.Clamp(EnginePower - engineLift, -heliData.maxspeed, 10);
                _rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

            }
            else if (acceleration == 0)
            {
                EnginePower = Mathf.MoveTowards(EnginePower, 10f, 1f);
                if (EnginePower - 10 <= 0.5f)
                {
                    _rb.constraints = RigidbodyConstraints.FreezePositionY;

                }

            }
            else
            {
                EnginePower = Mathf.Clamp(EnginePower + engineLift, 10, heliData.maxspeed);
                _rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            }
        }
        else
        {
            if (acceleration > 0)
            {
                EnginePower = Mathf.Clamp(EnginePower + engineLift, 0, heliData.maxspeed);
                //_rb.isKinematic = false;
            }
            else
            {
                EnginePower = Mathf.MoveTowards(EnginePower, 0, 0.1f);
            }
        }





    }
    public void ApplyBreaks(float breakingForce)
    {
    }
    public void HelicopterHover()
    {
        bladeRotation.BladeRotate();
        float upFoce = EnginePower * _rb.mass;
        if (_rb.transform.position.y >= effectiveHeight)
        {
            upFoce = 0;

        }
        _rb.AddRelativeForce(Vector3.up * upFoce);
    }
    public void HelicopterMovement()
    {
        if (Movement.y > 0)
        {
            _rb.AddRelativeForce(Vector3.forward * Mathf.Max(0f, Movement.y * heliData.accelerationForce * _rb.mass));
        }
        else if (Movement.y < 0)
        {
            _rb.AddRelativeForce(Vector3.back * Mathf.Max(0f, -Movement.y * heliData.accelerationForce * _rb.mass));
        }
        if (Movement.x > 0)
        {
            _rb.AddRelativeForce(Vector3.right * Mathf.Max(0f, Movement.x * heliData.accelerationForce * _rb.mass));
        }
        else if (Movement.x < 0)
        {
            _rb.AddRelativeForce(Vector3.left * Mathf.Max(0f, -Movement.x * heliData.accelerationForce * _rb.mass));
        }

        if (!onSurface)
        {
            _rb.transform.rotation = Quaternion.RotateTowards(_rb.transform.rotation, Quaternion.Euler(new Vector3(0f, Camera.main.transform.rotation.eulerAngles.y, 0f)), 100f * Time.deltaTime);
        }
    }
    public void HelicopterTilting()
    {
        Tilting.y = Mathf.Lerp(Tilting.y, Movement.y * heliData.wheelsTorque, Time.deltaTime);
        Tilting.x = Mathf.Lerp(Tilting.x, Movement.x * heliData.wheelsTorque, Time.deltaTime);
        _rb.transform.localRotation = Quaternion.Euler(Tilting.y, _rb.transform.localEulerAngles.y, -Tilting.x);

    }
    public void Fall()
    {
        StartCoroutine(CouroutineFall());
    }
    IEnumerator CouroutineFall()
    {
        while (enginePower > 0)
        {
            EnginePower = Mathf.MoveTowards(EnginePower, 0, 0.1f);
            bladeRotation.BladeRotate();
            _rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            _rb.isKinematic = false;
            yield return null;
        }
        yield break;
    }
    public void Return()
    {
        StartCoroutine(CouroutineReturn());
    }
    IEnumerator CouroutineReturn()
    {
        yield return new WaitForSeconds(5f);
        if (_driver != null)
        {
            yield break;
        }
        if (Vector3.Distance(transform.position, Player.ins.transform.position) > 100)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(CouroutineReturn());
        }
    }
}
