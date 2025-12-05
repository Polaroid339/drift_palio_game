using UnityEngine;

public class ManipularVida : MonoBehaviour
{
	VidaJogador vidaJogador;
	
	public int quantidade;
	public float damageTime;
	float currentDamageTime;
	
	void Start()
	{
		vidaJogador = GameObject.FindWithTag("Player").GetComponent<VidaJogador>();
	}
	
	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			currentDamageTime += Time.deltaTime;
			if(currentDamageTime > damageTime)
			{
				vidaJogador.AlterarVida(quantidade);
				currentDamageTime = 0.0f;
			}
		}
	}
}