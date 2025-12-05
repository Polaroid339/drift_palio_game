using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float trampolineForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu tem o script PlayerMoviment
        PlayerMoviment player = other.GetComponent<PlayerMoviment>();

        if (player != null)
        {
            // Impulso vertical — substitui a gravidade momentaneamente
            player.SetTrampolineForce(trampolineForce);
        }
    }
}
