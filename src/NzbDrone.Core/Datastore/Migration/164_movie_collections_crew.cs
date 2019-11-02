using FluentMigrator;
using NzbDrone.Core.Datastore.Migration.Framework;

namespace NzbDrone.Core.Datastore.Migration
{
    [Migration(164)]
    public class movie_collections_crew : NzbDroneMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("Movies").AddColumn("Collection").AsString().Nullable();
            Delete.Column("Actors").FromTable("Movies");

            Create.TableForModel("People").WithColumn("MovieId").AsInt32()
                                  .WithColumn("TmdbId").AsInt32()
                                  .WithColumn("Name").AsString()
                                  .WithColumn("Images").AsString()
                                  .WithColumn("CastCharacter").AsString().Nullable()
                                  .WithColumn("Order").AsInt32()
                                  .WithColumn("CrewJob").AsString().Nullable()
                                  .WithColumn("CrewDepartment").AsString().Nullable()
                                  .WithColumn("Type").AsInt32();

            Create.Index().OnTable("People").OnColumn("MovieId");
        }
    }
}
