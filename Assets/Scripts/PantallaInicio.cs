using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicio : MonoBehaviour
{
    [SerializeField] private GameObject ImagenControles;
    private bool isActive = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isActive)
        {
            ImagenControles.SetActive(false);
            isActive = false;
        }
    }

    public void ClickEmpezar()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickControles()
    {
        isActive = true;
        ImagenControles.SetActive(true);
    }


}
