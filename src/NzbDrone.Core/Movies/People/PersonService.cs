using System.Collections.Generic;
using System.Linq;
using NzbDrone.Core.Messaging.Events;
using NzbDrone.Core.Movies.Events;

namespace NzbDrone.Core.Movies.People
{
    public interface IPersonService
    {
        List<Person> GetAllPeopleForMovie(int movieId);
        Person AddPerson(Person title, Movie movie);
        List<Person> AddPeople(List<Person> titles, Movie movie);
        Person GetById(int id);
        List<Person> GetAllPeople();
        List<Person> UpdatePeople(List<Person> titles, Movie movie);
    }

    public class PersonService : IPersonService, IHandleAsync<MovieDeletedEvent>
    {
        private readonly IPersonRepository _personRepo;

        public PersonService(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        public List<Person> GetAllPeopleForMovie(int movieId)
        {
            return _personRepo.FindByMovieId(movieId).ToList();
        }

        public Person AddPerson(Person title, Movie movie)
        {
            title.MovieId = movie.Id;
            return _personRepo.Insert(title);
        }

        public List<Person> AddPeople(List<Person> titles, Movie movie)
        {
            titles.ForEach(t => t.MovieId = movie.Id);
            _personRepo.InsertMany(titles);
            return titles;
        }

        public Person GetById(int id)
        {
            return _personRepo.Get(id);
        }

        public List<Person> GetAllPeople()
        {
            return _personRepo.All().ToList();
        }

        public void RemoveTitle(Person title)
        {
            _personRepo.Delete(title);
        }

        public List<Person> UpdatePeople(List<Person> people, Movie movie)
        {
            int movieId = movie.Id;

            // First update the movie ids so we can correlate them later.
            people.ForEach(t => t.MovieId = movieId);

            // Now find titles to delete, update and insert.
            var existingPeople = _personRepo.FindByMovieId(movieId);

            var insert = people.Where(t => !existingPeople.Contains(t));
            var update = existingPeople.Where(t => people.Contains(t));
            var delete = existingPeople.Where(t => !people.Contains(t));

            _personRepo.DeleteMany(delete.ToList());
            _personRepo.UpdateMany(update.ToList());
            _personRepo.InsertMany(insert.ToList());

            return people;
        }

        public void HandleAsync(MovieDeletedEvent message)
        {
            var title = GetAllPeopleForMovie(message.Movie.Id);
            _personRepo.DeleteMany(title);
        }
    }
}
