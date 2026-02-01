public static class DeviceLayout
{
    public static int GetColumnCount(float viewportWidth)
    {
        // условно
        if (viewportWidth >= 900f)
            return 3; // планшет
        else
            return 2; // смартфон
    }
}