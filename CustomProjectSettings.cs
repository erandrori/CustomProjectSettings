using UnityEngine;


public class CustomProjectSettings : ScriptableObject
{
    public enum Environment { Publish, Sandbox }

    [SerializeField] private Environment environment = Environment.Sandbox;

    [SerializeField] private bool entityLogEnabled;

    [SerializeField] private bool notificationsLogEnabled;

    public static Environment EnvironmentChoosen { get; private set; }
    public static bool EntityLogEnabled { get; private set; }
    public static bool NotificationsLogEnabled { get; private set; }

    private void OnEnable()
    {
        Refresh(this);
    }

    public static void Refresh(CustomProjectSettings instance)
    {
        EnvironmentChoosen = instance.environment;
        EntityLogEnabled = instance.entityLogEnabled;
        NotificationsLogEnabled = instance.notificationsLogEnabled;
    }
}