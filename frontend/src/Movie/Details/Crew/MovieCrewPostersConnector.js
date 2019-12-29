import _ from 'lodash';
import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { createSelector } from 'reselect';
import MovieCrewPosters from './MovieCrewPosters';
import { fetchRootFolders } from 'Store/Actions/rootFolderActions';

function createMapStateToProps() {
  return createSelector(
    (state) => state.moviePeople.items,
    (people) => {
      const crew = _.reduce(people, (acc, person) => {
        if (person.type === 'crew') {
          acc.push(person);
        }

        return acc;
      }, []);

      return {
        crew
      };
    }
  );
}

const mapDispatchToProps = {
  fetchRootFolders
};

class MovieCrewPostersConnector extends Component {

  //
  // Lifecycle

  componentDidMount() {
    this.props.fetchRootFolders();
  }

  //
  // Render

  render() {

    return (
      <MovieCrewPosters
        {...this.props}
      />
    );
  }
}

MovieCrewPostersConnector.propTypes = {
  fetchRootFolders: PropTypes.func.isRequired
};

export default connect(createMapStateToProps, mapDispatchToProps)(MovieCrewPostersConnector);
