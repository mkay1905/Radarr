using System.Collections.Generic;
using NzbDrone.Core.Datastore;
using NzbDrone.Core.Messaging.Events;

namespace NzbDrone.Core.Movies.People
{
    public interface IPersonRepository : IBasicRepository<Person>
    {
        List<Person> FindByMovieId(int movieId);
    }

    public class PersonRepository : BasicRepository<Person>, IPersonRepository
    {
        public PersonRepository(IMainDatabase database, IEventAggregator eventAggregator)
            : base(database, eventAggregator)
        {
        }

        public List<Person> FindByMovieId(int movieId)
        {
            return Query(x => x.MovieId == movieId);
        }
    }
}
