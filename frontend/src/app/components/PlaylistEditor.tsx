import { PlaylistContext } from '~/app/state/playlist/playlist.context';
import { Select } from '~/common/components/Select';
import { TreeView } from '~/common/components/TreeView/TreeView';
import { RuleTreeNodeContent } from '~/app/components/RuleTreeNodeContent';
import { Input } from '~/common/components/Input';
import { CheckBox } from '~/common/components/CheckBox';
import * as React from 'react';
import { AppContext } from '~/app/state/app.context';
import { defaultGroupMatchType, RuleMatchTypes, UpdateTypes } from '~/app/app.const';
import { Inline } from '~/common/components/Inline';
import { TreeNodeData } from '~/common/components/TreeView/types/tree';
import { RuleOrRuleGroup } from '~/app/types/rule';
import { AutoSize } from '~/common/components/AutoSize';

type PlaylistEditorProps = {};

export const PlaylistEditor: React.FC<PlaylistEditorProps> = () => {
    const playlistContext = React.useContext(PlaylistContext);
    const appContext = React.useContext(AppContext);
    const { updateBasicData, changeExpand, isShuffleUpdateType } = playlistContext;
    const rulesTree = playlistContext.getRulesTree();
    const basicData = playlistContext.getBasicData();
    const limitOrdersBy = appContext.getLimitOrdersBy();

    return (
        <>
            <Inline>
                <Input
                    maxWidth={true}
                    value={basicData.name}
                    label="Name:"
                    onBlur={e => updateBasicData({ name: e.target.value })}
                />
                <Select
                    label="Update type:"
                    values={UpdateTypes.map(x => x)}
                    value={basicData.updateType}
                    onChange={newVal =>
                        updateBasicData({
                            updateType: newVal,
                        })
                    }
                />
            </Inline>

            <Inline>
                <CheckBox
                    checked={basicData.limit.hasLimit}
                    label="Limit"
                    onChange={e =>
                        updateBasicData({
                            limit: {
                                ...basicData.limit,
                                hasLimit: e.target.checked,
                            },
                        })
                    }
                />
                <Input
                    disabled={!basicData.limit.hasLimit}
                    maxWidth={true}
                    value={basicData.limit.maxItems}
                    label="Max items:"
                    type="number"
                    onBlur={e =>
                        updateBasicData({
                            limit: {
                                ...basicData.limit,
                                maxItems: Number(e.target.value),
                            },
                        })
                    }
                />
                <Select
                    disabled={!basicData.limit.hasLimit || isShuffleUpdateType()}
                    maxWidth={true}
                    values={limitOrdersBy}
                    label="Sort by:"
                    value={basicData.limit.orderBy}
                    onChange={newVal =>
                        updateBasicData({
                            limit: {
                                ...basicData.limit,
                                orderBy: newVal,
                            },
                        })
                    }
                />
            </Inline>

            <TreeView
                data={rulesTree}
                renderNodeContent={nodeData => <RuleTreeNodeContent node={nodeData} />}
                onExpandedChange={(node, isExpanded) => changeExpand(node, isExpanded)}
                overrides={{
                    Node: {
                        props: {
                            overrides: {
                                ContentContainer: {
                                    props: {
                                        renderGroupHeader: nodeData => (
                                            <GroupContentHeader node={nodeData} />
                                        ),
                                    },
                                },
                            },
                        },
                    },
                }}
            />
        </>
    );
};

const GroupContentHeader: React.FC<{ node: TreeNodeData<RuleOrRuleGroup> }> = props => {
    const playlistContext = React.useContext(PlaylistContext);
    const nodeData = props.node.data;
    return (
        <>
            {nodeData.kind === 'ruleGroup' && (
                <AutoSize>
                    <Select
                        values={RuleMatchTypes.map(x => x)}
                        value={nodeData.matchMode || defaultGroupMatchType}
                        onChange={newVal => playlistContext.changeMatchMode(nodeData, newVal)}
                    />
                </AutoSize>
            )}
        </>
    );
};
