using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class longClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public float reloadTime = 0.5f;
	private float timer = 0;
	private bool pointerDown = false;

	public UnityEvent onLongClick;

	public void OnPointerDown(PointerEventData eventData)
	{
		pointerDown = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pointerDown = false;
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (pointerDown && timer > reloadTime)
		{
			timer = 0;
			if (onLongClick != null)
				onLongClick.Invoke();
		}
	}
}