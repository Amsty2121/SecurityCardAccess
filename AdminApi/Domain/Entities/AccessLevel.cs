namespace Domain.Entities
{
    public enum AccessLevel
    {
        Blocked = -1,
        Guest,
        Employee,
        TechnicalEmployee,
        Administrator,
        God
    }
}
