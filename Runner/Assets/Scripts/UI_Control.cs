using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Control : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public bool gameStart = false;
    public Transform player;
    public float smoothx = 0.03f;
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 temPosition = player.position;
        temPosition.x = Mathf.Clamp(temPosition.x + eventData.delta.x * smoothx, -4.5f, 4.5f);
        player.position = temPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameStart = true;
    }
}
