using UnityEngine;
using UnityEngine.EventSystems;

public class _MagGlass : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static _MagGlass instance;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 oldPos;
    [SerializeField] Canvas canvas;
    [SerializeField] Animator magnifiedViewAnim;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        oldPos = transform.position;
    }

    public void GlassHit(string name)
    {
        magnifiedViewAnim.Play(name);
        if (name == "Glass")
        {
            transform.position = oldPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.localScale = new Vector3(1.5f, 1.5f, .5f);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += (eventData.delta / canvas.scaleFactor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.localScale = Vector3.one;
        canvasGroup.blocksRaycasts = true;
    }
}