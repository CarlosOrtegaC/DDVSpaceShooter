using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject disparoPrefap;
    [SerializeField] private GameObject spawnPoint1;
    [SerializeField] private GameObject spawnPoint2;
    [SerializeField] private AudioClip disparoSonido;
    [SerializeField] private List<Image> vidasUI; //Lista de imágenes de las vidas
    [SerializeField] private GameObject panelMuerte; //Panel que se muestra al morir

    private AudioSource audioSource;
    private float temporizador = 0.5f;
    private int vidas = 5;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ActualizarVidasVisuales();
        panelMuerte.SetActive(false); //Me aseguro que el panel esté desactivado al inicio
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        DelimitarMovimiento();
        Disparar();
    }
    void Movimiento() 
    { 
        //Nos movemos de forma libre en ambos ejes
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * velocidad * Time.deltaTime);
    }
    void DelimitarMovimiento()
    {
        //Limito movimiento para no salirme de los márgenes
        float xDelimitada = Mathf.Clamp(transform.position.x, -8.25f, 8.25f);
        float yDelimitada = Mathf.Clamp(transform.position.y, -4.45f, 4.45f);
        transform.position = new Vector3(xDelimitada, yDelimitada, 0);
    }

    void Disparar() 
    {
        temporizador += 1 * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && temporizador > ratioDisparo)
        {
            Instantiate(disparoPrefap, spawnPoint1.transform.position, Quaternion.identity);
            Instantiate(disparoPrefap, spawnPoint2.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(disparoSonido);
            temporizador = 0;
        }
    
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if(elOtro.gameObject.CompareTag("DisparoEnemigo") || elOtro.gameObject.CompareTag("Enemigo"))
        {
            vidas --;
            ActualizarVidasVisuales();
            Destroy(elOtro.gameObject);

            if (elOtro.CompareTag("Enemigo"))
            {
                FindObjectOfType<ScoreManager>()?.AddScore(10); //Añade 10 puntos si es un enemigo con el que colisiona
            }

            if (vidas <= 0)
            {
                Destroy(this.gameObject);
                Muerte();
            }
        }
    }

    void ActualizarVidasVisuales()
    {
        //Oculta las imágenes de las vidas según la cantidad restante
        for (int i = 0; i < vidasUI.Count; i++)
        {
            vidasUI[i].enabled = i < vidas; //Activa solo las imágenes correspondientes a las vidas restantes
        }
    }
    void Muerte()
    {
        Time.timeScale = 0; //Detiene todo el juego
        panelMuerte.SetActive(true); //Muestra el panel de muerte
    }

    public void AumentarVelocidad(float nuevaVelocidad)
    {
        velocidad = nuevaVelocidad;
    }

    public void AumentarRatioDisparo(float nuevoRatio)
    {
        ratioDisparo = nuevoRatio;
    }
}
