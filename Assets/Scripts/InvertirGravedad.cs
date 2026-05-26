using UnityEngine;

public class InvertirGravedad : MonoBehaviour
{
    [Header("Objetos a Invertir")]
    [SerializeField] GameObject[] objetosAInvertir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject obj in objetosAInvertir)
            {
                if (obj != null)
                {
                    // 1. Invertir la gravedad en el Rigidbody2D
                    Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        // Multiplicamos por -1. 
                        // Así, si la gravedad es 1 pasa a -1. Si pisan otro trigger, vuelve a la normalidad (1).
                        rb.gravityScale *= -1f;
                    }

                    // 2. Hacer Flip en el eje Y girando todo el objeto
                    Vector3 escalaActual = obj.transform.localScale;
                    escalaActual.y *= -1f;
                    obj.transform.localScale = escalaActual;
                }
            }

            // Desactivamos el colisionador para que el efecto solo se aplique una vez al pasar
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
