export type TreeNodeData<T = any> = {
    id: string;
    isRoot: boolean;
    children: string[];
    isExpanded: boolean;
    level: number;
    data?: T;
};

export type TreeViewData<T = any> = {
    byId: {
        [Key: string]: TreeNodeData<T>;
    };
    rootIds: string[];
};
