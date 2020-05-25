using Neo4j.Driver;
using Neo4jObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NeoObjectMapperTests
{
    public class TestCustomTypes : TestEngine
    {
        //[Fact]
        //public void TestConversionFromDic()
        //{
        //    Neo4jCustomTypeConverter typeConverter = new Neo4jCustomTypeConverter();
        //    IReadOnlyDictionary<string, object> properties = new Dictionary<string, object>()
        //    {
        //        { "A" , 5 },
        //        { "B" , long.Parse("555") },
        //        { "C" , 4700.32 },
        //        { "D", 32.7F },
        //        { "E", "con" },
        //        { "F", true },
        //        { "g", Guid.NewGuid().ToString() },
        //        { "h", new LocalDateTime(DateTime.Now.AddYears(-26)) },
        //        { "i", new LocalDate(DateTime.Now.AddYears(-26)) },
        //        { "j", new OffsetTime(DateTime.Now.AddYears(-26),TimeSpan.FromSeconds(66)) },
        //        { "k", new LocalTime(DateTime.Now.AddYears(-26)) },
        //        { "l", new ZonedDateTime(new DateTimeOffset(DateTime.Now.AddYears(-26))) },
        //        { "m", new LocalDateTime(DateTime.Now.AddYears(-26)) },
        //        { "n", new Duration(50) },
        //        { "o", new Point(2,2,2) },
        //        { "p", new byte[]{ 6 } },
        //        { "q", new List<object>{ 7 } },
        //        { "r", new Dictionary<string,object>{ { "val" , 8 } } },
        //    };
        //    var result = typeConverter.ConvertPropertiesTo<TypeTestClass>(properties);
        //    Assert.True(DicIsTestClass(result, properties));
        //}
        //private bool DicIsTestClass(TypeTestClass tc, IReadOnlyDictionary<string, object> properties)
        //{
        //    return
        //        tc.A.Equals(properties["A"]) &&
        //        tc.b.Equals(properties["B"]) &&
        //        tc.C.Equals(properties["C"]) &&
        //        tc.d.Equals(properties["D"]) &&
        //        tc.e.Equals(properties["E"]) &&
        //        tc.f.Equals(properties["F"]) &&
        //        tc.g.Equals(Guid.Parse((string)properties["g"])) &&
        //        tc.h.Equals((properties["h"] as LocalDateTime).ToDateTime()) &&
        //        tc.i.Equals(properties["i"]) &&
        //        tc.j.Equals(properties["j"]) &&
        //        tc.k.Equals(properties["k"]) &&
        //        tc.l.Equals(properties["l"]) &&
        //        tc.m.Equals(properties["m"]) &&
        //        tc.n.Equals(properties["n"]) &&
        //        tc.o.Equals(properties["o"]) &&
        //        tc.p.Equals(properties["p"]) &&
        //        tc.q.Equals(properties["q"]) &&
        //        tc.r.Equals(properties["r"]);
        //}

        //private class TypeTestClass
        //{
        //    public int A { get; set; }
        //    public long b { get; set; }
        //    public double C { get; set; }
        //    public float d { get; set; }
        //    public string e { get; set; }
        //    public bool f { get; set; }
        //    public Guid g { get; set; }
        //    public DateTime h { get; set; }
        //    public LocalDate i { get; set; }
        //    public OffsetTime j { get; set; }
        //    public LocalTime k { get; set; }
        //    public ZonedDateTime l { get; set; }
        //    public LocalDateTime m { get; set; }
        //    public Duration n { get; set; }
        //    public Point o { get; set; }
        //    public byte[] p { get; set; }
        //    public IList<object> q { get; set; }
        //    public IDictionary<string, object> r { get; set; }
        //}
    }
}
