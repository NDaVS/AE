using UnityEngine;

public class WelcomeRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotationSpeed = 90.0f;

    void Update()
    {
        // Вычисляем угол поворота за кадр
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Создаем вектор поворота
        Vector3 rotationVector = new Vector3(0, rotationAmount, 0);

        // Поворачиваем объект
        transform.Rotate(rotationVector);
    }
}
