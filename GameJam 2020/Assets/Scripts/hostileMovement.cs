using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hostileMovement : MonoBehaviour
{
    public float speedX, speedY, speedZ;
    public float rotateX, rotateY, rotateZ;
    public Transform Player;
    public float dist = 6f;
    [SerializeField] private float movementSpeed = 3f;

    [SerializeField] private MeshRenderer hostileMesh;
    private MaterialPropertyBlock materialBlock;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.motor.transform;
        materialBlock = new MaterialPropertyBlock();

        rotateX = Random.Range(-1f, 1f);
        rotateY = Random.Range(-1f, 1f);
        rotateZ = Random.Range(-1f, 1f);

        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        speedX = Random.Range(-5f, 5f) * Time.deltaTime;
        speedY = Random.Range(-5f, 5f) * Time.deltaTime;
        speedZ = Random.Range(-5f, 5f) * Time.deltaTime;
        transform.position += new Vector3(speedX, speedY, speedZ);
        transform.eulerAngles += new Vector3(rotateX, rotateY, rotateZ);
    }

    private IEnumerator Flash()
    {
        while (true)
        {
            hostileMesh.GetPropertyBlock(materialBlock);
            materialBlock.SetColor("_EmissionColor", Color.white * 2);
            hostileMesh.SetPropertyBlock(materialBlock);

            yield return new WaitForSeconds(0.5f);

            hostileMesh.GetPropertyBlock(materialBlock);
            materialBlock.SetColor("_EmissionColor", Color.white / 2);
            hostileMesh.SetPropertyBlock(materialBlock);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
