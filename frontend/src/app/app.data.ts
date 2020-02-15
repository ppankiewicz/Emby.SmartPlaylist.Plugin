import { AppData, AppPlaylist, AppPlaylists } from '~/app/types/appData';
import { demoAppData } from '~/app/app.demo';

export const loadAppData = (appId: string): Promise<AppData> => {
    return new Promise<AppData>(res => {
        res({
            appId: appId,
            ...demoAppData,
        });
    });
};

export const saveAppPlaylist = (playlist: AppPlaylist): Promise<AppPlaylists> => {
    // tslint:disable-next-line:no-console
    console.log(`saveAppPlaylist:\n ${JSON.stringify(playlist)}`);
    return new Promise<AppPlaylists>(res => {
        res();
    });
};

export const deletePlaylist = (playlistId: string): Promise<void> => {
    return new Promise<void>(res => {
        res();
    });
};
