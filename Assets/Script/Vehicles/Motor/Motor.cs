﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour, IDriverVehicles
{
    [Header("MotorInfo")]
    public PointVehicles pointVehicles;
    public VehiclesData motorData;
    public VehiclesData _vehiclesData { get; set; }
    public Transform _damepoint { get; set; }
    public Rigidbody _rb { get; set; }
    public GameObject _vehicles { get; set; }
    public VehicleSensor _sensor { get; set; }
    public List<Transform> _enterFormPos { get; set; }
    public GameObject _driver { get; set; }
    public Transform _driverSit { get; set; }
    public Transform _exitForce { get; set; }
    public Transform _camtarget { get; set; }
    public float _maxspeed { get; set; }
    public VehicleSensor sensor;
    [Header("Wheels Collider")]
    public WheelCollider frontWheelCollider;
    public WheelCollider backWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontWheelTransform;
    public Transform backWheelTransform;
    public Transform rudder;

    [Header("Wheel Engine")]
    private float presentAcceleration = 0f;

    [Header("Vehicle Steering")]
    private float presentTurnAngle = 0f;
    [Header("Vehicle Tilting")]
    private Vector2 Tilting = Vector2.zero;

    void Awake()
    {
        _maxspeed = motorData.maxspeed;
        _enterFormPos = pointVehicles.enterFormPos;
        _driverSit = pointVehicles.driverSit;
        _camtarget = pointVehicles.camtarget;
        _rb = GetComponent<Rigidbody>();
        _damepoint = pointVehicles.damePoint;
        _sensor = sensor;
        _vehicles = gameObject;
        _vehiclesData = motorData;
    }
    public void DriverVehicles(float acceleration, float vertical, float horizontal, float maxspeed)
    {
        VerticalMove(acceleration, maxspeed);
        HorizontalMove(horizontal);
        TiltingToMotorcycle(0, horizontal);
        _driver.transform.position = _driverSit.position;
        if (Input.GetKey(KeyCode.A))
        {
            HorizontalMove(-1);
            TiltingToMotorcycle(0, -1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            HorizontalMove(1);
            TiltingToMotorcycle(0, 1);
        }
    }
    public void VerticalMove(float acceleration, float speed)
    {
        _rb.centerOfMass = Vector3.zero;
        float forwardSpeed = transform.InverseTransformDirection(_rb.velocity).z;
        if (acceleration != 0)
        {
            ApplyBreaks(0);
            if (acceleration * forwardSpeed < 0 && _rb.velocity.magnitude >= 1)
            {

                presentAcceleration = 0;
                ApplyBreaks(motorData.breakingForceMax);


            }
            else
            {
                if (_rb.velocity.magnitude <= speed)
                {
                    presentAcceleration = acceleration * motorData.accelerationForce;
                }
                else
                {

                    presentAcceleration = 0;
                }


            }

        }
        else
        {
            ApplyBreaks(motorData.breakingForce);

            presentAcceleration = 0;
        }

        //frontWheelCollider.motorTorque = presentAcceleration;
        backWheelCollider.motorTorque = presentAcceleration;

    }

    public void HorizontalMove(float Horizontal)
    {

        presentTurnAngle = motorData.wheelsTorque * Horizontal;
        frontWheelCollider.steerAngle = presentTurnAngle;
        SteeringWheels(frontWheelCollider, frontWheelTransform);
        SteeringWheels(backWheelCollider, backWheelTransform);
        rudder.localRotation = Quaternion.Euler(0, presentTurnAngle, 0);

    }

    public void ApplyBreaks(float breakingForce)
    {
        frontWheelCollider.brakeTorque = breakingForce;
        backWheelCollider.brakeTorque = breakingForce;
    }
    public void TiltingToMotorcycle(float Vertical, float Horizontal)
    {
        float v = _rb.velocity.magnitude / _maxspeed;
        Tilting.y = Mathf.Lerp(Tilting.y, Vertical * motorData.wheelsTorque * v, 5 * Time.deltaTime);
        Tilting.x = Mathf.Lerp(Tilting.x, Horizontal * motorData.wheelsTorque * v, 5 * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Tilting.y, _rb.transform.localEulerAngles.y, -Tilting.x);

    }
    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 pos;
        Quaternion rotation;
        WC.GetWorldPose(out pos, out rotation);
        WT.rotation = rotation;
        WT.position = pos;
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
