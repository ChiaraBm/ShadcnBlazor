window.languageSelector = {
    get: () => window.localStorage['BlazorLanguage'],
    set: (value) => window.localStorage['BlazorLanguage'] = value
}