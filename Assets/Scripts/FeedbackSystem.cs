using UnityEngine;

public class FeedbackSystem : MonoBehaviour
{
    public ParticleSystem jumpEffect;
    public AudioSource jumpSound;

    public void OnJumpStart()
    {
        if (jumpEffect != null) jumpEffect.Play();

        if (jumpSound != null)
        {
            jumpSound.loop = true;
            jumpSound.Play();
        }
    }

    public void OnJumpEnd()
    {
        if (jumpSound != null)
        {
            jumpSound.loop = false;
            jumpSound.Stop();
        }
    }
}
