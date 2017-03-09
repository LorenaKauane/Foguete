using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogueteTeste : MonoBehaviour {
	/*Variavel responsavel para calcular o tempo*/
	float time = 5.0f;
	/*Variavel responsavel pela parte de tras do foguete*/
	public GameObject primeiroEstagio;
	/*Variavel responsavel do paraquedas*/
	public GameObject paraquedas;


	void Start () {

	}

	void Update () {

		ajustarAngulo ();
		pouso ();
	}
	/*Metodo responsavel por ajustar o angulo caso o usuario deseje.*/
	void ajustarAngulo(){
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			/*Para rodar o foguete, vezes 3 por que e a velocidade da rotação */
			transform.Rotate (Vector3.back * 3);
		}
		if(Input.GetKey (KeyCode.LeftArrow)){
			transform.Rotate (Vector3.forward * 3 );
		}
		if (Input.GetKey (KeyCode.Space)) {
			
			lancamento ();
		}

	}
	/*Metodo responsavel por lançar o foguete*/
	void lancamento(){
		/*Ativando a gravidade do componente foguete*/
		GetComponent<Rigidbody> ().useGravity = true;
		/*Se o contador foi maior que zero vai dando força ao foguete*/
		while (time >= 0) {
			/*Transform UP para ir a onde o foguete esta apontando, * 3 para a velocidade*/
			GetComponent<Rigidbody> ().AddForce (transform.up * 3);
			/*Decrementado , com os quadros por segundo com o deltaTime*/
			time -= Time.deltaTime;
			}	
		}

	/*Metodo responsavel pelo pouso*/
	void pouso (){
		Debug.Log ("POSICAO Y VELOCITY::: "+GetComponent<Rigidbody> ().velocity.y);
		/*Se a velocidade do Y do foguete, for menor que  -1 e por que esta descendo*/
		if (GetComponent<Rigidbody> ().velocity.y < -1) {
			/*O drag responsavel pela descida mais '' leve '' */
			GetComponent<Rigidbody> ().drag = 2;
			/*A parte de tras do foguete neste momento não tem mais pai para poder sair da parte do foguete
			  e voar*/
			primeiroEstagio.transform.SetParent (null);
			/*Coloco o kinematic como falso para poder descer */
			primeiroEstagio.GetComponent<Rigidbody> ().isKinematic = false;
			/* adiciono uma força para ele descer e na posição desejada x,y,z*/
			primeiroEstagio.GetComponent<Rigidbody> ().AddForce (new Vector3(-2,-10,0));
			/*Se a parte de tras do foguete estiver em uma determinada posição, que no meu caso e -2 
			  o paraquedas aparece visivel*/
			if (primeiroEstagio.transform.position.y < -2) {
				paraquedas.SetActive (true);
			}
			/*Para realizar a rotação  se for maior que -1 ele roda */
			//Debug.Log ("Rotação pela direita::: " +transform.rotation.z);
			if (transform.rotation.z  > -1) {
				/*Calcular a rotação -1 * o quadro por segundo * a velocidade que eu desejo que rode*/
				transform.Rotate (Vector3.back*(Time.deltaTime*40));
			}

		}

	}
}


