using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var life = other.GetComponent<PlayerLifeSystem>();
            if (life != null)
            {
                life.TakeDamage();
            }

            gameObject.SetActive(false);
        }
    }

}
