import { RuleOrRuleGroup } from '~/app/types/rule';
import { TreeViewData } from '~/common/components/TreeView/types/tree';
import { PlaylistState } from '~/app/state/playlist/playlist.reducer';
import { isShuffleUpdateType, PlaylistBasicData } from '~/app/types/playlist';

export type PlaylistSelectors = {
    getRulesTree(): TreeViewData<RuleOrRuleGroup>;
    getBasicData(): PlaylistBasicData;
    isShuffleUpdateType(): boolean;
};

export const createPlaylistSelectors = (state: PlaylistState): PlaylistSelectors => {
    return {
        getRulesTree: (): TreeViewData<RuleOrRuleGroup> => {
            return state.rulesTree;
        },
        getBasicData: (): PlaylistBasicData => {
            const { rulesTree, ...basicData } = state;
            return basicData;
        },
        isShuffleUpdateType: (): boolean => {
            return isShuffleUpdateType(state.updateType);
        },
    };
};
