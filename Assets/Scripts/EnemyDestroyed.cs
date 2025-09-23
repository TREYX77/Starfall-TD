using UnityEngine;

public class EnemyDestroyed : MonoBehaviour
{
    [SerializeField] private string designatedTag = "Enemy";

    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(designatedTag))
        {
            Destroy(gameObject);
        }
    }
}
