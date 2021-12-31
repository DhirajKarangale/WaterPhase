using UnityEngine;

public class MagGlass : MonoBehaviour
{
    [SerializeField] Collider2D glassPanel, air, water, ice, blockerLeft, blockerRight;
    [SerializeField] Animator animator;
    private bool isPointerDown;
    private Vector3 oldPos;

    private void Start()
    {
        oldPos = transform.position;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            transform.position =
            new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }

        GlassCollide();
    }

    private void GlassCollide()
    {
        if (Physics2D.IsTouchingLayers(blockerLeft) || Physics2D.IsTouchingLayers(blockerRight))
        {
            animator.Play("Not");
        }
        else if (Physics2D.IsTouchingLayers(glassPanel))
        {
            animator.Play("GlassPanel");
            if (!isPointerDown) transform.position = oldPos;
        }
        else if (Physics2D.IsTouchingLayers(water))
        {
            animator.Play("Water");
        }
        else if (Physics2D.IsTouchingLayers(ice))
        {
            animator.Play("Ice");
        }
        else if (Physics2D.IsTouchingLayers(air))
        {
            animator.Play("Air");
        }
        else
        {
            animator.Play("Not");
        }
    }

    public void OnPointerUp()
    {
        isPointerDown = false;
        transform.localScale = new Vector3(0.05f, 0.05f, 1);
    }

    public void OnPointerDown()
    {
        transform.localScale = new Vector3(0.065f, 0.065f, 1);
        isPointerDown = true;
    }
}
