using UnityEngine;
using UnityEngine.UIElements;

public class parallax : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        transform.position = new Vector3(cam.position.x, transform.position.y, 30);
    }
}
