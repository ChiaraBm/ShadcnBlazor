import tailwindcss from '@tailwindcss/postcss';
import cssnano from 'cssnano';
import extractClasses from './extract-classes.js';

const config = {
    plugins: [
        tailwindcss(),
        cssnano({
            preset: "default"
        }),
    ],
};

if (process.env.EXTRACT_CLASSES === "true") {
    config.plugins.push(extractClasses());
}

export default config;
