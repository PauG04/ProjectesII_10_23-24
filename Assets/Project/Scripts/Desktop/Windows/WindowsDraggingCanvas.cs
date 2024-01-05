using UnityEngine;
using UnityEngine.EventSystems;

public class WindowsDraggingCanvas : MonoBehaviour, IDragHandler, IBeginDragHandler
{
	private Canvas _canvas;
	private RectTransform _rectTransform;
	private ListOfWindows _listOfWindows;
	private Vector3 _dragOffset;

    // Start is called before the first frame update
    void Start()
    {
	    _canvas = gameObject.GetComponent<Canvas>();
	    _rectTransform = gameObject.GetComponent<RectTransform>();
	    _listOfWindows = gameObject.transform.parent.GetComponent<ListOfWindows>();
    }
    
	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (_rectTransform != null)
		{
			Vector3 globalMousePos;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position, Camera.main, out globalMousePos))
			{
				_dragOffset = _rectTransform.position - globalMousePos;
			}
		}
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		_listOfWindows.MoveObjectInFront(gameObject);
		
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position, Camera.main, out globalMousePos))
		{
			_rectTransform.position = globalMousePos + _dragOffset;
		}
	}
}
