using Application.Common.Interfaces;

namespace Infrastructure_Dapper.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

