import * as React from 'react';
import { AppActions, createAppActions } from '~/app/state/app.actions';
import { AppSelectors, createAppSelectors } from '~/app/state/app.selectors';
import { AppAction, AppState } from '~/app/state/app.reducer';

export type AppContextProps = Partial<AppActions & AppSelectors>;

export const AppContext = React.createContext<AppContextProps>({});

export const createAppContextValue = (
    appState: AppState,
    dispatcher: React.Dispatch<AppAction>,
): AppContextProps => {
    return {
        ...createAppActions(dispatcher, appState),
        ...createAppSelectors(appState),
    };
};
