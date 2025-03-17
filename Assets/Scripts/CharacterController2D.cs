using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Oyuncu zýpladýðýnda eklenen kuvvet miktarý.
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // Çömelme hareketine uygulanan maxSpeed miktarý. 1 = %100
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = 0f;     // Hareketi ne kadar yumuþatacaðý
    [SerializeField] private bool m_AirControl = false;                         // Oyuncunun zýplarken yönlendirme yapýp yapamayacaðý;
    [SerializeField] private LayerMask m_WhatIsGround;                          // Karakter için yerin ne olduðunu belirleyen bir maske
    [SerializeField] private Transform m_GroundCheck;                           // Oyuncunun yerde olup olmadýðýný kontrol etmek için iþaretlenen bir konum.
    [SerializeField] private Transform m_CeilingCheck;                          // Tavaný kontrol etmek için iþaretlenen bir konum
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // Çömelirken devre dýþý býrakýlacak bir çarpýþma kutusu

    const float k_GroundedRadius = .2f; // Yerde olup olmadýðýný belirlemek için kullanýlan daire dökümünün yarýçapý
    private bool m_Grounded;            // Oyuncunun yerde olup olmadýðý.
    const float k_CeilingRadius = .2f; // Oyuncunun ayaða kalkýp kalkamayacaðýný belirlemek için kullanýlan daire dökümünün yarýçapý
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // Oyuncunun þu anda hangi yöne baktýðýný belirlemek için.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // Yere kontrol konumuna yapýlan bir daire dökümü, zemin olarak belirlenmiþ herhangi bir þeye çarparsa oyuncu yerde olur
        // Bu katmanlar kullanýlarak yapýlabilir, ancak Örnek Varlýklar proje ayarlarýnýzý geçersiz kýlmaz.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // Çömeliyorsa, karakterin ayaða kalkýp kalkamayacaðýný kontrol et
        if (!crouch)
        {
            // Karakterin ayaða kalkmasýný engelleyen bir tavan varsa, çömelmeye devam et
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        // Oyuncuyu yalnýzca yerdeyse veya hava kontrolü açýksa kontrol et
        if (m_Grounded || m_AirControl)
        {

            // Çömeliyorsa
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Hýzý çömelme hýzý çarpaný ile azalt
                move *= m_CrouchSpeed;

                // Çömelirken çarpýþma kutularýndan birini devre dýþý býrak
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Çömelmiyorken çarpýþma kutusunu etkinleþtir
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Karakteri hedef hýzý bularak hareket ettir
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // Ve ardýndan bu hareketi yumuþatarak karaktere uygula
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // Eðer giriþ oyuncuyu saða doðru hareket ettiriyorsa ve oyuncu sola bakýyorsa...
            if (move > 0 && !m_FacingRight)
            {
                // ... oyuncuyu çevir.
                Flip();
            }
            // Aksi halde giriþ oyuncuyu sola doðru hareket ettiriyorsa ve oyuncu saða bakýyorsa...
            else if (move < 0 && m_FacingRight)
            {
                // ... oyuncuyu çevir.
                Flip();
            }
        }
        // Oyuncu zýplamalýysa...
        if (m_Grounded && jump)
        {
            // Oyuncuya dikey bir kuvvet ekle.
            m_Grounded = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Oyuncunun hangi yöne baktýðýný deðiþtir.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
