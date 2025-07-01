using UnityEngine;

public static class SaveData
{
    private const string VidaExtraKey = "vidasExtraPermanente";

    public static int GetVidasExtra()
    {
        return PlayerPrefs.GetInt(VidaExtraKey, 0);
    }

    public static void AñadirVidaExtra()
    {
        int nuevas = GetVidasExtra() + 1;
        PlayerPrefs.SetInt(VidaExtraKey, nuevas);
        PlayerPrefs.Save();
        Debug.Log($"🎁 ¡Vida extra permanente añadida! Total: {nuevas}");
    }

    public static void ResetearVidasExtra()
    {
        PlayerPrefs.DeleteKey(VidaExtraKey);
    }
}