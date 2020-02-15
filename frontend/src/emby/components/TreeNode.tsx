import * as React from 'react';
import { TreeNode as TreeNodeBase, TreeNodeProps } from '~/common/components/TreeView/TreeNode';

export const TreeNode: React.FC<TreeNodeProps> = props => {
    return (
        <TreeNodeBase
            {...props}
            overrides={{
                ...props.overrides,
                ContentContainer: {
                    ...props.overrides.ContentContainer,
                    props: {
                        ...props.overrides.ContentContainer.props,
                        className: 'align-items-center justify-content-center',
                    },
                },
            }}
        />
    );
};
