using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "KillZone")
        {
            Destroy(this.gameObject);
        }
    }
}
