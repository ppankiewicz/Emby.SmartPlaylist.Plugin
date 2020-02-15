import { TreeNodeData, TreeViewData } from '~/common/components/TreeView/types/tree';
import { addItemAt } from '~/common/helpers/array';

export const getParentNode = (treeData: TreeViewData, node: TreeNodeData) => {
    return Object.values(treeData.byId).filter(x => x.children.includes(node.id))[0];
};

export const addChildNode = (
    tree: TreeViewData,
    parentNode: TreeNodeData,
    newNode: TreeNodeData,
    contextNode: TreeNodeData = undefined,
) => {
    const contextNodeId = (contextNode && contextNode.id) || '';
    const currentItemIndex = parentNode.children.indexOf(contextNodeId);

    newNode.level = parentNode.level + 1;

    return {
        ...tree,
        byId: {
            ...tree.byId,
            [parentNode.id]: {
                ...parentNode,
                children: addItemAt(parentNode.children, newNode.id, currentItemIndex + 1),
            },
            [newNode.id]: newNode,
        },
    };
};

export const addRootNode = (tree: TreeViewData, newNode: TreeNodeData, node?: TreeNodeData) => {
    let currentItemIndex = 0;
    if (node) {
        currentItemIndex = tree.rootIds.indexOf(node.id);
    }
    newNode.isRoot = true;
    return {
        ...tree,
        byId: {
            ...tree.byId,
            [newNode.id]: newNode,
        },
        rootIds: addItemAt(tree.rootIds, newNode.id, currentItemIndex + 1),
    };
};

export const addNewNode = (tree: TreeViewData, newNode: TreeNodeData) => {
    return addRootNode(tree, newNode);
};

export const addNode = (tree: TreeViewData, node: TreeNodeData, newNode: TreeNodeData) => {
    if (node.isRoot) {
        return addRootNode(tree, newNode, node);
    } else {
        const parentNode = getParentNode(tree, node);
        return addChildNode(tree, parentNode, newNode, node);
    }
};

export const removeNode = (tree: TreeViewData, node: TreeNodeData) => {
    const { [node.id]: deleted, ...byId } = tree.byId;

    if (node.isRoot) {
        return {
            ...tree,
            ...byId,
            rootIds: tree.rootIds.filter(x => x !== node.id),
        };
    } else {
        const parentNode = getParentNode(tree, node);
        const removeParent = parentNode.children.length === 1;
        const treeWithtoutNode = {
            ...tree,
            ...byId,
            ...removeChildNode(tree, parentNode, node),
        };

        if (removeParent) {
            return {
                ...treeWithtoutNode,
                ...removeNode(treeWithtoutNode, parentNode),
            };
        }

        return {
            ...treeWithtoutNode,
        };
    }
};

export const removeChildNode = (
    tree: TreeViewData,
    parentNode: TreeNodeData,
    node: TreeNodeData,
) => {
    return {
        byId: {
            ...tree.byId,
            [parentNode.id]: {
                ...parentNode,
                children: parentNode.children.filter(x => x !== node.id),
            },
        },
    };
};

export const changeExpand = (
    tree: TreeViewData,
    node: TreeNodeData,
    changedIsExpanded: boolean,
) => {
    return {
        ...tree,
        byId: {
            ...tree.byId,
            [node.id]: {
                ...node,
                isExpanded: changedIsExpanded,
            },
        },
    };
};
