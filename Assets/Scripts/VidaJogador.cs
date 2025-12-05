using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
	public float vida = 100;
	public Image barraDeVida;
	public TMP_Text TextoSaude; 
	public Animator animator; 
	
	private float tempoMostraSinal = 1.5f;
	private float cronometroSinal = 0f;
	private string sinalAtual = "";
	
	private bool estaVivo =  true;
	
	void Start()
	{
		AtualizarInterface(0);
	}

	void Update()
	{
		if(cronometroSinal > 0)
			cronometroSinal -= Time.deltaTime;

		if(vida <= 0 && estaVivo)
		{
			estaVivo =  false;
			GameOver();
		}			

		AtualizarInterface(0);
	}

	public void AlterarVida(float delta)
	{
		if(!estaVivo) return;
		
		vida += delta;
		vida = Mathf.Clamp(vida, 0, 100);
		
		if(delta > 0)
			sinalAtual = "+";
		else if(delta < 0)
			sinalAtual = "-";
		else
			sinalAtual = "";
		
		cronometroSinal = tempoMostraSinal;
		
		AtualizarInterface(delta);
		
		if(vida <= 0 && animator != null)
		{
			animator.enabled = false;
		}
	}
	
	private void AtualizarInterface(float delta)
	{
		barraDeVida.fillAmount = vida / 100f;
		
		if(vida > 0)
		{
			if(cronometroSinal > 0)
				TextoSaude.text = $"{sinalAtual}{vida:F0}";
			else
				TextoSaude.text = vida.ToString("F0");
		}
		else
		{
			TextoSaude.text = "- MORTO -";
		}
	}
	
	private void GameOver()
	{
		Debug.Log("Game Over!");
		SceneManager.LoadScene("Menu Inicial");
	}
}


