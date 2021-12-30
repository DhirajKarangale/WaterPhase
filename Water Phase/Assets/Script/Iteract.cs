using UnityEngine;
using UnityEngine.EventSystems;

public class Iteract : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag && (eventData.pointerDrag.name == "Magnifying_Glass Image"))
        {
            _MagGlass.instance.GlassHit(transform.name);
        }
    }
}