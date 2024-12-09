using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDrop : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float tiempoVida = 3f;

    public enum TipoEsfera { Velocidad, RatioDisparo }
    public TipoEsfera tipo;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempoVida); //Destruye la esfera después de un tiempo
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                if (tipo == TipoEsfera.Velocidad)
                {
                    player.AumentarVelocidad(10); //Esfera de aumentar la velocidad
                }
                else if (tipo == TipoEsfera.RatioDisparo)
                {
                    player.AumentarRatioDisparo(0.3f); //Esfera de aumentar el ratio de disparo
                }
            }
            Destroy(gameObject); //Destruye la esfera
        }
    }
}
