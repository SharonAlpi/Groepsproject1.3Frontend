using System;

[Serializable]
public class Info
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string NameDocter { get; set; }
    public bool Route { get; set; }
    public DateTime BirthDay { get; set; }
    public int AvatarId { get; set; }
}
