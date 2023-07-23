using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationController : MonoBehaviour
{

    public static VibrationController instance;

    private float timer = 0f;
    private bool isVibrating = false;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (isVibrating)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                StopVibration();
            }
        }
    }

    public void StartVibration(float leftMotorIntensity, float rightMotorIntensity, float vibrationDuration)
    {

        if (Gamepad.current == null)
        {
            Debug.LogWarning("No gamepad found.");
            return;
        }

        
        Gamepad.current.SetMotorSpeeds(leftMotorIntensity, rightMotorIntensity);

        timer = vibrationDuration;
        isVibrating = true;
    }

    private void StopVibration()
    {
        Gamepad.current.SetMotorSpeeds(0f, 0f);

        timer = 0f;
        isVibrating = false;
    }
}
