import {basicSetup} from "codemirror";
import {EditorView, keymap} from "@codemirror/view";
import {indentWithTab} from "@codemirror/commands";
import {javascript} from "@codemirror/lang-javascript";
import {python} from "@codemirror/lang-python";
import {php} from "@codemirror/lang-php";
import {json} from "@codemirror/lang-json";
import {yaml} from "@codemirror/lang-yaml";
import {html} from "@codemirror/lang-html";
import {sql} from "@codemirror/lang-sql";
import {xml} from "@codemirror/lang-xml";
import {css} from "@codemirror/lang-css";
import {shell} from "@codemirror/legacy-modes/mode/shell";
import {dockerFile} from "@codemirror/legacy-modes/mode/dockerfile";
import {go} from "@codemirror/legacy-modes/mode/go";
import {lua} from "@codemirror/legacy-modes/mode/lua";
import {properties} from "@codemirror/legacy-modes/mode/properties";
import {StreamLanguage} from "@codemirror/language"
import {githubDark} from "@fsegurai/codemirror-theme-bundle";

const roundedTheme = EditorView.theme({
    "&": {
        borderRadius: "8px",
        overflow: "hidden"
    },
    ".cm-gutters": {
        borderRadius: "8px 0 0 8px"
    }
})

function getLanguageExtension(language) {
    switch (language) {
        case "javascript":
            return javascript();

        case "html":
            return html();

        case "json":
            return json();

        case "sql":
            return sql();

        case "yaml":
            return yaml();

        case "php":
            return php();

        case "xml":
            return xml();

        case "python":
            return python();

        case "css":
            return css();

        case "shell":
            return StreamLanguage.define(shell);

        case "dockerfile":
            return StreamLanguage.define(dockerFile);

        case "go":
            return StreamLanguage.define(go);

        case "lua":
            return StreamLanguage.define(lua);

        case "properties":
            return StreamLanguage.define(properties);

        default:
            return javascript();
    }
}

window.CodeMirrorAdapter = {
    init: function (element, initialValue, dotnetRef, language) {
        const extensions = [
            basicSetup,
            githubDark,
            roundedTheme,
            keymap.of([indentWithTab])
        ];

        if (language)
            extensions.push(getLanguageExtension(language));

        return new EditorView({
            doc: initialValue,
            extensions: extensions,
            parent: element
        });
    },
    setValue: function (view, newValue) {
        view.dispatch({
            changes: {from: 0, to: view.state.doc.length, insert: newValue}
        });
    },
    getValue: function (view) {
        return view.state.doc.toString();
    }
};