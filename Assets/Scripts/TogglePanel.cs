using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;     // O painel
    public Text panelText;       // O texto que já está dentro do painel

    private bool isOpen = false;

    public void Toggle()
    {
        isOpen = !isOpen;

        // Ativa ou desativa o painel
        panel.SetActive(isOpen);

        // Mostra ou esconde o texto que já está no painel
        panelText.gameObject.SetActive(isOpen);
    }
}
