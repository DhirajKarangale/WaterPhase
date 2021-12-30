using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    [Header("Temp Controller")]
    private static int tempController;
    [SerializeField] RectTransform water, air, ice;
    [SerializeField] GameObject heat, hold, chill;
    [SerializeField] TMP_Text tempText, waterState;
    private float temp;

    private void Start()
    {
        tempController = 1;
        temp = 25.0f;
    }

    private void Update()
    {
        tempText.text = temp.ToString("F1");
        TempController();

        if (temp >= 100)
        {
            Boil();
        }
        else
        {
            waterState.gameObject.SetActive(false);
        }
    }

    private void TempController()
    {
        switch (tempController)
        {
            case 0:
                // Heat
                heat.SetActive(true);
                hold.SetActive(false);
                chill.SetActive(false);
                if (temp <= 100) temp += Time.deltaTime * 7;
                break;
            case 1:
                // Hold
                heat.SetActive(false);
                hold.SetActive(true);
                chill.SetActive(false);
                break;
            case 2:
                // Chill
                heat.SetActive(false);
                hold.SetActive(false);
                chill.SetActive(true);
                if (temp > -1) temp -= Time.deltaTime * 7;
                break;
        }
    }

    private void Boil()
    {
        waterState.text = "Boiling";
        waterState.gameObject.SetActive(true);
        float scale = air.localScale.y;
        scale += Time.deltaTime;
        air.localScale = new Vector3(air.localScale.x, scale, air.localScale.z);
    }

    public void TempControlButton(int temp)
    {
        tempController = temp;
    }
    public void Test()
    {
        Debug.Log("Test");
    }
}