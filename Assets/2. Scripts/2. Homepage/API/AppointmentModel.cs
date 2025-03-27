using System;

[Serializable]
public class Appointment
{
    public Guid Id;
    public string Name;
    public DateTime Date;
    public int StickerId;
    public Guid UserId;
}