using System;
using System.Collections.Generic;
using NzbDrone.Core.Messaging.Events;
using NzbDrone.Core.Movies;
using NzbDrone.Core.Movies.People;
using Radarr.Http;

namespace Radarr.Api.V3.People
{
    public class PersonModule : RadarrRestModule<PersonResource>
    {
        private readonly IPersonService _personService;
        private readonly IMovieService _movieService;
        private readonly IEventAggregator _eventAggregator;

        public PersonModule(IPersonService personService, IMovieService movieService, IEventAggregator eventAggregator)
            : base("/person")
        {
            _personService = personService;
            _movieService = movieService;
            _eventAggregator = eventAggregator;

            GetResourceById = GetPerson;
            GetResourceAll = GetPeople;
        }

        private PersonResource GetPerson(int id)
        {
            return _personService.GetById(id).ToResource();
        }

        private List<PersonResource> GetPeople()
        {
            var movieIdQuery = Request.Query.MovieId;

            if (movieIdQuery.HasValue)
            {
                int movieId = Convert.ToInt32(movieIdQuery.Value);

                return _personService.GetAllPeopleForMovie(movieId).ToResource();
            }

            return _personService.GetAllPeople().ToResource();
        }
    }
}
