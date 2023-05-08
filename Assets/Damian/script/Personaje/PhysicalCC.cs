using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicalCC : MonoBehaviour
{
    public CharacterController cc { get; private set; } 
	// Propiedad pública que accede al componente CharacterController del personaje. El getter es público y el setter es privado.

    private IEnumerator dampingCor; 
	// Variable privada que almacena una referencia a una corutina que se encarga de aplicar amortiguación a la velocidad del personaje.

    [Header("Ground Check")]
    public bool isGround; 
	// Variable pública que indica si el personaje está tocando el suelo.
    public float groundAngle; 
	// Variable pública que almacena el ángulo de la superficie sobre la que el personaje está caminando.
    public Vector3 groundNormal { get; private set; } 
	// Propiedad pública que almacena la normal de la superficie sobre la que el personaje está caminando. El getter es público y el setter es privado.

    [Header("Movement")]
    public bool ProjectMoveOnGround;
	// Variable pública que indica si el movimiento del personaje debe proyectarse sobre la superficie del suelo.
    public Vector3 moveInput; 
	// Variable pública que almacena la dirección del movimiento del personaje.
    private Vector3 moveVelocity; 
	// Variable privada que almacena la velocidad del movimiento del personaje.

    [Header("Slope and inertia")]
    public float slopeLimit = 45; 
	// Variable pública que indica el ángulo máximo de inclinación de la superficie sobre la que el personaje puede caminar.
    public float inertiaDampingTime = 0.1f; 
	// Variable pública que determina el tiempo de amortiguación de la velocidad del personaje al cambiar de dirección.
    public float slopeStartForce = 3f; 
	// Variable pública que indica la fuerza inicial que se aplica al personaje cuando comienza a caminar sobre una superficie inclinada.
    public float slopeAcceleration = 3f; 
	// Variable pública que indica la aceleración del personaje cuando camina sobre una superficie inclinada.
    public Vector3 inertiaVelocity;
	// Variable pública que almacena la velocidad de inercia del personaje.

    [Header("Interaction with the platform")]
    public bool platformAction; 
	// Variable pública que indica si el personaje está interactuando con una plataforma.
    public Vector3 platformVelocity; 
	// Variable pública que almacena la velocidad de la plataforma con la que está interactuando el personaje.

    [Header("Collision")]
    public bool applyCollision = true; 
	// Variable pública que indica si se deben aplicar las colisiones al personaje.
    public float pushForce = 55f;
	// Variable pública que indica la fuerza con la que se empuja al personaje cuando choca con un objeto.


    private void Start()
	{
		cc = GetComponent<CharacterController>();
	}

	private void Update()
	{
        // Realiza una comprobación del suelo para determinar si el personaje está actualmente en el suelo
        GroundCheck();

        // Sólo realiza el movimiento si el personaje está en el suelo
        if (isGround)
        {
            // Proyectar el vector de entrada de movimiento sobre el plano de tierra
            moveVelocity = ProjectMoveOnGround ? Vector3.ProjectOnPlane(moveInput, groundNormal) : moveInput;

            // Si el personaje está en una pendiente, realiza una amortiguación de inercia para ralentizar su movimiento
            if (groundAngle < slopeLimit && inertiaVelocity != Vector3.zero)
            {
                InertiaDamping();
            }
        }

        // Aplicar gravedad al personaje mediante el metodo
        GravityUpdate();

        // Calcula la dirección de movimiento global del personaje, que incluye la velocidad de movimiento, 
        // velocidad de inercia, y cualquier velocidad de plataforma
        Vector3 moveDirection = (moveVelocity + inertiaVelocity + platformVelocity);

        // Mueve el personaje usando el componente CharacterController
        cc.Move((moveDirection) * Time.deltaTime);

    }

    private void GravityUpdate()
	{
		if (isGround && groundAngle > slopeLimit)
		{
			inertiaVelocity += Vector3.ProjectOnPlane(groundNormal.normalized + (Vector3.down * (groundAngle / 30)).normalized * Mathf.Pow(slopeStartForce, slopeAcceleration), groundNormal) * Time.deltaTime;
		}
        //else if (!isGround) inertiaVelocity.y -= Mathf.Pow(3f, 3) * Time.deltaTime;
        else if (!isGround)
        {
            // Si el personaje no está en el suelo, aplica la gravedad de caída libre
            inertiaVelocity.y -= Mathf.Pow(3f, 3) * Time.deltaTime;
        }

    }


    // Aplica amortiguación de inercia al personaje para ralentizar su movimiento en una pendiente.
    private void InertiaDamping()
	{
		var a = Vector3.zero;

        //frenado de la inercia al aplicar la fuerza de movimiento
        //calcular el ángulo entre la velocidad de inercia y los vectores de velocidad de movimiento
        var resistanceAngle = Vector3.Angle(Vector3.ProjectOnPlane(inertiaVelocity, groundNormal),
            Vector3.ProjectOnPlane(moveVelocity, groundNormal));

        // Si el ángulo de resistencia es 0, ajústalo a 90 para evitar un error de división por 0
        resistanceAngle = resistanceAngle == 0 ? 90 : resistanceAngle;

        // Amortigua suavemente la velocidad de inercia en el tiempo para ralentizar el movimiento del personaje en una pendiente.
        inertiaVelocity = (inertiaVelocity + moveVelocity).magnitude <= 0.1f ? Vector3.zero :
            Vector3.SmoothDamp(inertiaVelocity, Vector3.zero, ref a, inertiaDampingTime / (3 / (180 / resistanceAngle)));
    }


    // Realiza una comprobación del suelo para determinar si el personaje está actualmente en el suelo
    private void GroundCheck()
	{
        // Lanza una esfera hacia abajo desde la posición del personaje para comprobar si colisiona con el suelo
        if (Physics.SphereCast(transform.position, cc.radius, Vector3.down, out RaycastHit hit, cc.height / 2 - cc.radius + 0.01f))
        {
            isGround = true;
            groundAngle = Vector3.Angle(Vector3.up, hit.normal);
            groundNormal = hit.normal; ;

            /**
			 * if (hit.transform.tag == "Platform")
				platformVelocity = hit.collider.attachedRigidbody == null | !platformAction ?
				 Vector3.zero : hit.collider.attachedRigidbody.velocity;
			 */
            // Si el personaje está de pie sobre una plataforma, obtener la velocidad de la plataforma para tener en cuenta el movimiento
            if (hit.transform.tag == "Platform")
            {
                platformVelocity = hit.collider.attachedRigidbody == null || !platformAction ? Vector3.zero :
                    hit.collider.attachedRigidbody.velocity;
            }

            // Compruebe el ángulo del suelo utilizando un molde de caja para detectar mejor el ángulo de la pendiente
            if (Physics.BoxCast(transform.position, new Vector3(cc.radius / 2.5f, cc.radius / 3f, cc.radius / 2.5f), Vector3.down, out RaycastHit helpHit, transform.rotation, cc.height / 2 - cc.radius / 2))
            {
                groundAngle = Vector3.Angle(Vector3.up, helpHit.normal);
            }
        }
        else
        {
            platformVelocity = Vector3.zero;
            isGround = false;
        }
    }

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        // The OnControllerColliderHit method is called when the controller hits a collider while performing a Move
        if (!applyCollision) return;

        // Obtiene el componente Rigidbody del colisionador golpeado por el controlador
        Rigidbody body = hit.collider.attachedRigidbody;

        // Comprueba si el Rigidbody es nulo o cinemático
        if (body == null || body.isKinematic)
        {
            return;
        }

        // Calcular la dirección en la que empujar el Rigidbody
        Vector3 pushDir = hit.point - (hit.point + hit.moveDirection.normalized);

        // Aplica una fuerza al Rigidbody en la dirección del empuje
        body.AddForce(pushDir * pushForce, ForceMode.Force);

    }

}
