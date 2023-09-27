using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    public GameObject damagePopupGO;
    public TextMeshPro textMesh;
    public TMP_ColorGradient rojo;
    public TMP_ColorGradient naranja;
    public TMP_ColorGradient verde;
    public TMP_ColorGradient blanco;
    public TMP_ColorGradient azul;

    public float disappearTimer = 3;

    void Awake()
    {

    }
    void Update()
    {
        float moveSpeed = 0.5f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        //textMesh.fontSize += Time.deltaTime;
        if (disappearTimer < 0)
        {
            VertexGradient colorGradient = textMesh.colorGradient;
            float disappearSpeed = 10f;
            colorGradient.bottomLeft.a -= disappearSpeed * Time.deltaTime;
            colorGradient.bottomRight.a -= disappearSpeed * Time.deltaTime;
            colorGradient.topLeft.a -= disappearSpeed * Time.deltaTime;
            colorGradient.topRight.a -= disappearSpeed * Time.deltaTime;

            textMesh.colorGradient = colorGradient;

            if (disappearTimer < -5)
            {
                Destroy(damagePopupGO);
            }
        }

    }


    public void Setup(int damageAmount, string color)
    {
        VertexGradient colorGradient = textMesh.colorGradient;
        textMesh.SetText(damageAmount.ToString());
        if (color == "verde")
        {
            colorGradient.topLeft = verde.topLeft;
            colorGradient.topRight = verde.topRight;
            colorGradient.bottomLeft = verde.bottomLeft;
            colorGradient.bottomRight = verde.bottomRight;
        }
        else if (color == "rojo")
        {
            colorGradient.topLeft = rojo.topLeft;
            colorGradient.topRight = rojo.topRight;
            colorGradient.bottomLeft = rojo.bottomLeft;
            colorGradient.bottomRight = rojo.bottomRight;
        }
        else if (color == "naranja")
        {
            colorGradient.topLeft = naranja.topLeft;
            colorGradient.topRight = naranja.topRight;
            colorGradient.bottomLeft = naranja.bottomLeft;
            colorGradient.bottomRight = naranja.bottomRight;
        }
        else if (color == "blanco")
        {
            colorGradient.topLeft = blanco.topLeft;
            colorGradient.topRight = blanco.topRight;
            colorGradient.bottomLeft = blanco.bottomLeft;
            colorGradient.bottomRight = blanco.bottomRight;
        }
        else
        {
            colorGradient.topLeft = azul.topLeft;
            colorGradient.topRight = azul.topRight;
            colorGradient.bottomLeft = azul.bottomLeft;
            colorGradient.bottomRight = azul.bottomRight;
        }

        textMesh.colorGradient = colorGradient;
    }

    public void SetupText(string text, string color)
    {
        VertexGradient colorGradient = textMesh.colorGradient;
        textMesh.SetText(text);
        if (color == "verde")
        {
            colorGradient.topLeft = verde.topLeft;
            colorGradient.topRight = verde.topRight;
            colorGradient.bottomLeft = verde.bottomLeft;
            colorGradient.bottomRight = verde.bottomRight;
        }
        else if (color == "rojo")
        {
            colorGradient.topLeft = rojo.topLeft;
            colorGradient.topRight = rojo.topRight;
            colorGradient.bottomLeft = rojo.bottomLeft;
            colorGradient.bottomRight = rojo.bottomRight;
        }
        else if (color == "naranja")
        {
            colorGradient.topLeft = naranja.topLeft;
            colorGradient.topRight = naranja.topRight;
            colorGradient.bottomLeft = naranja.bottomLeft;
            colorGradient.bottomRight = naranja.bottomRight;
        }
        else if (color == "blanco")
        {
            colorGradient.topLeft = blanco.topLeft;
            colorGradient.topRight = blanco.topRight;
            colorGradient.bottomLeft = blanco.bottomLeft;
            colorGradient.bottomRight = blanco.bottomRight;
        }
        else
        {
            colorGradient.topLeft = azul.topLeft;
            colorGradient.topRight = azul.topRight;
            colorGradient.bottomLeft = azul.bottomLeft;
            colorGradient.bottomRight = azul.bottomRight;
        }

        textMesh.colorGradient = colorGradient;
    }
}
