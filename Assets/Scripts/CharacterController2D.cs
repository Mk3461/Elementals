using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Oyuncu z�plad���nda eklenen kuvvet miktar�.
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // ��melme hareketine uygulanan maxSpeed miktar�. 1 = %100
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = 0f;     // Hareketi ne kadar yumu�ataca��
    [SerializeField] private bool m_AirControl = false;                         // Oyuncunun z�plarken y�nlendirme yap�p yapamayaca��;
    [SerializeField] private LayerMask m_WhatIsGround;                          // Karakter i�in yerin ne oldu�unu belirleyen bir maske
    [SerializeField] private Transform m_GroundCheck;                           // Oyuncunun yerde olup olmad���n� kontrol etmek i�in i�aretlenen bir konum.
    [SerializeField] private Transform m_CeilingCheck;                          // Tavan� kontrol etmek i�in i�aretlenen bir konum
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // ��melirken devre d��� b�rak�lacak bir �arp��ma kutusu

    const float k_GroundedRadius = .2f; // Yerde olup olmad���n� belirlemek i�in kullan�lan daire d�k�m�n�n yar��ap�
    private bool m_Grounded;            // Oyuncunun yerde olup olmad���.
    const float k_CeilingRadius = .2f; // Oyuncunun aya�a kalk�p kalkamayaca��n� belirlemek i�in kullan�lan daire d�k�m�n�n yar��ap�
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // Oyuncunun �u anda hangi y�ne bakt���n� belirlemek i�in.
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

        // Yere kontrol konumuna yap�lan bir daire d�k�m�, zemin olarak belirlenmi� herhangi bir �eye �arparsa oyuncu yerde olur
        // Bu katmanlar kullan�larak yap�labilir, ancak �rnek Varl�klar proje ayarlar�n�z� ge�ersiz k�lmaz.
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
        // ��meliyorsa, karakterin aya�a kalk�p kalkamayaca��n� kontrol et
        if (!crouch)
        {
            // Karakterin aya�a kalkmas�n� engelleyen bir tavan varsa, ��melmeye devam et
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        // Oyuncuyu yaln�zca yerdeyse veya hava kontrol� a��ksa kontrol et
        if (m_Grounded || m_AirControl)
        {

            // ��meliyorsa
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // H�z� ��melme h�z� �arpan� ile azalt
                move *= m_CrouchSpeed;

                // ��melirken �arp��ma kutular�ndan birini devre d��� b�rak
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // ��melmiyorken �arp��ma kutusunu etkinle�tir
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Karakteri hedef h�z� bularak hareket ettir
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // Ve ard�ndan bu hareketi yumu�atarak karaktere uygula
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // E�er giri� oyuncuyu sa�a do�ru hareket ettiriyorsa ve oyuncu sola bak�yorsa...
            if (move > 0 && !m_FacingRight)
            {
                // ... oyuncuyu �evir.
                Flip();
            }
            // Aksi halde giri� oyuncuyu sola do�ru hareket ettiriyorsa ve oyuncu sa�a bak�yorsa...
            else if (move < 0 && m_FacingRight)
            {
                // ... oyuncuyu �evir.
                Flip();
            }
        }
        // Oyuncu z�plamal�ysa...
        if (m_Grounded && jump)
        {
            // Oyuncuya dikey bir kuvvet ekle.
            m_Grounded = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Oyuncunun hangi y�ne bakt���n� de�i�tir.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
