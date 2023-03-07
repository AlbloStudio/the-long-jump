using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float minAngle = -20f;
    [SerializeField] private float maxAngle = 20f;

    private Quaternion _sourceRotation;
    private Quaternion _targetRotation;
    private float _timeCount = 0.0f;

    private void Awake()
    {
        _sourceRotation = transform.rotation;
        _targetRotation = GetRandomRotation();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(_sourceRotation, _targetRotation, _timeCount * speed);
        _timeCount += Time.deltaTime;

        if (_timeCount * speed > 1)
        {
            _timeCount = 0;
            _sourceRotation = transform.rotation;
            _targetRotation = GetRandomRotation();
        }
    }

    private Quaternion GetRandomRotation()
    {
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        return Quaternion.Euler(eulerRotation.x, eulerRotation.y, Random.Range(minAngle, maxAngle));
    }
}
