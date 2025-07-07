using System;

[Serializable]
public class DogBreed
{
    public int id;
    public string name;
    public string bred_for;
    public string breed_group;
    public string life_span;
    public string temperament;
    public string origin;
    public Weight weight;
    public Height height;
    // и другие поля, если нужны
}

[Serializable]
public class Weight
{
    public string imperial;
    public string metric;
}

[Serializable]
public class Height
{
    public string imperial;
    public string metric;
}

[System.Serializable]
public class DogFact
{
    public string name;
    public string description;
}

[System.Serializable]
public class DogFactResponse
{
    public DogFact data;
}
