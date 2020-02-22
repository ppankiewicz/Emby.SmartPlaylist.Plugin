import { AppData, AppPlaylist, AppPlaylists } from '~/app/types/appData';
import camelcaseKeys = require('camelcase-keys');
import { parseDate } from '~/common/helpers/date';
import { convertObjectPropValues } from '~/common/helpers/object';

type ApiClient = {
    getPluginConfiguration<TConfig>(pluginId: string): Promise<TConfig>;
    updatePluginConfiguration<TConfig>(pluginId: string, config: TConfig): Promise<any>;
    ajax<T = any>(request: any): Promise<T>;
};

declare global {
    // tslint:disable-next-line:interface-name
    interface Window {
        Dashboard: any;
        ApiClient: ApiClient;
    }
}

export const loadAppData = async (appId: string): Promise<AppData> => {
    let appData = await window.ApiClient.ajax<AppData>(
        {
            url: '/smartplaylist/appData',
            type: 'GET',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            contentType: 'application/json',
            dataType: 'json',
        }
    );

    appData = camelcaseKeys(appData, {
        deep: true,
    }) as AppData;

    convertObjectPropValues(appData, o => parseDate(o));

    return new Promise<AppData>(res => {
        res({
            appId: appId,
            ...appData,
        });
    });
};

export const saveAppPlaylist = async (playlist: AppPlaylist): Promise<AppPlaylists> => {
    return window.ApiClient.ajax(
        {
            url: '/smartplaylist',
            type: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            data: JSON.stringify(playlist),
        }
    );
};

export const deletePlaylist = async (playlistId: string): Promise<any> => {
    return window.ApiClient.ajax(
        {
            url: `/smartplaylist/${playlistId}`,
            type: 'DELETE',
        }
    );
};
