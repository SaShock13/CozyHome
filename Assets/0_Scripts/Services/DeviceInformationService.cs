using UnityEngine;

public static class DeviceInformationService
{
    public static bool IsTablet(out float diagonalInches , out float dpi)
    {
        dpi = Screen.dpi;
        float widthInches = Screen.width / dpi;
        float heightInches = Screen.height / dpi;
        diagonalInches = Mathf.Sqrt(widthInches * widthInches + heightInches * heightInches);

        bool isTablet = diagonalInches >= 6.5f; // 6.5-7 дюймов как минимум для планшета

        return isTablet;
    }
}
