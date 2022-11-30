using FluentMigrator;

namespace nh_migration;

[Migration(202230111130)]
public class PrepareDatabaseMigration : FluentMigrator.AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Items")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Value").AsString();

        Create.Table("KindA")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Value").AsString();
        Create.Table("KindB")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Value").AsString();
        Create.Table("KindC")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Value").AsString();
        Create.Table("KindD")
            .WithColumn("Id").AsInt32().PrimaryKey()
            .WithColumn("Value").AsString();

        Create.Table("ItemKindsA")
            .WithColumn("ItemId").AsInt32().NotNullable()
            .WithColumn("KindId").AsInt32().NotNullable();
        Create.Table("ItemKindsB")
            .WithColumn("ItemId").AsInt32().NotNullable()
            .WithColumn("KindId").AsInt32().NotNullable();
        Create.Table("ItemKindsC")
            .WithColumn("ItemId").AsInt32().NotNullable()
            .WithColumn("KindId").AsInt32().NotNullable();
        Create.Table("ItemKindsD")
            .WithColumn("ItemId").AsInt32().NotNullable()
            .WithColumn("KindId").AsInt32().NotNullable();

        Create.ForeignKey("ItemKindA_FK1")
            .FromTable("ItemKindsA").ForeignColumn("ItemId")
            .ToTable("Items").PrimaryColumn("Id");
        Create.ForeignKey("ItemKindA_FK2")
            .FromTable("ItemKindsA").ForeignColumn("KindId")
            .ToTable("KindA").PrimaryColumn("Id");

        Create.ForeignKey("ItemKindB_FK1")
            .FromTable("ItemKindsB").ForeignColumn("ItemId")
            .ToTable("Items").PrimaryColumn("Id");
        Create.ForeignKey("ItemKindB_FK2")
            .FromTable("ItemKindsB").ForeignColumn("KindId")
            .ToTable("KindA").PrimaryColumn("Id");

        Create.ForeignKey("ItemKindC_FK1")
            .FromTable("ItemKindsC").ForeignColumn("ItemId")
            .ToTable("Items").PrimaryColumn("Id");
        Create.ForeignKey("ItemKindC_FK2")
            .FromTable("ItemKindsC").ForeignColumn("KindId")
            .ToTable("KindA").PrimaryColumn("Id");

        Create.ForeignKey("ItemKindD_FK1")
            .FromTable("ItemKindsD").ForeignColumn("ItemId")
            .ToTable("Items").PrimaryColumn("Id");
        Create.ForeignKey("ItemKindD_FK2")
            .FromTable("ItemKindsD").ForeignColumn("KindId")
            .ToTable("KindA").PrimaryColumn("Id");

        Insert.IntoTable("Items").Row(new {Id = 1, Value = "Test"});
        
        Insert.IntoTable("KindA").Row(new {Id = 1, Value = "KindA"});
        Insert.IntoTable("KindB").Row(new { Id = 1, Value = "KindB" });
        Insert.IntoTable("KindC").Row(new { Id = 1, Value = "KindC" });
        Insert.IntoTable("KindD").Row(new { Id = 1, Value = "KindD" });

        Insert.IntoTable("ItemKindsA").Row(new {ItemId = 1, KindId = 1});
        Insert.IntoTable("ItemKindsB").Row(new { ItemId = 1, KindId = 1 });
        Insert.IntoTable("ItemKindsC").Row(new { ItemId = 1, KindId = 1 });
        Insert.IntoTable("ItemKindsD").Row(new { ItemId = 1, KindId = 1 });
    }
}
