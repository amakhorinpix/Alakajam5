using UnityEngine;

public class WizardScript : MonoBehaviour
{

    private bool _canJump = true;
    private bool _atFountain = false;
    public float Speed = 5f;
    public Rigidbody2D Rigidbody;

    private byte _mana;
    public byte Mana
    {
        get { return _mana; }
        set
        {
            if (value > 100) _mana = 100;
            _mana = value;
            Debug.LogWarning(_mana);
        }
    }

	void Update ()
	{
	    Movement();
	    Fountain();
	}

    void Fountain()
    {
        if (_atFountain && Input.GetKeyDown(KeyCode.Q)) Mana++;
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.D)) transform.Translate(Speed * Time.fixedDeltaTime, 0f, 0f);
        if (Input.GetKey(KeyCode.A)) transform.Translate(-Speed * Time.fixedDeltaTime, 0f, 0f);
        if (_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody.AddForce(new Vector2(0f, 400f));
            _canJump = false;

        }
        if (Mathf.Abs(transform.position.x) > 7f) transform.position =
            new Vector3(Mathf.Sign(transform.position.x) * 7f, transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        _atFountain = collider.tag == "Fountain";
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag != "Fountain") return;
        _atFountain = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Floor") return;
        _canJump = true;
    }
}
