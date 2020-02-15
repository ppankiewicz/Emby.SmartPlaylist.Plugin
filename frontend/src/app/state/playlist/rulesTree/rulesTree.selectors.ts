import { TreeNodeData, TreeViewData } from '~/common/components/TreeView/types/tree';

export type RulesTreeSelectors = {
    isRemovable(node: TreeNodeData): boolean;
    canAddRule(node: TreeNodeData): boolean;
};

const findParentNode = (tree: TreeViewData, id: string) => {
    return Object.values(tree.byId).find(x => x.children.includes(id));
};

const isRemovable = (tree: TreeViewData, node: TreeNodeData) => {
    if (tree.rootIds.length === 1) {
        if (node.isRoot) {
            return false;
        }

        const parent = findParentNode(tree, node.id);
        if (parent) {
            if (parent.children.length > 1) {
                return true;
            } else {
                return isRemovable(tree, parent);
            }
        }
    }

    return true;
};

export const createRulesTreeSelectors = (tree: TreeViewData): RulesTreeSelectors => {
    return {
        isRemovable: (node: TreeNodeData) => {
            return isRemovable(tree, node);
        },
        canAddRule: (node: TreeNodeData) => {
            return !node.isRoot;
        },
    };
};
