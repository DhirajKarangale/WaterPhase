using UnityEngine;
using System.Collections;

public sealed class AnimateMolecule : MonoBehaviour
{
    #region Inspector

    [SerializeField] Transform leftPos, rightPos;
    [SerializeField] bool vibrate = false;
    private float speed = -0.2f;
    private float magnitude = 0.008f;
    private bool pingPong = false;
    public static bool vibrateNow;
    private float _baseY;

    #endregion



    #region MonoBehavior

    private void Start()
    {
        if (vibrate)
        {
            speed = 0;
            magnitude = Random.Range(0.005f, 0.01f);
        }
        else
        {
            speed = Random.Range(1.5f, 3.5f);
            if (Random.value > 0.3f)
            {
                pingPong = true;
                speed = -speed;
            }
            transform.position = new Vector3(Random.Range(leftPos.position.x, rightPos.position.x), Random.Range(leftPos.position.y, rightPos.position.y), 0);

            _baseY = transform.position.y;
        }
    }
    private void OnEnable()
    {
        if (vibrate) StartCoroutine(Vibrate());
    }

    private void Update()
    {
        if (vibrate) return;

        Transform myTransform = transform;
        Vector3 pos = myTransform.position;
        pos.x += speed * Time.deltaTime;

        pos.y = _baseY + Mathf.Sin(_baseY + Time.time) * speed / 2;

        bool movingRight = speed > 0;
        bool changeDirection = (movingRight && pos.x >= rightPos.position.x) || (!movingRight && pos.x <= leftPos.position.x);

        if (changeDirection)
        {
            if (pingPong)
            {
                speed *= -1;
                return;
            }

            pos.x = movingRight ? leftPos.position.x : rightPos.position.x;
        }

        myTransform.position = pos;
    }

    public IEnumerator Vibrate()
    {
        Vector3 originalPos = transform.localPosition;
        while (true)
        {
            float x = originalPos.x + Random.Range(-1, 1) * magnitude;
            float y = originalPos.y + Random.Range(-1, 1) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            yield return null;
            transform.localPosition = originalPos;
        }
    }
    #endregion
}