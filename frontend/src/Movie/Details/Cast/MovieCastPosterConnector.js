import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import MovieCastPoster from './MovieCastPoster';
import createMoviePersonListSelector from 'Store/Selectors/createMoviePersonListSelector';
import { selectNetImportSchema, setNetImportValue, setNetImportFieldValue } from 'Store/Actions/settingsActions';

function createMapStateToProps() {
  return createMoviePersonListSelector();
}

const mapDispatchToProps = {
  selectNetImportSchema,
  setNetImportFieldValue,
  setNetImportValue
};

class MovieCastPosterConnector extends Component {

  //
  // Listeners

  onNetImportSelect = () => {
    this.props.selectNetImportSchema({ implementation: 'TMDbPersonImport', presetName: undefined });
    this.props.setNetImportFieldValue({ name: 'personId', value: this.props.tmdbId.toString() });
    this.props.setNetImportValue({ name: 'name', value: `${this.props.personName} - ${this.props.tmdbId}` });
  }

  //
  // Render

  render() {
    const {
      tmdbId,
      personName
    } = this.props;

    return (
      <MovieCastPoster
        {...this.props}
        tmdbId={tmdbId}
        personName={personName}
        onNetImportSelect={this.onNetImportSelect}
      />
    );
  }
}

MovieCastPosterConnector.propTypes = {
  tmdbId: PropTypes.number.isRequired,
  personName: PropTypes.string.isRequired,
  selectNetImportSchema: PropTypes.func.isRequired,
  setNetImportFieldValue: PropTypes.func.isRequired,
  setNetImportValue: PropTypes.func.isRequired
};

export default connect(createMapStateToProps, mapDispatchToProps)(MovieCastPosterConnector);
