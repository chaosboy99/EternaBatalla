using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Variables generales de jugador
    public float speed = 5; // Velocidad del jugador
    public float jumpHeight = 5; // Altura del salto del jugador origunal 15
    public PhysicalCC physicalCC; // Componente del personaje que maneja la física

    public Transform bodyRender; // Transform del cuerpo del jugador
    IEnumerator sitCort; // Enumerator para la animación de sentarse
    public bool isSitting; // Booleano para saber si el jugador está sentado

    // Componente Animator del personaje
    private Animator animPersonaje;
    void Start()
    {
        animPersonaje = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica si el personaje está en el suelo
        if (physicalCC.isGround)
        {
            // Establece la entrada del movimiento en función de las teclas de dirección y la velocidad
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            physicalCC.moveInput = Vector3.ClampMagnitude(transform.forward * y + transform.right * x, 1f) * speed;
            Debug.Log("X:" + x +" Y:"+y);
            // Establece las variables de velocidad en el componente Animator
            animPersonaje.SetFloat("velocidadX", x);
            animPersonaje.SetFloat("velocidadY", y);

            // Verifica si el jugador presiona la tecla Espacio para saltar
            if (Input.GetKeyDown(KeyCode.E))
            {
                physicalCC.inertiaVelocity.y = 0f; // Resetea la velocidad de inercia vertical
                physicalCC.inertiaVelocity.y += jumpHeight; // Agrega la altura del salto a la velocidad de inercia vertical
            }

            // Verifica si el jugador presiona la tecla C para sentarse
            if (Input.GetKeyDown(KeyCode.C) && sitCort == null)
            {
                // Inicia la animación de sentarse
                sitCort = sitDown();
                StartCoroutine(sitCort);
            }
            // Verifica si el jugador presiona el clic derecho para atacar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
    }

    // Enumerator para la animación de sentarse
    IEnumerator sitDown()
    {
        // Verifica si el jugador ya está sentado y si hay algo encima de él
        if (isSitting && Physics.Raycast(transform.position, Vector3.up, physicalCC.cc.height * 1.5f))
        {
            sitCort = null;
            yield break;
        }
        // Cambia el estado de sentado del jugador
        isSitting = !isSitting;

        // Establece los tamaños iniciales y finales del personaje y su transform para la animación
        float t = 0;
        float startSize = physicalCC.cc.height;
        float finalSize = isSitting ? physicalCC.cc.height / 2 : physicalCC.cc.height * 2;

        Vector3 startBodySize = bodyRender.localScale;
        Vector3 finalBodySize = isSitting ? bodyRender.localScale - Vector3.up * bodyRender.localScale.y / 2f : bodyRender.localScale + Vector3.up * bodyRender.localScale.y;

        // Ajusta la velocidad y altura del salto en función de si el jugador está sentado o no
        speed = isSitting ? speed / 2 : speed * 2;
        jumpHeight = isSitting ? jumpHeight * 3 : jumpHeight / 3;

        // Realiza la animación de sentarse durante 0.2 segundos
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            // Interpola el tamaño del CharacterController y el transform del cuerpo del jugador
            physicalCC.cc.height = Mathf.Lerp(startSize, finalSize, t / 0.2f);
            bodyRender.localScale = Vector3.Lerp(startBodySize, finalBodySize, t / 0.2f);
            yield return null;
        }

        // Resetea el enumerator de la animación
        sitCort = null;
        yield break;
    }
    // Función de ataque
    void Attack()
    {
        // Busca el objeto enemigo
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemigo"))
            {
                animPersonaje.SetTrigger("ataque");
                Debug.Log("Ataque1");
            }
        }
    }
}
