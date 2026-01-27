using UnityEngine;

public class NearPlayerTrigger : MonoBehaviour
{
    public bool IsPlayerNearby {  get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = false;
        }
    }
}
