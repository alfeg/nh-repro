using NHibernate.Mapping.ByCode;
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Mapping;

namespace nh_shared;

public class AppSessionFactory
{
    public Configuration Configuration { get; }
    public ISessionFactory SessionFactory { get; }

    public AppSessionFactory(string connectionString)
    {
        var mapper = new ModelMapper();
        mapper.AddMapping<ItemMap>();
        mapper.AddMapping<ItemKindAMap>();
        mapper.AddMapping<ItemKindBMap>();
        mapper.AddMapping<ItemKindCMap>();
        mapper.AddMapping<ItemKindDMap>();
        var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

        Configuration = new Configuration();
        Configuration.DataBaseIntegration(db =>
        {
            db.ConnectionString = connectionString;
            db.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
            db.Driver<NHibernate.Driver.SqlClientDriver>();
            db.KeywordsAutoImport = Hbm2DDLKeyWords.None;
        }).AddMapping(domainMapping);
        
        Configuration.SessionFactory().GenerateStatistics();

        SessionFactory = Configuration.BuildSessionFactory();
    }

    public ISession OpenSession()
    {
        return SessionFactory.OpenSession();
    }
}

public class Item
{
    public virtual int Id { get; set; }
    public virtual string Value { get; set; }
    public virtual IList<ItemKindA> KindAs { get; set; } = new List<ItemKindA>();
    public virtual IList<ItemKindB> KindBs { get; set; }
    public virtual IList<ItemKindC> KindCs { get; set; }
    public virtual IList<ItemKindD> KindDs { get; set; }
}

public class ItemKindA
{
    public virtual int Id { get; set; }
    public virtual string Value { get; set; }
    public virtual IList<Item> Items { get; set; } = new List<Item>();
}

public class ItemKindB
{
    public virtual int Id { get; set; }
    public virtual string Value { get; set; }
    public virtual IList<Item> Items { get; set; }
}

public class ItemKindC
{
    public virtual int Id { get; set; }
    public virtual string Value { get; set; }
    public virtual IList<Item> Items { get; set; }
}

public class ItemKindD
{
    public virtual int Id { get; set; }
    public virtual string Value { get; set; }
    public virtual IList<Item> Items { get; set; }
}

public class ItemMap : ClassMapping<Item>
{
    public ItemMap()
    {
        Table("Items");
        Id(x => x.Id, mapper => mapper.Column("Id"));
        Property(x => x.Value);
        Bag(x => x.KindAs,
            map =>
            {
                map.Table("ItemKindsA");
                map.Key(k => k.Column("KindId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("ItemId")));

        Bag(x => x.KindBs,
            map =>
            {
                map.Table("ItemKindsB");
                map.Key(k => k.Column("KindId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("ItemId")));
        Bag(x => x.KindCs,
            map =>
            {
                map.Table("ItemKindsC");
                map.Key(k => k.Column("KindId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("ItemId")));
        Bag(x => x.KindDs,
            map =>
            {
                map.Table("ItemKindsd");
                map.Key(k => k.Column("KindId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("ItemId")));
    }
}

public class ItemKindAMap : ClassMapping<ItemKindA>
{
    public ItemKindAMap()
    {
        Table("KindA");
        Lazy(true);
        Property(x => x.Value);
        Id(x => x.Id, mapper => mapper.Column("ID"));

        this.Bag(x => x.Items,
            map =>
            {
                map.Table("ItemKindsA");
                map.Inverse(true);
                map.Key(k => k.Column("ItemId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("KindId")));
    }
}
public class ItemKindBMap : ClassMapping<ItemKindB>
{
    public ItemKindBMap()
    {
        Table("KindB");
        Lazy(true);
        Property(x => x.Value);
        Id(x => x.Id, mapper => mapper.Column("ID"));

        this.Bag(x => x.Items,
            map =>
            {
                map.Table("ItemKindsB");
                map.Inverse(true);
                map.Key(k => k.Column("ItemId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("KindId")));
    }
}
public class ItemKindCMap : ClassMapping<ItemKindC>
{
    public ItemKindCMap()
    {
        Table("KindC");
        Lazy(true);
        Property(x => x.Value);
        Id(x => x.Id, mapper => mapper.Column("ID"));

        this.Bag(x => x.Items,
            map =>
            {
                map.Table("ItemKindsC");
                map.Inverse(true);
                map.Key(k => k.Column("ItemId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("KindId")));
    }
}
public class ItemKindDMap : ClassMapping<ItemKindD>
{
    public ItemKindDMap()
    {
        Table("KindD");
        Lazy(true);
        Property(x => x.Value);
        Id(x => x.Id, mapper => mapper.Column("ID"));

        this.Bag(x => x.Items,
            map =>
            {
                map.Table("ItemKindsD");
                map.Inverse(true);
                map.Key(k => k.Column("ItemId"));
                map.Lazy(CollectionLazy.Lazy);
            },
            map => map.ManyToMany(m => m.Column("KindId")));
    }
}

public static class FailingQuery
{
    public static void DoQuery(ISessionFactory factory)
    {
        using var session = factory.OpenSession();
        var data = session.Query<Item>()
            .Fetch(a => a.KindAs)    
            .ToList();

        var item = data.First();
        var kindA = item.KindAs.First();
        var kindAItems = kindA.Items.ToList();
    }
}