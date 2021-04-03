using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrigger : NetworkBehaviour
{
    public bool Grounded;
    public bool CoroutineAllowed;
    private Transform self;
    private CharacterControls cr;

    [SerializeField]
    private GameObject dustlari;

    private void Start()
    {
        self = transform;
        cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("GROUNDED");

            /*   NetworkIdentity playerr = transform.parent.gameObject.GetComponent<NetworkIdentity>();
               NetworkIdentity PP = GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkIdentity>();
               NetworkIdentity dsdsd = GetComponent<NetworkIdentity>();
               ClientInstance cl = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ClientInstance>();
   */
            UI ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
            ui.Dust(self);
            Grounded = true;
            CoroutineAllowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Grounded = false;
            CoroutineAllowed = false;
        }
    }

    private void Update()
    {
        if (Grounded && cr.blend > 0 && CoroutineAllowed)
        {
            Debug.Log("run");
            StartCoroutine("spawndust");
            CoroutineAllowed = false;
        }
        if (cr.blend <= 0 || !Grounded)
        {
            StopCoroutine("spawndust");
            CoroutineAllowed = true;
        }
    }

    private IEnumerator spawndust()
    {
        while (Grounded)
        {
            Instantiate(dustlari, transform.position, dustlari.transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }
    }
}