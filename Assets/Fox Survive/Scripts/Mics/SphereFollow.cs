using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SphereFollow : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private StandartControlls _controls;

    private float _distance;

    //Input
    private Vector2 _positionDelta;
    private float _distanceDelta;

    [Inject]
    public void Inject(StandartControlls controlls)
    {
        _controls = controlls;
    }

    private void Awake()
    {
        AddDistanse(_settings.DistanceMax);
    }

    private void Update()
    {
        _positionDelta += _controls.Gameplay.CameraPositionDelta.ReadValue<Vector2>();
        _distanceDelta += _controls.Gameplay.CameraDistanceDelta.ReadValue<float>();
        Debug.Log(_distanceDelta);
    }

    private void FixedUpdate()
    {
        AddDistanse(_distanceDelta * _settings.DistanceSensitivity);

        _positionDelta = Vector2.zero;
        _distanceDelta = 0;
    }

    private void LateUpdate()
    {

    }

    private void AddDistanse(float value)
    {
        if(_settings.InverseDistance) value = -value;

        _distance += value;

        if(_distance > _settings.DistanceMax) _distance = _settings.DistanceMax;
        
        if(_distance < _settings.DistanceMin) _distance = _settings.DistanceMin;
    }

    [Serializable]
    public class Settings
    {
        public Transform LookAt;

        public float Sensitivity;
        public float DistanceSensitivity;

        public bool InverseX;
        public bool InverseY;
        public bool InverseDistance;

        [Header("Camera follow speed per second in units")]
        public float Speed;

        [Header("Y limit angle")]
        [Range(0, 180)]
        public float YMinAngle;
        [Range(0, 180)]
        public float YMaxAngle;

        [Header("Distance")]
        public float DistanceMin;
        public float DistanceMax;
    }
}
