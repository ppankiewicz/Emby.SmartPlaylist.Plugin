import * as React from 'react';
import { OverrideProps, useOverrides } from '~/common/hooks/useOverrides';
import { Button } from '~/common/components/Button';
import { Icon } from '~/common/components/Icon';
import { TreeNodeData } from '~/common/components/TreeView/types/tree';
import { Inline } from '~/common/components/Inline';

export type TreeNodeProps = {
    data: TreeNodeData;
    overrides?: {
        ContentContainer: OverrideProps<ContentContainerProps>;
    };
    setExpanded(data: TreeNodeData, isExpanded: boolean): void;
    getChildNodes(data: TreeNodeData): TreeNodeData[];
    renderContent(data: TreeNodeData): React.ReactNode;
};

export type ContentContainerProps = {
    renderGroupHeader?(data: TreeNodeData): React.ReactNode;
} & React.HtmlHTMLAttributes<any>;

const ContentContainerComp: React.FC<ContentContainerProps> = props => {
    return <Inline>{props.children}</Inline>;
};

export const TreeNode: React.FC<TreeNodeProps> = props => {
    const childrenNodes = () => props.getChildNodes(props.data);
    const hasChildren = () => childrenNodes().length > 0;
    const { isExpanded, isRoot } = props.data;

    const [ContentContainer, contentContainerProps] = useOverrides(
        props.overrides && props.overrides.ContentContainer,
        ContentContainerComp,
    );

    const nodeContent = React.useMemo(() => {
        return (
            <ContentContainer {...contentContainerProps}>
                {hasChildren() && (
                    <>
                        <Button onClick={_ => props.setExpanded(props.data, !isExpanded)}>
                            {isExpanded ? <Icon type={'expanded'} /> : <Icon type={'collapsed'} />}
                        </Button>

                        {contentContainerProps.renderGroupHeader &&
                            contentContainerProps.renderGroupHeader(props.data)}
                    </>
                )}

                {props.renderContent(props.data)}
            </ContentContainer>
        );
    }, [props.data]);

    return (
        <div style={!isRoot ? { marginLeft: '2.4em' } : {}}>
            {nodeContent}

            {isExpanded &&
                childrenNodes().map(nodeData => (
                    <TreeNode {...props} key={nodeData.id} data={nodeData} />
                ))}
        </div>
    );
};
