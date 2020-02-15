import { Playlist } from '~/app/types/playlist';
import { Guid } from '~/common/helpers/guid';
import { createRule, createRuleGroup } from '~/app/types/rule.factory';
import { createTreeNodeData } from '~/common/components/TreeView/types/tree.factory';
import { TreeViewData } from '~/common/components/TreeView/types/tree';
import { RuleOrRuleGroup } from '~/app/types/rule';
import {
    addChildNode,
    addNewNode,
} from '~/common/components/TreeView/actions/treeViewData.actions';
import { deafultPlaylistLimit, defaultUpdateType } from '~/app/app.const';

export const createPlaylist = (): Playlist => {
    const newChildNode = createTreeNodeData({
        id: Guid.newGuid(),
        data: createRule(),
    });

    const newParentNode = createTreeNodeData({ id: Guid.newGuid(), data: createRuleGroup() });

    let rulesTree: TreeViewData<RuleOrRuleGroup> = {
        byId: {},
        rootIds: [],
    };

    rulesTree = {
        ...addNewNode(rulesTree, newParentNode),
    };
    rulesTree = {
        ...addChildNode(rulesTree, newParentNode, newChildNode),
    };

    return {
        id: Guid.newGuid(),
        name: '',
        rulesTree: rulesTree,
        limit: deafultPlaylistLimit,
        updateType: defaultUpdateType,
    };
};
