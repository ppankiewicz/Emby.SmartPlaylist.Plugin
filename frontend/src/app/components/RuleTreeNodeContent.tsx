import * as React from 'react';
import { RuleOrRuleGroup } from '~/app/types/rule';
import { RuleTreeNodeActions } from '~/app/components/RuleTreeNodeActions';
import { RuleEditor } from '~/app/components/RuleEditor/RuleEditor';
import { TreeNodeData } from '~/common/components/TreeView/types/tree';
import { FloatRight } from '~/common/components/FloatRight';

export const RuleTreeNodeContent: React.FC<{
    node: TreeNodeData<RuleOrRuleGroup>;
}> = props => {
    const { node } = props;

    return (
        <>
            {node.data.kind === 'rule' && <RuleEditor value={node.data} />}

            <FloatRight>
                <RuleTreeNodeActions {...props} />
            </FloatRight>
        </>
    );
};
