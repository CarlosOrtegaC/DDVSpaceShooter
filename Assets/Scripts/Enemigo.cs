using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject disparoPrefap;
    [SerializeField] private Transform spawnPoint1; //Selecciono la posición del spawn de disparo para que aparezca en su posición
    [SerializeField] private AudioClip disparoSonido;
    [SerializeField] private GameObject esferaVerdePrefab;
    [SerializeField] private GameObject esferaRojaPrefab;
    private Animator animator; //Hacemos referencia al Animator
    private AudioSource audioSource;
    private bool isDestroyed = false; //Con esto evito múltiples impactos

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnearDisparos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad *Time.deltaTime);
    }

    IEnumerator SpawnearDisparos()
    {
        while (true)
        {
            Instantiate(disparoPrefap, spawnPoint1.position, Quaternion.identity); //Le asigno a la instanciación de disparo donde aparece y sin rotación
            audioSource.PlayOneShot(disparoSonido); //Sonido del disparo
            yield return new WaitForSeconds(1f);
        }

    }
    private void OnTriggerEnter2D(Collider2D elOtro) //Este es el enemigo
    {
        if (isDestroyed) return; //Si ya está marcado como destruido, no acepta más colisiones

        if (elOtro.gameObject.CompareTag("DisparoPlayer")) //Quitamos la parte de destrucción con el Player, ya que ya la tiene el Player en su código
        {
            isDestroyed = true; //Marca al enemigo como destruido inmediatamente
            Destroy(elOtro.gameObject); //Destruye el disparo

            //Añade 10 puntos al score
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(10);
            }

            Muerte();
        }
    }
    void DropearEsfera()
    {
        float probabilidadGeneral = Random.Range(0f, 1f);
        if (probabilidadGeneral <= 0.2f) //20% de probabilidad de dropear una esfera
        {
            float probabilidadEsfera = Random.Range(0f, 1f);
            if (probabilidadEsfera <= 0.6f)
            {
                Instantiate(esferaVerdePrefab, transform.position, Quaternion.identity); //Esfera verde (60%)
            }
            else
            {
                Instantiate(esferaRojaPrefab, transform.position, Quaternion.identity); //Esfera roja (40%)
            }
        }
    }

    void Muerte()
    {
        DropearEsfera(); //Llama al método para intentar dropear una esfera
        animator.SetTrigger("Muerte"); //Activa la animación de muerte
        StopAllCoroutines(); //Detiene disparos del enemigo
        Destroy(gameObject, 0.5f); //Tiempo de desstrucción
    }
}
