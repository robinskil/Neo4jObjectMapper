# Neo4j Object Mapper

Welcome to the wiki for the Neo4j Object Mapper library. This an object mapper designed for the .NetStandard 2.0 framework.
Using this library does require the developer to have some knowledge about Cypher.

## Query a single node
### Example Model
```cs
public class Country
{
    public string CountryID { get; set; }
    public string CountryName { get; set; }
    public ICollection<City> Cities { get; set; }
}
```
The mapper will map all value types to the corresponding properties of the model. Property names are treated case-insensitive.
### Querying a single node
```cs
IDriver Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
INeoContext context = new NeoContext(Driver);
Country result = await context.QueryDefault<Country>("MATCH(n:Country {countryName:'Russia'}) return n");
```

### Querying a single node with parameters
Parameters have to be prefixed with a $ inside the cypher query.
```cs
INeoContext context = new NeoContext(Driver);
var parameters = new Dictionary<string, object>();
parameters.Add("Country", "Russia");
var result = await context.QueryDefault<Country>("MATCH(n:Country {countryName:$Country}) return n",parameters);
```
