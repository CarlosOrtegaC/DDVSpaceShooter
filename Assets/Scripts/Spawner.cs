using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private TextMeshProUGUI textoOledas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnearEnemigos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnearEnemigos()
    {
        for (int i = 0; i < 5; i++) // Niveles
        {
            for (int j = 0; j < 3; j++) // Oleadas
            {
                textoOledas.text = "NIVEL " + (i + 1) + " - " + "OLEADA " + (j + 1);
                yield return new WaitForSeconds(2);
                textoOledas.text = "";
                for (int k = 0; k < 10; k++)  // Enemigos
                {
                    Vector3 puntoAleatorio = new Vector3(transform.position.x, Random.Range(-4.45f, 4.45f), 0);
                    Instantiate(enemigoPrefab, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
