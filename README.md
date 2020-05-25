# Neo4j Object Mapper

Welcome to the wiki for the Neo4j Object Mapper library. This an object mapper based on .NET Standard 2.0 .
Using this library does require the developer to have some knowledge about Cypher.

## Query a single node
### Example Models
```cs
public class Person
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IList<Owns> Owns { get; set; }
}
//In neo4j this will be our relation between person and house. So a person will own a house.
public class Owns
{
    public DateTime OwnedFrom { get; set; }
    public DateTime OwnedTill { get; set; }
    public House House { get; set; }
}

public class House
{
    public string Address { get; set; }
    public int Age { get; set; }
}
```
The mapper will map all value types to the corresponding properties of the model. Property names are treated case-insensitive.
### Querying a single node
```cs
IDriver Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
INeoContext context = new NeoContext(Driver);
//Query default will return the first record it gets from the neo4j database.
Person person = await context.QueryDefault<Person>("MATCH (p:Person) RETURN p");
```

### Querying a single node with parameters
Parameters have to be prefixed with a $ inside the cypher query.
```cs
IDriver Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
INeoContext context = new NeoContext(Driver);

Guid personGuid = Guid.NewGuid();
var parameters = new Dictionary<string, object>();
parameters.Add("p1", "neo");
//As guids aren't supported by neo4j you will have to stringify it when using it as a parameter. 
//However, models can have guid properties as the library converts the string to a guid
parameters.Add("p2", personGuid.ToString());
Person resultPerson = await context.QueryDefault<Person>("MATCH (p:Person { Name: $p1 , Id: $p2 }) RETURN p",parameters);
```

### Query a single node and his relations with other nodes
The mapping function is very simmiliar to how it's used in Dapper.
```cs
IDriver Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
INeoContext context = new NeoContext(Driver);
Dictionary<Guid, Person> personContainer = new Dictionary<Guid, Person>();
Person personResult = await context.QueryDefaultIncludeable<Person, Owns, House>("MATCH (p:Person { Name: 'neo' })-[o:Owns]->(h:House) return p,o,h",
    (person, owns, house) =>
    {
        if (!personContainer.ContainsKey(person.Id))
        {
            personContainer.Add(person.Id, person);
            person.Owns = new List<Owns>();
        }
        personContainer[p.Id].Owns.Add(o);
        owns.House = house;
        return personContainer[p.Id];
    }
);
```

### Querying multiple nodes
```cs
IDriver Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
INeoContext context = new NeoContext(Driver);
IEnumerable<Person> personsResult = await context.QueryMultiple<Person>("MATCH (p:Person) return p");
```


### Inserting a node
```cs
IDriver Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
Person person = new Person()
{
    Age = 50,
    DateOfBirth = DateTime.Now.AddYears(-50),
    Id = Guid.NewGuid(),
    Name = "neo",
    Salary = 5400.77,
};
INeoContext context = new NeoContext(Driver);
IResultSummary resultExecuting = await context.InsertNode<Person>(person);
```
