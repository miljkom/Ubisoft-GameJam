using UnityEngine;

public class DestroyMiasma : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Miasma"))
        {
            other.transform.parent.GetComponent<RespawningMiasma>().DestroyMiasma();
        }

        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyMovement>().MoveEnemy(transform.position);
    }
}
