using UnityEngine;

public class TempControl : MonoBehaviour
{
    private float temp;
    private static int tempController;
    [SerializeField] Transform air, water, ice;
    [SerializeField] GameObject heat, hold, chill;
    [SerializeField] TextMesh tempText, waterState;
    [SerializeField] Transform piston, pistonPath;
    [SerializeField] ParticleSystem bubblePS;
    [SerializeField] AudioSource buttonSound;
    private float orgWater, orgAir;

    private void Start()
    {
        orgWater = water.localScale.y;
        orgAir = air.localScale.y;
        tempController = 1;
        temp = 25.0f;
    }
    private void Update()
    {
        tempText.text = temp.ToString("F1");
        TempController();

        if (air.localScale.y > 0.899f)
        {
            piston.position = pistonPath.position;
        }

    }

    private void TempController()
    {
        switch (tempController)
        {
            case 0:
                // Heat
                Boil();
                break;
            case 1:
                // Hold
                heat.SetActive(false);
                hold.SetActive(true);
                chill.SetActive(false);
                break;
            case 2:
                // Chill
                Chill();
                break;
        }
    }

    private void Boil()
    {
        heat.SetActive(true);
        hold.SetActive(false);
        chill.SetActive(false);

        if (temp < 0)
        {
            temp += Time.deltaTime * 8;
            waterState.gameObject.SetActive(false);
            return;
        }

        float waterScale = water.localScale.y;
        if ((waterScale < orgWater) && (temp >= 0) && (temp <= 25))
        {
            bubblePS.Stop();
            waterState.text = "Melting";
            waterState.gameObject.SetActive(true);
            waterScale += (Time.deltaTime / 5);
            water.localScale = new Vector3(water.localScale.x, waterScale, water.localScale.z);
            return;
        }

        ice.gameObject.SetActive(false);

        if (temp <= 100)
        {
            temp += Time.deltaTime * 8;
            waterState.gameObject.SetActive(false);
            return;
        }

        if (waterScale > 0)
        {
            waterScale -= (Time.deltaTime / 5);
            water.localScale = new Vector3(water.localScale.x, waterScale, water.localScale.z);
            bubblePS.Play();
        }
        else
        {
            bubblePS.Stop();
        }

        float airScale = air.localScale.y;
        if (airScale <= 2.5f)
        {
            airScale += (Time.deltaTime / 5);
            air.localScale = new Vector3(air.localScale.x, airScale, air.localScale.z);
        }
        else
        {
            if (temp <= 120)
            {
                temp += Time.deltaTime * 8;
                waterState.gameObject.SetActive(false);
            }
            else
            {
                waterState.text = "Maximum Tempreature\nReached";
                waterState.gameObject.SetActive(true);
            }
        }
    }

    private void Chill()
    {
        heat.SetActive(false);
        hold.SetActive(false);
        chill.SetActive(true);
        float waterScale = water.localScale.y;

        if (temp >= 100)
        {
            temp -= Time.deltaTime * 8;
            waterState.gameObject.SetActive(false);
            return;
        }

        if ((waterScale <= orgWater) && (temp > 25))
        {
            bubblePS.Stop();
            waterState.text = "Condensing";
            waterState.gameObject.SetActive(true);
            waterScale += (Time.deltaTime / 5);
            water.localScale = new Vector3(water.localScale.x, waterScale, water.localScale.z);
        }

        float airScale = air.localScale.y;
        if ((airScale >= orgAir) && (temp > 25))
        {
            airScale -= (Time.deltaTime / 5);
            air.localScale = new Vector3(air.localScale.x, airScale, air.localScale.z);
            return;
        }

        if (temp > 0)
        {
            temp -= Time.deltaTime * 8;
            waterState.gameObject.SetActive(false);
            return;
        }

        ice.gameObject.SetActive(true);

        if (waterScale > 0)
        {
            bubblePS.Stop();
            waterState.text = "Freezing";
            waterState.gameObject.SetActive(true);
            waterScale -= (Time.deltaTime / 5);
            water.localScale = new Vector3(water.localScale.x, waterScale, water.localScale.z);
        }
        else
        {
            if (temp > -20)
            {
                temp -= Time.deltaTime * 8;
                waterState.gameObject.SetActive(false);
            }
            else
            {
                waterState.text = "Minimum Tempreature\nReached";
                waterState.gameObject.SetActive(true);
            }
        }
    }

    public void TempControlButton(int tempTo)
    {
        buttonSound.Play();
        tempController = tempTo;
    }
}
