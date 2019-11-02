using System.Collections.Generic;
using System.Linq;
using NzbDrone.Core.MediaCover;
using NzbDrone.Core.Movies.People;
using Radarr.Http.REST;

namespace Radarr.Api.V3.People
{
    public class PersonResource : RestResource
    {
        public PersonResource()
        {
        }

        public string PersonName { get; set; }
        public int TmdbId { get; set; }
        public int MovieId { get; set; }
        public List<MediaCover> Images { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
        public PersonType Type { get; set; }
    }

    public static class AlternativeTitleResourceMapper
    {
        public static PersonResource ToResource(this Person model)
        {
            if (model == null)
            {
                return null;
            }

            return new PersonResource
            {
                Id = model.Id,
                MovieId = model.MovieId,
                TmdbId = model.TmdbId,
                PersonName = model.Name,
                Order = model.Order,
                Character = model.CastCharacter,
                Department = model.CrewDepartment,
                Images = model.Images,
                Job = model.CrewJob,
                Type = model.Type
            };
        }

        public static Person ToModel(this PersonResource resource)
        {
            if (resource == null)
            {
                return null;
            }

            return new Person
            {
                Id = resource.Id,
                MovieId = resource.MovieId,
                Name = resource.PersonName,
                Order = resource.Order,
                CastCharacter = resource.Character,
                CrewDepartment = resource.Department,
                CrewJob = resource.Job,
                Type = resource.Type,
                Images = resource.Images,
                TmdbId = resource.TmdbId
            };
        }

        public static List<PersonResource> ToResource(this IEnumerable<Person> people)
        {
            return people.Select(ToResource).ToList();
        }
    }
}
