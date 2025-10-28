using UnityEngine;

public class CameraFeel : MonoBehaviour
{
   
    public Quaternion baseRotation;

   
    public float additivePitch;

   
    public float returnSpeed = 120f;

   
    public float maxAbsPitch = 10f;

    void Awake()
    {
        baseRotation = transform.rotation;
        additivePitch = 0f;
    }

    void LateUpdate()
    {
        if (Mathf.Abs(additivePitch) > 0.001f)
        {
            float step = returnSpeed * Time.deltaTime * Mathf.Sign(additivePitch);
            
            if (Mathf.Abs(step) > Mathf.Abs(additivePitch)) step = additivePitch;
            additivePitch -= step;
        }

        transform.rotation = baseRotation * Quaternion.Euler(-additivePitch, 0f, 0f);
    }

    public void KickTiltUp(float degrees)
    {
        additivePitch += degrees;           
        additivePitch = Mathf.Clamp(additivePitch, -maxAbsPitch, maxAbsPitch);
    }


    public void KickTiltDown(float degrees)
    {
        additivePitch -= degrees;               
        additivePitch = Mathf.Clamp(additivePitch, -maxAbsPitch, maxAbsPitch);
    }

    public void ResetBaseRotationNow()
    {
        baseRotation = transform.rotation;
        additivePitch = 0f;
    }
}
