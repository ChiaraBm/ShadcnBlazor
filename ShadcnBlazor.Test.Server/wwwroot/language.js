window.languageSelector = {
    set: (value) => document.cookie = `.AspNetCore.Culture=${encodeURIComponent(value)}; path=/`
} 