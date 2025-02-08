using System.Collections.Generic;
using System.Xml.Serialization;
using MyGameNamespace;


[XmlRoot("Level")]
public class Level
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("difficulty")]
    public string Difficulty { get; set; }
public List<Barrier> Barriers { get; set; }
    public Background MiddleBackground { get; set; }  
    public Background Background { get; set; }
    public Player Player { get; set; }
    public List<Zone> ObstacleZones { get; set; }
    public List<Platform> Platforms { get; set; }
    public List<Bonus> Bonuses { get; set; }
}

public class Background
{
    [XmlAttribute("image")]
    public string Image { get; set; }
    [XmlAttribute("position")]
    public string Position { get; set; }
}
public class MiddleBackground
{
    [XmlAttribute("image")]
    public string Image { get; set; }

    [XmlAttribute("position")]
    public string Position { get; set; } 
}

public class Player
{
    [XmlAttribute("startPosition")]
    public string StartPosition { get; set; }

    [XmlAttribute("sprite")]
    public string Sprite { get; set; }
}

public class Zone
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlAttribute("minPosition")]
    public string MinPosition { get; set; }

    [XmlAttribute("maxPosition")]
    public string MaxPosition { get; set; }

    [XmlAttribute("count")]
    public int Count { get; set; }
}

public class Platform
{
    [XmlAttribute("sprite")]
    public string Sprite { get; set; }

    [XmlAttribute("position")]
    public string Position { get; set; }

    [XmlAttribute("count")]
    public int Count { get; set; }
}

public class Bonus
{
    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlAttribute("position")]
    public string Position { get; set; }
}

