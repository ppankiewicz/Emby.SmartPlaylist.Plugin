import * as React from 'react';
import { Playlist } from '~/app/types/playlist';
import { RuleCriteriaDefinition } from '~/app/types/rule';
import { PlaylistAction, playlistReducer } from '~/app/state/playlist/playlist.reducer';
import { normalizeArray } from '~/common/helpers/array';
import { createTreeViewData } from '~/common/components/TreeView/types/tree.factory';
import { AppData } from '~/app/types/appData';
import { deafultPlaylistLimit } from '~/app/app.const';

export type AppPlaylistState = {
    byId: {
        [Key: string]: Playlist;
    };
    names: string[];
};

export type AppState = {
    appId: string;
    playlists: AppPlaylistState;
    rulesCriteriaDefinitions: RuleCriteriaDefinition[];
    limitOrdersBy: string[];
    editedPlaylist?: Playlist;
};

export const initAppState: AppState = {
    appId: '',
    playlists: {
        byId: {},
        names: [],
    },
    rulesCriteriaDefinitions: [],
    limitOrdersBy: [],
    editedPlaylist: undefined,
};

export type AppAction =
    | { type: 'app:loadSettings'; settings: AppData }
    | { type: 'app:addNewPlaylist'; playlist: Playlist }
    | { type: 'app:editPlaylist'; playlist: Playlist }
    | { type: 'app:discardPlaylist' }
    | { type: 'app:savePlaylist' }
    | { type: 'app:removePlaylist'; playlist: Playlist };

export const appReducer: React.Reducer<AppState, AppAction | PlaylistAction> = (state, action) => {
    switch (action.type) {
        case 'app:loadSettings': {
            const playlists = action.settings.playlists.map(x => ({
                limit: deafultPlaylistLimit,
                ...x,
                rulesTree: createTreeViewData(x.rulesTree),
            }));

            return {
                ...action.settings,
                playlists: {
                    byId: normalizeArray(playlists, 'id'),
                    names: playlists.map(x => x.id),
                },
            };
        }
        case 'app:addNewPlaylist': {
            return {
                ...state,
                editedPlaylist: { ...action.playlist },
            };
        }
        case 'app:editPlaylist': {
            return {
                ...state,
                editedPlaylist: { ...state.playlists.byId[action.playlist.id] },
            };
        }
        case 'app:discardPlaylist': {
            return {
                ...state,
                editedPlaylist: undefined,
            };
        }
        case 'app:removePlaylist': {
            const { [action.playlist.id]: deleted, ...byId } = state.playlists.byId;
            return {
                ...state,
                playlists: {
                    ...state.playlists,
                    byId: byId,
                    names: state.playlists.names.filter(x => x !== action.playlist.id),
                },
            };
        }
        case 'app:savePlaylist': {
            let names = state.playlists.names;
            if (!names.includes(state.editedPlaylist.id)) {
                names = [...names, state.editedPlaylist.id];
            }
            return {
                ...state,
                playlists: {
                    ...state.playlists,
                    byId: {
                        ...state.playlists.byId,
                        [state.editedPlaylist.id]: {
                            ...state.editedPlaylist,
                        },
                    },
                    names: names,
                },
                editedPlaylist: undefined,
            };
        }

        default: {
            return {
                ...state,
                editedPlaylist: {
                    ...state.editedPlaylist,
                    ...playlistReducer(state.editedPlaylist, action as PlaylistAction),
                },
            };
        }
    }
};
