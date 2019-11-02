using System.Collections.Generic;
using NzbDrone.Core.Datastore;

namespace NzbDrone.Core.Movies.People
{
    public class Person : ModelBase
    {
        public Person()
        {
            Images = new List<MediaCover.MediaCover>();
        }

        public string Name { get; set; }
        public int TmdbId { get; set; }
        public int MovieId { get; set; }
        public List<MediaCover.MediaCover> Images { get; set; }
        public string CrewDepartment { get; set; }
        public string CrewJob { get; set; }
        public string CastCharacter { get; set; }
        public int Order { get; set; }
        public PersonType Type { get; set; }
    }

    public enum PersonType
    {
        Cast,
        Crew
    }
}
