//Atualizado 29/08/2025
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private CharacterController personagem;
    private Animator animator;

    public Camera seguirCamera;

    [Header("Movimentação")]
    public float velocidadeNormal = 5f;
    public float velocidadeCorrida = 8f;
    public float velocidadeRotacao = 10f;

    [Header("Pulo & Gravidade")]
    public float alturaPulo = 1.0f;
    public float gravidade = -9.81f;

    private Vector3 velocidadeJogador;
    private bool jogadorNoChao;

    void Start()
    {
        personagem = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Se não arrastar a câmera no Inspector, pega a principal
        if (seguirCamera == null)
            seguirCamera = Camera.main;
    }

    void Update()
    {
        Mover();
    }

    void Mover()
    {
        // Checa se está no chão
        jogadorNoChao = personagem.isGrounded;
        if (jogadorNoChao && velocidadeJogador.y < 0)
        {
            velocidadeJogador.y = -2f; // mantém no chão
        }

        // Entrada de movimento
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        // Define velocidade (anda ou corre)
        bool correndo = Input.GetKey(KeyCode.LeftShift);
        float velocidadeAtual = correndo ? velocidadeCorrida : velocidadeNormal;

        // Movimento relativo à câmera
        Vector3 moveInput = Quaternion.Euler(0, seguirCamera.transform.eulerAngles.y, 0) * new Vector3(hInput, 0, vInput);
        Vector3 movementDirection = moveInput.normalized;

        // Move personagem
        personagem.Move(movementDirection * velocidadeAtual * Time.deltaTime);

        // Rotação suave
        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, velocidadeRotacao * Time.deltaTime);
        }

        // ---- ANIMAÇÕES ----
        animator.SetBool("Mover", movementDirection != Vector3.zero);
        animator.SetBool("EstaNoChao", jogadorNoChao);
        animator.SetBool("Correndo", correndo && movementDirection != Vector3.zero);

        // Pulo
        if (Input.GetButtonDown("Jump") && jogadorNoChao)
        {
            velocidadeJogador.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
            animator.SetTrigger("Saltar");
        }

        // Gravidade
        velocidadeJogador.y += gravidade * Time.deltaTime;
        personagem.Move(velocidadeJogador * Time.deltaTime);
    }
        // --- Chamado pelo trampolim ---
        public void SetTrampolineForce(float force)
    {
        velocidadeJogador.y = Mathf.Sqrt(force * -2f * gravidade);
     }
}

/*
Animator precisa desses parâmetros: 
Bool → Mover
Bool → EstaNoChao
Bool → Correndo
Trigger → Saltar
*/

/*
//código antigo
using UnityEngine;

public class Player : MonoBehaviour
{
	private CharacterController personagem;
	public float velocidade = 5f;
	public float velocidadeRotacao = 10f;
	public Camera seguirCamera;
	private Vector3 velocidadeJogador;
	private bool jogadorNoChao;
	public float alturaPulo = 1.0f;
	public float gravidade = -9.81f;

	void Start()
	{
		personagem = GetComponent<CharacterController>();
	}

	void Update()
	{
		Mover();
	}

	void Mover()
	{
		jogadorNoChao = personagem.isGrounded;
		if (jogadorNoChao && velocidadeJogador.y < 0)
		{
			velocidadeJogador.y = 0f;
		}

		float hInput = Input.GetAxis("Horizontal");
		float vInput = Input.GetAxis("Vertical");

		Vector3 moveInput = Quaternion.Euler(0, seguirCamera.transform.eulerAngles.y, 0) * new Vector3(hInput, 0, vInput);
		Vector3 movementDirection = moveInput.normalized;

		personagem.Move(movementDirection * velocidade * Time.deltaTime);

		if (movementDirection != Vector3.zero)
		{
			Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, velocidadeRotacao * Time.deltaTime);
		}
		if (Input.GetButtonDown("Jump") && jogadorNoChao)
		{
			velocidadeJogador.y += Mathf.Sqrt(alturaPulo * -3.0f * gravidade);
		}
		velocidadeJogador.y += gravidade * Time.deltaTime;
		personagem.Move(velocidadeJogador * Time.deltaTime);
	}
}
*/