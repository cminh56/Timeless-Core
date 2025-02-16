export const IGNORES_MESSAGE_KEY: string[] = [];

export const EM_STATUS = {
    unauthenticated: 'unauthenticated',
    loading: 'loading',
    RefreshAccessTokenError: 'RefreshAccessTokenError',
    authenticated: 'authenticated',
};

export const KEY_AUTH_TOKEN = 'EVENTMATE_AUTH_TOKEN';
export const AUTHENTICATION_ERROR_CODE = {
    UNAUTHORIZED: '401',
    LOGIN_TIMEOUT: 'OAuthCallback', 
};