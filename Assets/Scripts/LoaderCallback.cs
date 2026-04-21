using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Выполняет переход из загрузочной сцены в целевую сцену.
/// </summary>
public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    /// <summary>
    /// Один раз вызывает callback загрузчика после первого кадра.
    /// </summary>
    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;

            Loader.LoaderCallback();
        }
    }
}
