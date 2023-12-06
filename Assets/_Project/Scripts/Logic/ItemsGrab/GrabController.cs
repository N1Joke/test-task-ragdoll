using System;
using TMPro;
using UnityEngine;

public class GrabController : IDisposable
{
    public struct Ctx
    {
        public CameraController cameraController;
        public Camera mainCamera;
    }

    private readonly Ctx _ctx;
    private bool _canPickUpObject = false;
    private Rigidbody _currentPickedItem;
    private float _raycastRadius = 0.05f;
    private bool _canUpdateTouch;
    private float _hypotenuse;
    private float _touchYCoordinate;
    private int _rayCastLayerMaskGrab = 1 << 9 | 1 << 6;
    private int _rayCastLayerMaskMoveObject = 1 << 9 | 1 << 6 | 1 << 7;
    private int _nonRayCastLayer = 10;
    private int _tempObjectLayer;
    private float _maxRrayCastMoveObjectDistance = 40f;

    public GrabController(Ctx ctx)
    {
        _ctx = ctx;

        _ctx.cameraController.OnCameraModChanged += OnCameraModChanged;
    }

    public void Dispose()
    {
        _ctx.cameraController.OnCameraModChanged -= OnCameraModChanged;
    }

    private void OnCameraModChanged(CameraMod cameraMod)
    {
        switch (cameraMod)
        {
            case CameraMod.FreeLook:
                {
                    _canPickUpObject = true;
                    break;
                }
            case CameraMod.ThirdPerson:
                {
                    _canPickUpObject = false;
                    OnPlayerRelease();
                    break;
                }
        }
    }

    private void OnPlayerClick()
    {
        if (!_canPickUpObject)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _raycastRadius, out hit, Mathf.Infinity, _rayCastLayerMaskGrab))
        {
            if (hit.rigidbody != null)
            {
                _currentPickedItem = hit.rigidbody;
                _currentPickedItem.isKinematic = true;
                _canUpdateTouch = true;

                Vector3 direction = _ctx.mainCamera.ScreenToViewportPoint(Input.mousePosition);
                _touchYCoordinate = direction.y;
                _tempObjectLayer = _currentPickedItem.gameObject.layer;
                _currentPickedItem.gameObject.layer = _nonRayCastLayer;
            }
        }
    }

    private void OnPlayerRelease()
    {
        _canUpdateTouch = false;

        if (_currentPickedItem == null)
            return;

        _currentPickedItem.isKinematic = false;
        _currentPickedItem.gameObject.layer = _tempObjectLayer;
        _currentPickedItem = null;        
    }

    private void UpdatePos(Vector2 pos)
    {
        if (!_canUpdateTouch || _currentPickedItem == null || !_canPickUpObject)
            return;

        //Vector3 direction = _ctx.mainCamera.ScreenToViewportPoint(pos);
        //_hypotenuse = Mathf.Sqrt(Mathf.Pow((_ctx.mainCamera.transform.position.y - _currentPickedItem.transform.position.y), 2) + Mathf.Pow((_currentPickedItem.transform.position.z - _ctx.mainCamera.transform.position.z), 2));
        //direction.z = _hypotenuse;
        //float inputY = direction.y - _touchYCoordinate;
        //direction = _ctx.mainCamera.ViewportToWorldPoint(direction);
        //direction.y = Mathf.Lerp(direction.y, _ctx.mainCamera.transform.position.y, inputY);
        //direction.y = Mathf.Clamp(direction.y, 0.5f, _ctx.mainCamera.transform.position.y);
        //direction.z = _currentPickedItem.transform.position.z;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxRrayCastMoveObjectDistance, _rayCastLayerMaskMoveObject))
        {
            Vector3 targetPosition = hit.point;
            _currentPickedItem.transform.position = targetPosition + Vector3.up;
        }
    }

    public void Update(float deltaTime)
    {
        if (!_canPickUpObject)
            return;

        if (Input.GetMouseButtonDown(0))
            OnPlayerClick();
        else if (Input.GetMouseButton(0))
            UpdatePos(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0))
            OnPlayerRelease();
    }
}
