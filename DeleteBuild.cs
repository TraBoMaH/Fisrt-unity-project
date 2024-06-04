using UnityEngine;

public class DeleteBuild : MonoBehaviour
{
    private bool deletionMode = false; // Флаг для отслеживания режима удаления
    private GameObject objectToDelete;
    private LayerMask layer = 1 << 7; // Ссылка на объект для удаления

    void Update()
    {
        // Проверяем, активен ли режим удаления
        if (deletionMode)
        {
            // Проверяем, нажата ли левая кнопка мыши
            if (Input.GetKeyDown(KeyCode.T))
            {
                // Создаем луч из центра экрана
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                // Проверяем, попал ли луч в какой-либо объект
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
                {
                    // Сохраняем попавший объект
                    objectToDelete = hit.collider.gameObject;

                    // Удаляем объект
                    Destroy(objectToDelete);
                }
            }
        }
    }

    // Метод для переключения режима удаления
    public void ToggleDeletionMode()
    {
        deletionMode = !deletionMode;
    }
}
