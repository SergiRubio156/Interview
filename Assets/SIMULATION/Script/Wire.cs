using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool isLeftWire;
    public Color customColor;

    private Image _image;

    private LineRenderer _lineRenderer;

    private  Canvas _canvas;

    private bool _isDragStarted = false;
    private WireTask _wireTask;
    public bool isSucces = false;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _wireTask = GetComponentInParent<WireTask>();
    }

    private void Update()
    {
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera,
                out movePos
                );
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePos));
        }
        else
        {
            if(!isSucces)
            {
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }        
        }

        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, _canvas.worldCamera);
        if (isHovered)
        {
            _wireTask.CurrentHoveredWire = this;
        }
    }

    public void SetColor(Color _color)
    {
        _image.color = _color;
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;
        customColor = _color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Not USE
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isLeftWire) { return; }
        else if (isSucces) { return; }
        _isDragStarted = true;
        _wireTask.CurrentDraggedWire = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_wireTask.CurrentHoveredWire != null)
        {
            if(_wireTask.CurrentHoveredWire.customColor == customColor && !_wireTask.CurrentHoveredWire.isLeftWire)
            {
                isSucces = true;
                _wireTask.CurrentHoveredWire.isSucces = true;
                _wireTask.CheckTaskCompleted();
            }
        }

        _isDragStarted = false;
        _wireTask.CurrentDraggedWire = null;
    }
}
