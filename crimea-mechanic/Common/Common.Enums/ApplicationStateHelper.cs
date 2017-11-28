namespace Common.Enums
{
    public static class ApplicationStateHelper
    {
        public static string GetDescription(this ApplicationState state)
        {
            switch (state)
            {
                case ApplicationState.InSearch:
                    return "В поиске автосервиса";
                case ApplicationState.InProcessing:
                    return "На исполнении";
                case ApplicationState.Completed:
                    return "Завершена";
                case ApplicationState.Rejected:
                    return "Отклонена";
                case ApplicationState.Deleted:
                    return "Удалена";
                default:
                    return "Неизвестное состояние";
            }
        }
    }
}
