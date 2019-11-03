import { createAction } from 'redux-actions';
import { batchActions } from 'redux-batched-actions';
import createAjaxRequest from 'Utilities/createAjaxRequest';
import { createThunk, handleThunks } from 'Store/thunks';
import createHandleActions from './Creators/createHandleActions';
import { set, update } from './baseActions';

//
// Variables

export const section = 'moviePeople';

//
// State

export const defaultState = {
  isFetching: false,
  isPopulated: false,
  error: null,
  items: []
};

//
// Actions Types

export const FETCH_MOVIE_PEOPLE = 'moviePeople/fetchMoviePeople';
export const CLEAR_MOVIE_PEOPLE = 'moviePeople/clearMoviePeople';

//
// Action Creators

export const fetchMoviePeople = createThunk(FETCH_MOVIE_PEOPLE);
export const clearMoviePeople = createAction(CLEAR_MOVIE_PEOPLE);

//
// Action Handlers

export const actionHandlers = handleThunks({

  [FETCH_MOVIE_PEOPLE]: function(getState, payload, dispatch) {
    dispatch(set({ section, isFetching: true }));

    const promise = createAjaxRequest({
      url: '/person',
      data: payload
    }).request;

    promise.done((data) => {
      dispatch(batchActions([
        update({ section, data }),

        set({
          section,
          isFetching: false,
          isPopulated: true,
          error: null
        })
      ]));
    });

    promise.fail((xhr) => {
      dispatch(set({
        section,
        isFetching: false,
        isPopulated: false,
        error: xhr
      }));
    });
  }
});

//
// Reducers

export const reducers = createHandleActions({

  [CLEAR_MOVIE_PEOPLE]: (state) => {
    return Object.assign({}, state, defaultState);
  }

}, defaultState, section);
