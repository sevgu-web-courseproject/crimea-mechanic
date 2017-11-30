namespace Common.Enums
{
    public static class FuelTypeHelper
    {
        public static string GetDescription(this FuelType type)
        {
            switch (type)
            {
                case FuelType.Benzine:
                    return "Бензин";
                case FuelType.Diesel:
                    return "Дизель";
                case FuelType.Another:
                    return "Альтернативный вид топлива";
                default:
                    return "Неизвестный хомосамиенсам";
            }
        }
    }
}
