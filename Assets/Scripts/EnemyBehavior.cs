using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float velocidadeMovimento;

    [SerializeField]
    private float distanciaMinima;

    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    private Transform alvo;

    [SerializeField]
    private float raioVisao;

    [SerializeField]
    private LayerMask layerAreaVisao;

    [SerializeField]
    private float distanciaMaximaAtaque;

    [SerializeField]
    private float intervaloEntreAtaquesEmSegundos;

    private float tempoEsperaProximoAtaqueEmSegundos;

    // Start is called before the first frame update
    void Start()
    {
        this.tempoEsperaProximoAtaqueEmSegundos = this.intervaloEntreAtaquesEmSegundos;
    }

    // Update is called once per frame
    void Update()
    {
        ProcurarJogador();
        if (this.alvo != null) {
            Mover();
            VerificarPossibilidadeAtaque();
        } else {
            PararMovimentacao();
        }
    }

    private void VerificarPossibilidadeAtaque() {
        float distancia = Vector3.Distance(this.transform.position, this.alvo.position);
        if (distancia <= this.distanciaMaximaAtaque) {
            this.tempoEsperaProximoAtaqueEmSegundos -= Time.deltaTime;
            if (this.tempoEsperaProximoAtaqueEmSegundos <= 0) {
                this.tempoEsperaProximoAtaqueEmSegundos = this.intervaloEntreAtaquesEmSegundos;
                Atacar();
            }
        }
    }

    private void Atacar() {
        Health jogador = this.alvo.GetComponent<Health>();
        jogador.Damage(2);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, this.raioVisao);
    }

    private void ProcurarJogador() {
        Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, this.layerAreaVisao);
        if (colisor != null) {
            this.alvo = colisor.transform;
        } else {
            this.alvo = null;
        }
    }

    private void Mover() {
        Vector2 posicaoAlvo = this.alvo.position;
        Vector2 posicaoAtual = this.transform.position;

        float distancia = Vector2.Distance(posicaoAtual, posicaoAlvo);
        if (distancia >= distanciaMinima) {
            Vector2 direcao = posicaoAlvo - posicaoAtual;
            direcao = direcao.normalized;

            this.rigidbody.velocity = (this.velocidadeMovimento * direcao);

            if (this.rigidbody.velocity.x > 0) {
                this.spriteRenderer.flipX = false;
            } else if (this.rigidbody.velocity.x < 0) {
                this.spriteRenderer.flipX = true;
            }
        } else {
            PararMovimentacao();
        }
    }

    private void PararMovimentacao() {
        this.rigidbody.velocity = Vector2.zero;
    }

}
