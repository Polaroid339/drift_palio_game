using UnityEngine;

public class EnemyFollowNoNavMesh : MonoBehaviour
{
    public Transform player;          // Arraste o Player aqui
    public float speed = 3f;          // Velocidade ao seguir
    public float followDistance = 15f; // Distância em que ele começa a seguir
    public float rotationSpeed = 5f;   // Suavidade ao rotacionar

    void Update()
    {
        if (player == null) return;

        // Distância entre inimigo e jogador
        float distance = Vector3.Distance(transform.position, player.position);

        // Só segue se estiver dentro do raio
        if (distance <= followDistance)
        {
            // Direção até o jogador
            Vector3 direction = (player.position - transform.position).normalized;

            // Rotaciona suavemente em direção ao jogador
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // Move para frente
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}