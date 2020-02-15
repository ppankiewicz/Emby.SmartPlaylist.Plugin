import * as React from 'react';
import { Override, useOverrides } from '~/common/hooks/useOverrides';
import { TreeNode, TreeNodeProps } from '~/common/components/TreeView/TreeNode';
import { TreeNodeData, TreeViewData } from '~/common/components/TreeView/types/tree';

type TreeViewProps = {
    data: TreeViewData;
    overrides?: {
        Node: Override<TreeNodeProps>;
    };
    onExpandedChange?(nodeData: TreeNodeData, isExpanded: boolean): void;
    renderNodeContent(data: TreeNodeData): React.ReactNode;
};

export const TreeView: React.FC<TreeViewProps> = props => {
    const [Node, nodeProps] = useOverrides(props.overrides && props.overrides.Node, TreeNode);

    const { data } = props;
    const getRootNodes = (): TreeNodeData[] => {
        return data.rootIds.map(x => data.byId[x]);
    };

    const getChildNodes = (nodeData: TreeNodeData): TreeNodeData[] => {
        return nodeData.children.map(x => data.byId[x]);
    };

    const setExpanded = (nodeData: TreeNodeData, isExpanded: boolean): void => {
        props.onExpandedChange(nodeData, isExpanded);
    };

    return (
        <>
            {getRootNodes().map(nodeData => (
                <Node
                    key={nodeData.id}
                    data={nodeData}
                    getChildNodes={getChildNodes}
                    setExpanded={setExpanded}
                    renderContent={props.renderNodeContent}
                    {...nodeProps}
                />
            ))}
        </>
    );
};
