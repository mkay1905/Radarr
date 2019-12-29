import _ from 'lodash';
import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { createSelector } from 'reselect';
import MovieCastPosters from './MovieCastPosters';
import { fetchRootFolders } from 'Store/Actions/rootFolderActions';

function createMapStateToProps() {
  return createSelector(
    (state) => state.moviePeople.items,
    (people) => {
      const cast = _.reduce(people, (acc, person) => {
        if (person.type === 'cast') {
          acc.push(person);
        }

        return acc;
      }, []);

      return {
        cast
      };
    }
  );
}

const mapDispatchToProps = {
  fetchRootFolders
};

class MovieCastPostersConnector extends Component {

  //
  // Lifecycle

  componentDidMount() {
    this.props.fetchRootFolders();
  }

  //
  // Render

  render() {

    return (
      <MovieCastPosters
        {...this.props}
      />
    );
  }
}

MovieCastPostersConnector.propTypes = {
  fetchRootFolders: PropTypes.func.isRequired
};

export default connect(createMapStateToProps, mapDispatchToProps)(MovieCastPostersConnector);
