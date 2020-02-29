import { Playlist } from '~/app/types/playlist';
import {
    RuleCriteriaDefinition,
    RuleCriteriaOperator,
    ListValueRange,
    ListValue,
} from '~/app/types/rule';
import { AppPlaylistState, AppState } from '~/app/state/app.reducer';
import { AppData, AppPlaylist } from '~/app/types/appData';
import { TreeViewData } from '~/common/components/TreeView/types/tree';

export type AppSelectors = {
    getPlaylists(): Playlist[];
    getEditedPlaylist(): Playlist;
    isNewPlaylist(id: string): boolean;
    getRuleCriteriaOperators(criteriaName: string): RuleCriteriaOperator[];
    getRulesCriteriaDefinitions(): RuleCriteriaDefinition[];
    getRulesCriteriaDefinition(criteriaName: string): RuleCriteriaDefinition;
    getAppData(): AppData;
    getLimitOrdersBy(): string[];
};

export const createAppSelectors = (state: AppState): AppSelectors => {
    return {
        getPlaylists: (): Playlist[] => {
            return state.playlists.names.map(name => state.playlists.byId[name]);
        },
        getEditedPlaylist: (): Playlist => {
            return state.editedPlaylist;
        },
        isNewPlaylist: (id: string): boolean => {
            return !Object.keys(state.playlists.byId).includes(id);
        },
        getRuleCriteriaOperators: (criteriaName: string): RuleCriteriaOperator[] => {
            const ruleCritDef = state.rulesCriteriaDefinitions.find(x => x.name === criteriaName);
            if (ruleCritDef && ruleCritDef.type.operators) {
                return ruleCritDef.type.operators;
            } else {
                return [];
            }
        },
        getRulesCriteriaDefinitions: (): RuleCriteriaDefinition[] => {
            return state.rulesCriteriaDefinitions;
        },
        getRulesCriteriaDefinition: (criteriaName: string): RuleCriteriaDefinition => {
            return state.rulesCriteriaDefinitions.find(x => x.name === criteriaName);
        },
        getAppData: (): AppData => {
            return getAppData(state);
        },
        getLimitOrdersBy: (): string[] => {
            return state.limitOrdersBy;
        },
    };
};

export const getAppData = (state: AppState): AppData => {
    return {
        ...state,
        playlists: convertToAppPlaylists(state.playlists),
    };
};

export const getAppPlaylist = (state: AppState): AppPlaylist => {
    const playlist = state.editedPlaylist;
    return {
        ...playlist,
        rulesTree: getOrderedNodeIds(playlist.rulesTree.rootIds, playlist.rulesTree).map(
            id => playlist.rulesTree.byId[id],
        ),
    };
};

const convertToAppPlaylists = (playlistState: AppPlaylistState): AppPlaylist[] => {
    return playlistState.names
        .map(x => playlistState.byId[x])
        .map(x => ({
            ...x,
            rulesTree: getOrderedNodeIds(x.rulesTree.rootIds, x.rulesTree).map(
                id => x.rulesTree.byId[id],
            ),
        }));
};

const getOrderedNodeIds = (ids: string[], treeViewData: TreeViewData): string[] => {
    const arr: string[] = [];

    ids.forEach(id => {
        arr.push(id);
        const node = treeViewData.byId[id];
        if (node.children.length > 0) {
            getOrderedNodeIds(node.children, treeViewData).map(i => arr.push(i));
        }
    });

    return arr;
};
